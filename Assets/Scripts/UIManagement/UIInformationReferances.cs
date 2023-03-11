using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public abstract class UIInformationReferances : MonoBehaviour
{
    [SerializeField]
    protected SoldierScriptable[] m_soldierDatas;

    [SerializeField]
    [BoxGroup("Names")]
    protected TMP_Text m_buildingText, m_soldierText;
    [BoxGroup("Images")]
    [SerializeField]
    protected Image m_buidingImage, m_soldierImage;
    [SerializeField]
    [BoxGroup("Soldiers Health-Damage")]
    protected TMP_Text m_soldierHealthText, m_soldierDamageText;
    [BoxGroup("Soldier Spawner Handler")]
    [SerializeField]
    protected ButtonPointerHandler m_soldierSpawnerButtonHandler;
    [SerializeField]
    [BoxGroup("Building Health")]
    protected TMP_Text m_buildingHealth;
    [SerializeField]
    protected Sprite m_emptySprite;

    protected void OpenTextes(bool isActive)
    {
        m_soldierImage.gameObject.SetActive(isActive);
        m_soldierText.gameObject.SetActive(isActive);
        m_soldierHealthText.gameObject.SetActive(isActive);
        m_soldierDamageText.gameObject.SetActive(isActive);
        m_buildingHealth.gameObject.SetActive(isActive);
    }
    protected void ResetNames()
    {
        m_buildingText.text = "Barracks";
        m_soldierText.text = "Production";
    }
    protected void CloseCanSpawnSoldierArea()
    {
        m_soldierSpawnerButtonHandler.enabled = false;
    }
    protected void OpenCanSpawnSoldierArea()
    {
        m_soldierHealthText.transform.parent.localPosition = Vector3.zero;

        //Pick Random Soldiers For Wiew
        Structs.SoldierStruct soldierData = m_soldierDatas[Random.Range(0, m_soldierDatas.Length)].GetSoldierData();

        m_soldierText.text = "Spawn Soldier!";
        m_soldierImage.sprite = soldierData.soldierSprite;
        m_soldierImage.color = Color.white;

        m_soldierHealthText.gameObject.SetActive(false);
        m_soldierDamageText.gameObject.SetActive(false);
        m_soldierImage.gameObject.SetActive(true);
        m_soldierText.gameObject.SetActive(true);

        m_soldierSpawnerButtonHandler.enabled = true;
    }
}
