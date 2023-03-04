using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public class UIInformation : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_informationText;
    [SerializeField]
    private Image m_informationImage, m_productionImage;

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

    private TowerScriptable UpdateInformationPanel(TowerScriptable selectedTower)
    {
        var towerStruct = selectedTower.GetTowerData();
        m_informationText.text = towerStruct.towerName;
        m_informationImage.sprite = towerStruct.towerSprite;

        return null;
    }

    private SoldierScriptable UpdateInformationPanel(SoldierScriptable selectedSoldier)
    {
        var soldierStruct = selectedSoldier.GetSoldierData();
        m_informationImage.sprite = soldierStruct.soldierSprite;
        m_informationText.text = "Soldier " + soldierStruct.soldierIndex;

        return null;
    }

    private TowerScriptable UpdateProductionInformation(TowerScriptable selectedTower)
    {
        m_productionImage.sprite = selectedTower.GetTowerData().towerSprite;

        return null;
    }

    private SoldierScriptable UpdateProductionInformation(SoldierScriptable selectedSoldier)
    {
        m_productionImage.sprite = selectedSoldier.GetSoldierData().soldierSprite;

        return null;
    }
}
