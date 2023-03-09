using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamePlayController : GridPlacementSystem
{
    [SerializeField]
    private InputData _inputData;

    private Soldier soldier;
    private Building building;

    public static Soldier lastSelectedSoldier;
    public static Building lastSelectedBuilding;
    public static bool isAttackContinue;

    [SerializeField]
    private Vector2 _limitPlacementArea;

    public SelectableAbstract selectableAbstract { get; private set; }
    public Transform pickedObjectTrasform { get; private set; }
    public Enums.ObjectType pickedObjectType { get; private set; }

    private void OnEnable()
    {
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

            ReleaseObject();
            pickedTransform.TryGetComponent<Soldier>(out soldier);

            Debug.Log("Soldier Hanged!");
        }

        //Currently Picked Object Give it To Pool
        ReleaseObject();

        Debug.Log("Picked");
        pickedObjectTrasform = pickedTransform;
        pickedObjectType = objectType;
        selectableAbstract = pickedObjectTrasform.GetComponent<SelectableAbstract>();

        //Object is Soldier
        if (objectType == Enums.ObjectType.Soldier)
        {
            pickedObjectTrasform.TryGetComponent<Soldier>(out soldier);
        }
        //Object is Building
        else
        {
            pickedObjectTrasform.TryGetComponent<Building>(out building);
        }

        FollowBuildings(selectableAbstract);
    }

    private void Update()
    {
        #region Soldier Contorller
        if (lastSelectedSoldier != null)
        {
            if (isAttackContinue)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                //Move To Area With Pathfind
            }

            if (Input.GetMouseButtonDown(1))
            {
                //Move to Area then Attack To Target

                Debug.Log("AAAA");

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition()), Vector2.zero);
                if (hit.collider != null)
                {
                    ICanTakeDamagePlayableObject canTakeDamagePlayableObject1;

                    hit.transform.TryGetComponent<ICanTakeDamagePlayableObject>(out canTakeDamagePlayableObject1);

                    if (canTakeDamagePlayableObject1 != null)
                    {
                        if (hit.transform.gameObject.GetInstanceID() == lastSelectedSoldier.transform.gameObject.GetInstanceID())
                        {
                            ReleaseObject();
                            //Cant Attack To Self
                            Debug.Log("Cant Attack To Self!");
                            return;
                        }

                        isAttackContinue = true;
                        lastSelectedSoldier.transform.GetComponent<ICanAttackObject>().GiveDamage(hit.transform, canTakeDamagePlayableObject1);
                    }
                }
            }

            return;
        }
        #endregion

        #region Building & Placement Controller
        if (pickedObjectTrasform == null)
            return;

        Vector3 mousePosOnGame = Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition());


        //Release The Hanging Object
        if (Input.GetMouseButtonDown(1))
        {
            ClearArea();
            ReleaseObject();
            return;
        }

        //Cant Place Area
        if (_inputData.GetMousePosition().x < Screen.width / 4f || _inputData.GetMousePosition().x > (Screen.width - Screen.width / 4f))
        {
            mousePosOnGame.z = 10f;
            pickedObjectTrasform.position = Vector3.Lerp(pickedObjectTrasform.position, mousePosOnGame, 10f * Time.deltaTime);
        }
        //Picked Object On Placeable Area
        else
        {
            if (pickedObjectTrasform != null)
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

                        if (soldier != null)
                        {
                            soldier.PlaceToArea();
                        }
                        else if (building != null)
                        {
                            building.PlaceToArea();
                        }

                        TakeArea(selectableAbstract.sizeArea, selectableAbstract.objectType);

                        soldier = null;
                        building = null;
                        selectableAbstract = null;
                        pickedObjectTrasform = null;
                        pickedObjectTrasform = null;
                        return;
                    }
                }
            }
        }
        #endregion
    }

    public void ReleaseObject()
    {
        soldier = null;
        lastSelectedSoldier = null;

        if (pickedObjectTrasform != null && pickedObjectType != Enums.ObjectType.Soldier)
        {
            pickedObjectTrasform.transform.parent = null;
            EventManager.onObjectAddToPool?.Invoke(pickedObjectType, pickedObjectTrasform);
        }

        selectableAbstract = null;
        pickedObjectTrasform = null;
        building = null;
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
