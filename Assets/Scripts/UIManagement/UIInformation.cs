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
        EventManager.onSoldierSelectedInProductionPanel += UpdateInformationPanel;
        EventManager.onTowerSelectedInProductionPanel += UpdateInformationPanel;
    }

    private void OnDisable()
    {
        EventManager.onSoldierSelectedInProductionPanel -= UpdateInformationPanel;
        EventManager.onTowerSelectedInProductionPanel -= UpdateInformationPanel;
    }

    private TowerScriptable UpdateInformationPanel(TowerScriptable selectedTower)
    {
        var towerStruct = selectedTower.GetTowerData();
        m_informationText.text = towerStruct.towerName;
        m_informationImage.sprite = towerStruct.towerSprite;

        return null;
    }

    private SoliderScriptable UpdateInformationPanel(SoliderScriptable selectedSoldier)
    {

        return null;
    }
}
