using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPositionUpdater : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1000f)]
    private float _yPositionDiff;
    [SerializeField]
    private bool _isPositionsOk;
    private Transform[] _childs;
    [Tooltip("Open Mask On GamePlay")]
    [SerializeField]
    private bool _isMaskOpenScroll;

    private void Awake()
    {
        if (!_isPositionsOk)
            SetPositions();

        if (_isMaskOpenScroll)
            GetComponent<Mask>().enabled = true;
    }

    [NaughtyAttributes.Button()]
    private void SetPositions()
    {
        GetChildReferances(ref _childs);

        Vector3 upperChildPosition = _childs[0].position;
        upperChildPosition.y = _yPositionDiff;

        _childs[0].position = upperChildPosition;

        for (int i = 1; i < _childs.Length; ++i)
        {
            //Upper Child
            if (_childs[i] == transform.GetChild(0))
                continue;

            //Medium Child and Lower Child
            _childs[i].localPosition = transform.GetChild(i - 1).localPosition+ Vector3.down * _yPositionDiff;
        }
    }

    private void GetChildReferances(ref Transform[] transforms)
    {
        transforms = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; ++i)
        {
            transforms[i] = transform.GetChild(i);
        }
    }
}
