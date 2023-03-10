using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollCheck : MonoBehaviour
{
    private List<Transform> _scrollList = new List<Transform>();
    private Transform _mediumShowScrollableObject;
    private Vector3 _mediumObjectStartPosition;
    private int _mediumObjectIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            _scrollList.Add(transform.GetChild(i));
        }

        _mediumShowScrollableObject = _scrollList[_scrollList.Count / 2];
        _mediumObjectStartPosition = _mediumShowScrollableObject.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _scrollList.Count; ++i)
        {
            if(_mediumShowScrollableObject.localPosition.y > _mediumObjectStartPosition.y + 250f || _mediumShowScrollableObject.localPosition.y < _mediumObjectStartPosition.y - 250f)
            {
                //Reverse Positions

            }
        }
    }
}
