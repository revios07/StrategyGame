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
        renderers[0] = null;

        foreach(var renderer in renderers)
        {
            if (renderer == null)
                continue;

            renderer.sprite = _sprite;
        }
    }
}
