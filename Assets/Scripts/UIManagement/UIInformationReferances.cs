using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;

public abstract class UIInformationReferances : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Names")]
    protected TMP_Text m_buildingText, m_soldierText;
    [BoxGroup("Images")]
    [SerializeField]
    protected Image m_buidingImage, m_soldierImage;
    [SerializeField]
    [BoxGroup("Soldiers Health-Damage")]
    protected TMP_Text m_soldierHealthText, m_soldierDamageText;
    [SerializeField]
    [BoxGroup("Building Health")]
    protected TMP_Text m_buildingHealth;

    [SerializeField]
    protected Sprite m_emptySprite;

    protected void CloseHealthTextes()
    {
        m_soldierHealthText.gameObject.SetActive(false);
        m_soldierDamageText.gameObject.SetActive(false);
        m_buildingHealth.gameObject.SetActive(false);
    }

    protected void ResetNames()
    {
        m_buildingText.text = "Barracks";
        m_soldierText.text = "Production";
    }
}
