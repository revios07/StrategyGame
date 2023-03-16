using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

[SelectionBase]
public class SelectedObjectAssigner : MonoBehaviour
{
    public static SelectedObjectAssigner instance;
    [SerializeField]
    private GameObjectAssignerDataSO _objectAssignerData;

    private bool _isFollowing;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Image[] _images;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _images = GetComponentsInChildren<Image>();

        StopAssign();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void LateUpdate()
    {

    }

    public void StartAssign(Transform assignTransform)
    {
        _transform.parent = assignTransform.GetChild(0);
        _transform.localPosition = Vector3.zero;

        //Set Local Position and Scale
        ObjectType objectType = assignTransform.GetComponent<SelectableAbstract>().objectType;
        _objectAssignerData.GetVectors(out Vector3 localDiff, out Vector3 assignerPosDiff, objectType);

        _transform.localScale = localDiff;
        _transform.localPosition -= assignerPosDiff;
        //----

        _spriteRenderer.enabled = false;
        gameObject.SetActive(true);
    }
    public void ControllIsAssigned(Transform controllAssignedTransform)
    {
        if (_transform.parent != null)
        {
            if (Transform.Equals(_transform.parent.GetInstanceID(), controllAssignedTransform.GetChild(0).GetInstanceID()))
            {
                StopAssign();
            }
        }
    }
    public void StopAssign()
    {
        if (_transform.parent != null)
            _transform.parent = null;

        _spriteRenderer.enabled = false;
        gameObject.SetActive(false);
    }
}
