using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : GridPlacementSystem
{
    [SerializeField]
    private InputData _inputData;

    private Soldier soldier;
    private Building building;

    [SerializeField]
    private Vector2 _limitPlacementArea;

    public SelectableAbstract selectableAbstract { get; private set; }
    public Transform pickedObjectTrasform { get; private set; }
    public Enums.ObjectType pickedObjectType { get; private set; }

    private void OnEnable()
    {
        EventManager.pickedFromPool += PickObject;
        EventManager.onSoldierSpawnedRequest += ReleaseObject;
    }

    private void OnDisable()
    {
        EventManager.pickedFromPool -= PickObject;
        EventManager.onSoldierSpawnedRequest += ReleaseObject;
    }

    private void PickObject(Enums.ObjectType objectType, Transform pickedTransform)
    {
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
        Vector3 mousePosOnGame = Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition());

        if (pickedObjectTrasform == null)
            return;
        Debug.Log(pickedObjectTrasform.name);

        if (Input.GetMouseButtonDown(1))
        {
            ReleaseObject();
            return;
        }

        //Cant Place Area
        if (_inputData.GetMousePosition().x < Screen.width / 3f || _inputData.GetMousePosition().x > (Screen.width - Screen.width / 3f))
        {
            mousePosOnGame.z = 10f;
            pickedObjectTrasform.position = Vector3.Lerp(pickedObjectTrasform.position, mousePosOnGame, 10f * Time.deltaTime);
        }
        else //Picked Object On Placeable Area
        {
            if (pickedObjectTrasform != null)
            {
                if (!selectableAbstract.isPlaced)
                {
                    Vector3Int cellPos = gridLayout.LocalToCell(mousePosOnGame);

                    if (previousPos != cellPos)
                    {
                        selectableAbstract.transform.parent = gridLayout.transform;
                        selectableAbstract.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f, 0.0f));
                        previousPos = cellPos;
                        FollowBuildings(selectableAbstract);
                    }
                }

                if (Input.GetMouseButtonDown(0) && selectableAbstract.CanBePlaced())
                {
                    Debug.Log("Placed");
                    building.Placed();
                    selectableAbstract = null;
                    pickedObjectTrasform = null;
                    pickedObjectTrasform = null;
                    return;
                }
            }
        }
    }

    public void ReleaseObject()
    {
        if (pickedObjectTrasform != null)
        {
            EventManager.onObjectAddToPool?.Invoke(pickedObjectType, pickedObjectTrasform);
            selectableAbstract = null;
            pickedObjectTrasform = null;
            pickedObjectTrasform = null;
            soldier = null;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        //Limit Areas
        Gizmos.DrawLine(new Vector3(_limitPlacementArea.x, _limitPlacementArea.y), new Vector3(_limitPlacementArea.x, -_limitPlacementArea.y));
        Gizmos.DrawLine(new Vector3(_limitPlacementArea.x * -1.0f, _limitPlacementArea.y), new Vector3(_limitPlacementArea.x * -1.0f, -_limitPlacementArea.y));
    }
}
