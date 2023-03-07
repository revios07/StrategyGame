using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private InputData _inputData;

    private Soldier soldier;
    private Building building;

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
    }

    private void Update()
    {
        if (pickedObjectTrasform == null)
            return;

        if (Input.GetMouseButtonDown(0) && (pickedObjectType != Enums.ObjectType.Soldier) && building.CanPlaceable())
        {
            building.Placed();
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ReleaseObject();
            return;
        }

        Debug.Log(pickedObjectTrasform.name);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition());
        worldPos.z = 10f;
        pickedObjectTrasform.position = Vector3.Lerp(pickedObjectTrasform.position, worldPos, 10f * Time.deltaTime);


        //Building Picked
        if (pickedObjectType != Enums.ObjectType.Soldier && !building.IsPlaced())
        {
            /*
            Debug.Log(pickedObjectTrasform.name);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition());
            worldPos.z = 10f;
            pickedObjectTrasform.position = Vector3.Lerp(pickedObjectTrasform.position, worldPos, 10f * Time.deltaTime);
            */
        }
        else if(pickedObjectType == Enums.ObjectType.Soldier)
        {

        }
    }

    public void ReleaseObject()
    {
        if(pickedObjectTrasform != null)
        {
            EventManager.onObjectAddToPool?.Invoke(pickedObjectType, pickedObjectTrasform);
            selectableAbstract = null;
            pickedObjectTrasform = null;
            pickedObjectTrasform = null;
            soldier = null;
            return;
        }
    }
}
