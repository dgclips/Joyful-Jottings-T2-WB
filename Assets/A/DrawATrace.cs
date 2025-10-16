using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawATrace : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _detectionLayer;

    [SerializeField] RectTransform img1;
    [SerializeField] RectTransform img2;
    [SerializeField] RectTransform img3;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Transform _connectedObject;
    private bool _isDrawing = false;

    public Image circleImage;

    private void Start()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }
    private void Update()
    {
        if (_connectedObject != null) // Only update if line is connected
        {
            Vector3 worldPos1 = ConvertToWorldPosition(img1);
            Vector3 worldPos2 = ConvertToWorldPosition(img2);
            Vector3 worldPos3 = ConvertToWorldPosition(img3);

            _lineRenderer.SetPosition(0, worldPos1);
            if (_connectedObject == img2)
            {
                img2.GetComponent<Image>().raycastTarget = false;
                _lineRenderer.SetPosition(1, worldPos2);
            }
            if (_connectedObject == img3) {
              //  img3.GetComponent<Image>().raycastTarget = false;
                _lineRenderer.SetPosition(1, worldPos3); }

            //UpdateCollider(); // Ensure collider updates as well
        }
    }

    Vector3 ConvertToWorldPosition(RectTransform rectTransform)
    {
        Vector3 screenPos = rectTransform.position; // UI elements are in screen space
        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x + 1, screenPos.y, _mainCamera.nearClipPlane + 10f));
        worldPos.z = 0; // Keep it in 2D
        return worldPos;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isDrawing)
        {
            AudioManager.audioManager.Play("click");
            _startPos = GetWorldPosition(img1.position);
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _startPos);
            _lineRenderer.SetPosition(1, _startPos);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDrawing)
        {
            _endPos = GetWorldPosition(eventData.position);
            _lineRenderer.SetPosition(1, _endPos);
            // UpdateCollider();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isDrawing)
        {
            AudioManager.audioManager.Play("click");
            if (img2 != null || img3 !=null)
            {
                DetectEndObject();
            }
            else
            {
                _lineRenderer.positionCount = 0;
            }
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
        worldPos.z = 0; // Keep in 2D space
        return worldPos;
    }

    private void DetectEndObject()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition // Use actual screen position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == img2.gameObject || result.gameObject == img3.gameObject) // Ensure it's a UI element
            {
                TraceAShapeController.count++;
                _isDrawing = true;
                _connectedObject = result.gameObject.transform;
                UpdateLineToConnectedObject();
                circleImage.enabled = false;
                if (TraceAShapeController.count == TraceAShapeController.totalCount)
                {
                    EventManager.GameComplete();
                }
                return;
            }
        }
        _lineRenderer.positionCount = 0;
    }



    private void UpdateLineToConnectedObject()
    {
        if (_connectedObject != null)
        {
            _endPos = _connectedObject.position; // Snap line end to detected object
            _lineRenderer.SetPosition(1, _endPos);
            // UpdateCollider();
        }
    }

    public void ResetLine()
    {
        img2.GetComponent<Image>().raycastTarget = true;
        img3.GetComponent<Image>().raycastTarget = true;
        circleImage.enabled = true;
        _connectedObject = null;
        _isDrawing = false;
        _lineRenderer.positionCount = 0;

    }
    private void OnEnable()
    {
        ResetLine();
    }

}
