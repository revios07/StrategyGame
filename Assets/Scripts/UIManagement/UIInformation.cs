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
        CloseHealthTextes();
        ResetNames();
    }
    #endregion

    //Tower Selected On GameBoard
    private TowerScriptable UpdateInformationPanel(TowerScriptable selectedTower)
    {
        base.ResetNames();
        base.CloseHealthTextes();

        var towerStruct = selectedTower.GetTowerData();
        m_buildingText.text = towerStruct.towerName;
        m_buidingImage.color = Color.white;
        m_buidingImage.sprite = towerStruct.towerSprite;

        m_buildingHealth.gameObject.SetActive(true);
        m_buildingHealth.text = "Health => " + towerStruct.towerHealth;

        //Can Show Barracks Health Here

        //<region>
        //Show Soldiers Of Barrack Here
        //</region>

        return null;
    }

    //Soldier Selected On GameBoard
    private SoldierScriptable UpdateInformationPanel(SoldierScriptable selectedSoldier)
    {
        base.ResetNames();
        base.CloseHealthTextes();

        var soldierStruct = selectedSoldier.GetSoldierData();
        m_soldierImage.sprite = soldierStruct.soldierSprite;
        m_soldierImage.color = Color.white;
        m_soldierText.text = "Soldier " + soldierStruct.soldierIndex;

        //Can show Soldiers Health Here
        //Can show Soldiers Damage Here
        m_soldierDamageText.gameObject.SetActive(true);
        m_soldierHealthText.gameObject.SetActive(true);

        m_soldierHealthText.text = "Health => " + soldierStruct.soldierHealth;
        m_soldierDamageText.text = "Damage => " + soldierStruct.soldierDamage;

        m_buidingImage.sprite = m_emptySprite;
        m_buildingText.text = "Barracks";

        return null;
    }

    //Barracks Selected On Production Panel
    private TowerScriptable UpdateProductionInformation(TowerScriptable selectedTower)
    {
        UpdateInformationPanel(selectedTower);

        m_soldierImage.color = Color.red;
        m_soldierImage.sprite = m_emptySprite;

        return null;
    }

    //Soldier Selected On Production Panel
    private SoldierScriptable UpdateProductionInformation(SoldierScriptable selectedSoldier)
    {
        UpdateInformationPanel(selectedSoldier);

        return null;
    }
}
