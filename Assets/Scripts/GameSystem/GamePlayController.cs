using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamePlayController : GridPlacementSystem
{
    [SerializeField]
    private InputData _inputData;

    private Soldier _soldier;
    private Building _building;

    public static Soldier lastSelectedSoldier;
    public static Building lastSelectedBuilding;
    public static bool isAttackContinue;
    public static List<Soldier> currentlyAttakingSoldiers = new List<Soldier>();

    [SerializeField]
    private Vector2 _limitPlacementArea;

    public SelectableAbstract selectableAbstract { get; private set; }
    public Transform followTransform { get; private set; }
    public Enums.ObjectType pickedObjectType { get; private set; }

    private void OnEnable()
    {
        currentlyAttakingSoldiers.Clear();
        ReleaseObject();
        EventManager.pickedFromPool += PickObject;
        EventManager.onSoldierSpawnedRequest += ReleaseObject;
    }

    private void OnDisable()
    {
        EventManager.pickedFromPool -= PickObject;
        EventManager.onSoldierSpawnedRequest -= ReleaseObject;
    }

    private void PickObject(Enums.ObjectType objectType, Transform pickedTransform)
    {
        if (objectType == Enums.ObjectType.Bullet)
            return;

        if (objectType == Enums.ObjectType.Soldier)
        {
            return;

            /*ReleaseObject();
            pickedTransform.TryGetComponent<Soldier>(out _soldier);

            Debug.Log("Soldier Hanged!");*/
        }

        //Currently Picked Object Give it To Pool
        ReleaseObject();

        Debug.Log("Picked");
        followTransform = pickedTransform;
        pickedObjectType = objectType;
        selectableAbstract = followTransform.GetComponent<SelectableAbstract>();

        //Object is Soldier
        if (objectType == Enums.ObjectType.Soldier)
        {
            followTransform.TryGetComponent<Soldier>(out _soldier);
        }
        //Object is Building
        else
        {
            followTransform.TryGetComponent<Building>(out _building);
        }

        FollowBuildings(selectableAbstract);
    }

    private void Update()
    {
        Vector3 mousePosOnGame = Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition());

        #region Soldier Contorller
        if (lastSelectedSoldier != null)
        {
            if (currentlyAttakingSoldiers.Contains(lastSelectedSoldier))
            {
                //Soldier Still Attacking!
                return;
            }

            if (Input.GetMouseButton(2))
            {
                ReleaseObject();
                return;
            }

            #region Soldier Transform Carry
            if (Input.GetMouseButton(0))
            {
                //Move To Area With Pathfind? <<<< Didn't used :/ >>>>>

                followTransform = lastSelectedSoldier.transform;
                lastSelectedSoldier.isPlaced = false;
                selectableAbstract = lastSelectedSoldier;
                MoveWithMousePos(ref mousePosOnGame, true);
                return;
            }
            #endregion

            if (Input.GetMouseButtonDown(1))
            {
                //Move to Area then Attack To Target

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition()), Vector2.zero);
                if (hit.collider != null)
                {
                    ICanTakeDamagePlayableObject canTakeDamagePlayableObject1;

                    hit.transform.TryGetComponent<ICanTakeDamagePlayableObject>(out canTakeDamagePlayableObject1);

                    if (canTakeDamagePlayableObject1 != null)
                    {
                        if (hit.transform.gameObject.GetInstanceID() == lastSelectedSoldier.transform.gameObject.GetInstanceID())
                        {
                            //Cant Attack To Self
                            ReleaseObject();
                            Debug.Log("Cant Attack To Self!");
                            return;
                        }

                        if (!currentlyAttakingSoldiers.Contains(lastSelectedSoldier))
                            currentlyAttakingSoldiers.Add(lastSelectedSoldier);

                        isAttackContinue = true;
                        lastSelectedSoldier.transform.GetComponent<ICanAttackObject>().GiveDamage(hit.transform, canTakeDamagePlayableObject1);
                    }
                }
                else
                {
                    //<summary>
                    //Check Can Moveable Area
                    //If Moveable Area Move to Area
                    //</summary>
                }
            }

            return;
        }
        #endregion

        #region Building & Placement Controller
        if (followTransform == null)
            return;

        //Release The Hanging Object
        if (Input.GetMouseButtonDown(1))
        {
            ClearArea();
            ReleaseObject();
            return;
        }

        MoveWithMousePos(ref mousePosOnGame);
        #endregion
    }

    public void MoveWithMousePos(ref Vector3 mousePosOnGame)
    {
        //Cant Place Area
        if (_inputData.GetMousePosition().x < Screen.width / 4f || _inputData.GetMousePosition().x > (Screen.width - Screen.width / 4f))
        {
            mousePosOnGame.z = 10f;
            followTransform.position = Vector3.Lerp(followTransform.position, mousePosOnGame, 10f * Time.deltaTime);
        }
        //Picked Object On Placeable Area
        else
        {
            if (followTransform != null && selectableAbstract != null)
            {
                if (!selectableAbstract.isPlaced)
                {
                    Vector3Int cellPos = gridLayout.LocalToCell(mousePosOnGame);

                    //Controll Player Can Place Selection With Sprite Change
                    if (previousPos != cellPos)
                    {
                        selectableAbstract.transform.parent = gridLayout.transform;
                        selectableAbstract.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f, 0.0f));
                        previousPos = cellPos;
                        FollowBuildings(selectableAbstract);
                    }

                    //Place To Area
                    if (Input.GetMouseButtonDown(0) && selectableAbstract.CanBePlaced())
                    {
                        Debug.Log("Placed!");

                        if (_soldier != null)
                        {
                            if (Input.GetMouseButtonUp(0))
                                _soldier.PlaceToArea();
                            else
                                return;
                        }
                        else if (_building != null)
                        {
                            _building.PlaceToArea();
                        }

                        TakeArea(selectableAbstract.sizeArea, selectableAbstract.objectType);

                        _soldier = null;
                        _building = null;
                        lastSelectedSoldier = null;
                        lastSelectedBuilding = null;
                        selectableAbstract = null;
                        followTransform = null;
                        return;
                    }
                }
            }
        }
    }

    public void MoveWithMousePos(ref Vector3 mousePosOnGame, bool isSoldier)
    {
        ControllAndSetSoldiersTilesBlocks(followTransform, lastSelectedSoldier.sizeArea, playableAreaTilemap);
        MoveWithMousePos(ref mousePosOnGame);
    }

    public void ReleaseObject()
    {
        _soldier = null;
        lastSelectedSoldier = null;

        if (followTransform != null && pickedObjectType != Enums.ObjectType.Soldier)
        {
            followTransform.transform.parent = null;
            EventManager.onObjectAddToPool?.Invoke(pickedObjectType, followTransform);
        }

        selectableAbstract = null;
        followTransform = null;
        _building = null;
        return;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Limit Areas
        Gizmos.DrawLine(new Vector3(_limitPlacementArea.x, _limitPlacementArea.y), new Vector3(_limitPlacementArea.x, -_limitPlacementArea.y));
        Gizmos.DrawLine(new Vector3(_limitPlacementArea.x * -1.0f, _limitPlacementArea.y), new Vector3(_limitPlacementArea.x * -1.0f, -_limitPlacementArea.y));
    }
#endif
}
