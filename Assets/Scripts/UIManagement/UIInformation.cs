using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public class UIInformation : UIInformationReferances
{
    #region Unity Calls
    private void OnEnable()
    {
        EventManager.onSoldierSelectedInProductionPanel += UpdateProductionInformation;
        EventManager.onTowerSelectedInProductionPanel += UpdateProductionInformation;

        EventManager.onTowerScriptableSelectedGameBoard += UpdateInformationPanel;
        EventManager.onSoldierSelectedGameBoard += UpdateInformationPanel;
    }
    private void OnDisable()
    {
        EventManager.onSoldierSelectedInProductionPanel -= UpdateProductionInformation;
        EventManager.onTowerSelectedInProductionPanel -= UpdateProductionInformation;

        EventManager.onTowerScriptableSelectedGameBoard -= UpdateInformationPanel;
        EventManager.onSoldierSelectedGameBoard -= UpdateInformationPanel;
    }
    private void Start()
    {
        OpenTextes(false);
        ResetNames();
    }
    #endregion

    //Tower Selected On GameBoard
    private Structs.TowerStruct UpdateInformationPanel(ref Structs.TowerStruct selectedTower)
    {
        base.ResetNames();
        base.OpenTextes(false);

        m_buildingText.text = selectedTower.towerName;
        m_buidingImage.color = Color.white;
        m_buidingImage.sprite = selectedTower.towerSprite;

        m_buildingHealth.gameObject.SetActive(true);
        m_buildingHealth.text = "Health => " + selectedTower.towerHealth;

        //Only Barracks Can Spawn Soldiers
        if (selectedTower.objectType == Enums.ObjectType.Barracks && selectedTower.isPlaced)
        {
            //Can Spawn Soldiers
            OpenCanSpawnSoldierArea();
        }
        else
        {
            OpenTextes(false);
        }

        //Can Show Barracks Health Here
        //<region>
        //Show Soldiers Of Barrack Here
        //</region>

        return selectedTower;
    }

    //Soldier Selected On GameBoard
    private Structs.SoldierStruct UpdateInformationPanel(ref Structs.SoldierStruct selectedSoldier)
    {
        base.ResetNames();
        base.OpenTextes(false);

        m_soldierHealthText.transform.parent.localPosition = Vector3.up * 200f;

        /*
        m_soldierImage.sprite = selectedSoldier.soldierSprite;
        m_soldierImage.color = Color.white;
        m_soldierText.text = "Soldier " + selectedSoldier.soldierIndex;
        */

        m_buildingText.text = "Soldier " + selectedSoldier.soldierIndex;
        m_buidingImage.color = Color.white;
        m_buidingImage.sprite = selectedSoldier.soldierSprite;

        //Can show Soldiers Health Here
        //Can show Soldiers Damage Here
        m_soldierImage.gameObject.SetActive(false);
        m_soldierDamageText.gameObject.SetActive(true);
        m_soldierHealthText.gameObject.SetActive(true);

        m_soldierHealthText.text = "Health => " + selectedSoldier.soldierHealth;
        m_soldierDamageText.text = "Damage => " + selectedSoldier.soldierDamage;

        //m_buidingImage.sprite = m_emptySprite;
        //m_buildingText.text = "Barracks";

        return selectedSoldier;
    }

    //Tower Selected On Production Panel
    private Structs.TowerStruct UpdateProductionInformation(ref Structs.TowerStruct selectedTower)
    {
        UpdateInformationPanel(ref selectedTower);

        m_soldierImage.color = Color.red;
        m_soldierImage.sprite = m_emptySprite;

        m_soldierDamageText.gameObject.SetActive(false);
        m_soldierHealthText.gameObject.SetActive(false);

        return selectedTower;
    }

    //Soldier Selected On Production Panel
    private Structs.SoldierStruct UpdateProductionInformation(ref Structs.SoldierStruct selectedSoldier)
    {
        UpdateInformationPanel(ref selectedSoldier);

        return selectedSoldier;
    }
}
