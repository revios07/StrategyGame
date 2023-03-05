using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SpriteAssigner : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite;

    [Button("Assign Sprites")]
    [ExecuteAlways]
    public void AssignSprites()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        foreach(var renderer in renderers)
        {
            renderer.sprite = _sprite;
        }
    }
}
