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
        
    }

    private void OnDisable()
    {
        
    }

    private void UpdateInformationPanel(TowerScriptable selectedTower)
    {

    }

    private void UpdateInformationPanel(SoliderScriptable selectedSoldier)
    {

    }
}
