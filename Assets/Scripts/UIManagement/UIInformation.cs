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

        EventManager.onSelectableTakeDamageInGame += RefreshInformationPanel;
    }
    private void OnDisable()
    {
        EventManager.onSoldierSelectedInProductionPanel -= UpdateProductionInformation;
        EventManager.onTowerSelectedInProductionPanel -= UpdateProductionInformation;

        EventManager.onTowerScriptableSelectedGameBoard -= UpdateInformationPanel;
        EventManager.onSoldierSelectedGameBoard -= UpdateInformationPanel;

        EventManager.onSelectableTakeDamageInGame -= RefreshInformationPanel;
    }
    private void Start()
    {
        OpenTextes(false);
        ResetNames();
    }
    #endregion

    #region Item Selected on GameBoard
    //Tower Selected On GameBoard
    private Structs.TowerStruct UpdateInformationPanel(ref Structs.TowerStruct selectedTower, SelectableAbstract selectableAbstract)
    {
        base.ResetNames();
        base.OpenTextes(false);
        base.LoadReferancesForHealthUpdate(ref selectedTower);

        m_inGameItemPicked = true;
        m_selectableAbstract = selectableAbstract;

        if (selectedTower.objectType == Enums.ObjectType.Barracks)
        {
            m_pickedObjectType = Enums.ObjectType.Barracks;
        }
        else if (selectedTower.objectType == Enums.ObjectType.PowerPlant)
        {
            m_pickedObjectType = Enums.ObjectType.PowerPlant;
        }

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
    private Structs.SoldierStruct UpdateInformationPanel(ref Structs.SoldierStruct selectedSoldier, SelectableAbstract selectableAbstract)
    {
        base.ResetNames();
        base.OpenTextes(false);
        base.LoadReferancesForHealthUpdate(ref selectedSoldier);

        if (selectableAbstract != null)
        {
            m_inGameItemPicked = true;
            m_pickedObjectType = Enums.ObjectType.Soldier;
            m_selectableAbstract = selectableAbstract;
        }
        else
        {
            m_inGameItemPicked = false;
            m_pickedObjectType = Enums.ObjectType.Soldier;
            m_selectableAbstract = null;
        }

        m_soldierHealthText.transform.parent.localPosition = Vector3.up * 200f;

        m_buildingText.text = "Soldier " + selectedSoldier.soldierIndex;
        m_buidingImage.color = Color.white;
        m_buidingImage.sprite = selectedSoldier.soldierSprite;

        //Can show Soldiers Health/Damage Here
        m_soldierImage.gameObject.SetActive(false);
        m_soldierDamageText.gameObject.SetActive(true);
        m_soldierHealthText.gameObject.SetActive(true);

        m_soldierHealthText.text = "Health => " + selectedSoldier.soldierHealth;
        m_soldierDamageText.text = "Damage => " + selectedSoldier.soldierDamage;

        //m_buidingImage.sprite = m_emptySprite;
        //m_buildingText.text = "Barracks";

        return selectedSoldier;
    }
    #endregion

    #region Item Selected on Production Panel
    //Tower Selected On Production Panel
    private Structs.TowerStruct UpdateProductionInformation(ref Structs.TowerStruct selectedTower)
    {
        UpdateInformationPanel(ref selectedTower, null);
        m_inGameItemPicked = false;

        m_soldierImage.color = Color.red;
        m_soldierImage.sprite = m_emptySprite;

        m_soldierDamageText.gameObject.SetActive(false);
        m_soldierHealthText.gameObject.SetActive(false);

        return selectedTower;
    }
    //Soldier Selected On Production Panel
    private Structs.SoldierStruct UpdateProductionInformation(ref Structs.SoldierStruct selectedSoldier, SelectableAbstract selectableAbstract)
    {
        UpdateInformationPanel(ref selectedSoldier, selectableAbstract);
        m_inGameItemPicked = false;

        return selectedSoldier;
    }
    #endregion

    //Refresh Panel When Take Damage an Damageable Object
    private void RefreshInformationPanel(SelectableAbstract selectableAbstract)
    {
        if (!m_inGameItemPicked)
            return;

        //Soldier
        if ((selectableAbstract.objectType == Enums.ObjectType.Soldier) && (selectableAbstract == m_selectableAbstract))
        {
            m_soldierHealthText.text = "Health => " + selectableAbstract.GetComponent<Soldier>().soldierHealth;

        }
        //Building
        else if ((selectableAbstract.objectType == Enums.ObjectType.PowerPlant) || (selectableAbstract.objectType == Enums.ObjectType.Barracks) && (selectableAbstract == m_selectableAbstract))
        {
            m_buildingHealth.text = "Health => " + selectableAbstract.GetComponent<Building>().towerHealth;
        }
    }
}
