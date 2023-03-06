using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuInfiniteScroll : MonoBehaviour
{
    [SerializeField]
    private InputData _inputData;
    [SerializeField]
    private GameObject _scrollableAreaPrefab;

    [SerializeField]
    private List<Transform> _childTransforms = new List<Transform>();
    private static List<Transform> _scrollablePool = new List<Transform>();

    private float _startPositionY;

    //[NaughtyAttributes.Button()]
    [ExecuteAlways]
    public void CreatePool()
    {
        return;
        ClearPool();
        if (_scrollablePool.Count == 0 && GameObject.FindObjectsOfType<ProductionMenuInfiniteScroll>().Length < 3)
        {
            for (int i = 0; i < 2f; ++i)
            {
                GameObject clone = GameObject.Instantiate(_scrollableAreaPrefab);
                _scrollablePool.Add(clone.transform);
                clone.transform.SetParent(transform.parent);

                float positionOfClone = transform.position.y + 100f;
                clone.GetComponent<ProductionMenuInfiniteScroll>().SetStartPosition(positionOfClone, transform.GetComponent<RectTransform>().localPosition);
            }
        }
    }

    //[NaughtyAttributes.Button()]
    [ExecuteAlways]
    public void ClearPool()
    {
        foreach (var pooledObject in _scrollablePool)
        {
            DestroyImmediate(pooledObject.gameObject);
        }
        _scrollablePool.Clear();
    }

    private void Awake()
    {
        _startPositionY = transform.position.y;

        _scrollablePool.Add(this.transform);

        CreatePool();

        for (int i = 0; i < transform.childCount; ++i)
        {
            _childTransforms.Add(transform.GetChild(0));
        }
    }

    private void Update()
    {
        //Scroll Wheel Moves
        if (_inputData.GetMousePosition().x < Screen.width / 3f && Input.mouseScrollDelta.y != 0f)
        {
            Vector3 position = transform.position;
            position.y += Input.mouseScrollDelta.y * Time.deltaTime * _inputData.GetScrollSpeed();

            transform.position = position;
        }
    }

    public void SetStartPosition(float yPosition, Vector3 defaultPosition)
    {
        defaultPosition.y = yPosition;

        transform.GetComponent<RectTransform>().localPosition = defaultPosition;
        transform.localScale = Vector3.one;
    }
}
