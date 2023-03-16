using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Interfaces;

public class GamePlayController : GridPlacementSystem
{
    public static bool isAttackContinue { get; set; }
    public static bool isCarrying { get; private set; }

    public static Soldier lastSelectedSoldier;
    public static Building lastSelectedBuilding;

    public static List<Soldier> currentlyAttakingSoldiers = new List<Soldier>();
    public static List<Soldier> currentlyTakeingDamageSoldiers = new List<Soldier>();
    public static List<Soldier> currentlyAllPlacedSoldiers = new List<Soldier>();

    private bool _isSoldierCarryStarted;
    private float _soldierCarryTimer;

    [SerializeField]
    private Vector2 _limitPlacementArea;
    [SerializeField]
    private InputDataSO _inputData;

    private Soldier _soldier;
    private Building _building;

    public SelectableAbstract selectableAbstract { get; private set; }
    public Transform followTransform { get; private set; }
    public Enums.ObjectType pickedObjectType { get; private set; }

    private void OnEnable()
    {
        currentlyAttakingSoldiers.Clear();
        EventManager.pickedFromPool += PickObject;
        //EventManager.onSoldierSpawnedRequest += ReleaseObject;
    }
    private void OnDisable()
    {
        EventManager.pickedFromPool -= PickObject;
        //EventManager.onSoldierSpawnedRequest -= ReleaseObject;
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

            #region Soldier Transform Carry

            if (Input.GetMouseButton(0) && !currentlyTakeingDamageSoldiers.Contains(lastSelectedSoldier))
            {
                _soldierCarryTimer = Mathf.Clamp(_soldierCarryTimer, 0, 10f);
                _soldierCarryTimer += 1f * Time.deltaTime;
            }
            else
            {
                _soldierCarryTimer = 0f;
            }

            if (_soldierCarryTimer > 0.35f || _isSoldierCarryStarted)
            {
                //Move To Area With Pathfind? <<<< Didn't used :/ >>>>>

                followTransform = lastSelectedSoldier.transform;
                lastSelectedSoldier.isPlaced = false;
                selectableAbstract = lastSelectedSoldier;

                if (!_isSoldierCarryStarted)
                {
                    SetTilesBlock(lastSelectedSoldier.sizeArea, Enums.TileType.White, playableAreaTilemap);
                    _isSoldierCarryStarted = true;
                    return;
                    //ControllAndSetSoldiersTilesBlocks(lastSelectedSoldier.transform, lastSelectedSoldier.sizeArea, playableAreaTilemap);
                }

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

    private void PickObject(Enums.ObjectType objectType, Transform pickedTransform)
    {
        if (objectType == Enums.ObjectType.Bullet)
            return;

        if (objectType == Enums.ObjectType.Soldier)
        {
            _soldierCarryTimer = 0f;
            _isSoldierCarryStarted = false;
            return;

            /*ReleaseObject();
            pickedTransform.TryGetComponent<Soldier>(out _soldier);

            Debug.Log("Soldier Hanged!");*/
        }

        //Currently Picked Object Give it To Pool
        _isSoldierCarryStarted = false;
        _soldierCarryTimer = 0f;
        ReleaseObject();

        Debug.Log("Picked");
        followTransform = pickedTransform;
        pickedObjectType = objectType;
        selectableAbstract = followTransform.GetComponent<SelectableAbstract>();
        SelectedObjectAssigner.instance.StartAssign(followTransform);

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
    public void ReleaseObject()
    {
        _soldier = null;
        lastSelectedSoldier = null;
        _soldierCarryTimer = 0f;
        SelectedObjectAssigner.instance.StopAssign();

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

    //Building
    public void MoveWithMousePos(ref Vector3 mousePosOnGame)
    {
        //Cant Place Area
        if (_inputData.GetMousePosition().x < Screen.width / 5f || _inputData.GetMousePosition().x > (Screen.width - Screen.width / 5f))
        {
            isCarrying = true;
            mousePosOnGame.z = 10f;
            followTransform.position = Vector3.Lerp(followTransform.position, mousePosOnGame, 10f * Time.deltaTime);
            ClearArea();
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
                        isCarrying = true;
                        selectableAbstract.transform.parent = gridLayout.transform;
                        selectableAbstract.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f, 0.0f));
                        previousPos = cellPos;
                        FollowBuildings(selectableAbstract);
                    }

                    if (lastSelectedSoldier != null)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (lastSelectedSoldier.CanBePlaced())
                            {
                                TakeArea(selectableAbstract.sizeArea, selectableAbstract.objectType);
                                lastSelectedSoldier.PlaceToArea();
                                SelectedObjectAssigner.instance.StopAssign();

                                isCarrying = false;
                                _isSoldierCarryStarted = false;
                                _soldierCarryTimer = 0f;

                                _soldier = null;
                                _building = null;
                                lastSelectedSoldier = null;
                                lastSelectedBuilding = null;
                                selectableAbstract = null;
                                followTransform = null;
                                return;

                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                            return;
                    }

                    //Place To Area
                    if (Input.GetMouseButtonDown(0) && selectableAbstract.CanBePlaced())
                    {
                        Debug.Log("Placed!");


                        if (_building != null)
                        {
                            _building.PlaceToArea();
                        }

                        TakeArea(selectableAbstract.sizeArea, selectableAbstract.objectType);

                        isCarrying = false;
                        _isSoldierCarryStarted = false;
                        _soldierCarryTimer = 0f;

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
    //Soldier
    public void MoveWithMousePos(ref Vector3 mousePosOnGame, bool isSoldier)
    {
        ControllAndSetSoldiersTilesBlocks(followTransform, lastSelectedSoldier.sizeArea, playableAreaTilemap);
        MoveWithMousePos(ref mousePosOnGame);
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
