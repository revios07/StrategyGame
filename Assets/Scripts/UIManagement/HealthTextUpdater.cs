using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextUpdater : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _healthText;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        WriteHealth(10);
    }

    private void Start()
    {
        WriteHealth(10);
    }

    public void WriteHealth(float health)
    {
        _healthText.text = "Health => " + _slider.value;
    }
}
