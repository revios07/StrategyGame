using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = "UI/ObjectAssignerData", fileName = "ObjectAssignerData")]
public class GameObjectAssignerScriptable : ScriptableObject
{
    [NaughtyAttributes.BoxGroup("Local Scales When Assigned an Object")]
    [SerializeField]
    public Vector3 atBarracksLocalScale, atPowerPlantLocalScale, atSoldierLocalScale;

    [NaughtyAttributes.BoxGroup("Pos Diffs to Local Center")]
    [SerializeField]
    public Vector3 atBarracksPosDiffToZero, atPowerPlantPosDiffToZero, atSoldierPosDiffToZero;

    public void GetVectors(out Vector3 localScaleVector, out Vector3 assignerPosDiff, ObjectType objectType)
    {
        localScaleVector = Vector3.zero;
        assignerPosDiff = Vector3.zero;

        switch (objectType)
        {
            case (ObjectType.Barracks):
                {
                    localScaleVector = atBarracksLocalScale;
                    assignerPosDiff = atBarracksPosDiffToZero;
                    break;
                }
            case (ObjectType.PowerPlant):
                {
                    localScaleVector = atPowerPlantLocalScale;
                    assignerPosDiff = atPowerPlantPosDiffToZero;
                    break;
                }
            case (ObjectType.Soldier):
                {
                    localScaleVector = atSoldierLocalScale;
                    assignerPosDiff = atSoldierPosDiffToZero;
                    break;
                }
        }
    }
}
