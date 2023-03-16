using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuInfiniteScroll : MonoBehaviour
{
    private bool _canChange;
    [Tooltip("Diff between Lower and Upper Gameobject")]
    [SerializeField]
    private float _height, _movementUpperToLower;
    private float _startPositionY;

    [SerializeField]
    private InputDataSO _inputData;
    [SerializeField]
    private GameObject _scrollableAreaPrefab;

    [NaughtyAttributes.ShowNonSerializedField]
    private List<Transform> _childTransforms = new List<Transform>();
    private static List<Transform> _scrollablePool = new List<Transform>();

    private void Awake()
    {
        _startPositionY = 0f;
        _scrollablePool.Add(this.transform);

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

        if(transform.localPosition.y > _height * 2f + _startPositionY && Input.mouseScrollDelta.y > 0f)
        {
            Reposition(Vector2.down);
        }
        if(transform.localPosition.y < -_height * 2f + _startPositionY && Input.mouseScrollDelta.y < 0f)
        {
            Reposition(Vector2.up);
        }
    }

    private void Reposition(Vector2 directionVector)
    {
        Debug.Log("Reposition");
        Vector3 vector = directionVector * _movementUpperToLower;

        transform.localPosition += vector;
    }

    [ExecuteAlways]
    public void CreatePool()
    {
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

    [ExecuteAlways]
    public void ClearPool()
    {
        foreach (var pooledObject in _scrollablePool)
        {
            DestroyImmediate(pooledObject.gameObject);
        }
        _scrollablePool.Clear();
    }

    public void SetStartPosition(float yPosition, Vector3 defaultPosition)
    {
        defaultPosition.y = yPosition;

        transform.GetComponent<RectTransform>().localPosition = defaultPosition;
        transform.localScale = Vector3.one;
    }
}
