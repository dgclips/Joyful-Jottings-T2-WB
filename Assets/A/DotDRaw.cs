using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DotDRaw : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _detectionLayer;

    [SerializeField] RectTransform img1;
    [SerializeField] RectTransform img2;
    private List<Vector3> _drawnPoints = new List<Vector3>();
    private bool _isDrawing = false;
    [SerializeField] private float _lineWidth = 0.1f;
    bool _isComplete = false;
    public List<RaycastResult> results = new List<RaycastResult>();
    public bool isDrawAndPaint;
    private void Start()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        _lineRenderer.numCapVertices = 10;
        _lineRenderer.numCornerVertices = 5;
        _lineRenderer.sortingOrder = 2;

    }

    private void Update()
    {
        if (_isDrawing && _drawnPoints.Count > 1)
        {
            _lineRenderer.positionCount = _drawnPoints.Count;
            _lineRenderer.SetPositions(_drawnPoints.ToArray());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        if (!_isDrawing && !_isComplete)
        {
            AudioManager.audioManager.Play("click");
            _drawnPoints.Clear();
            _isDrawing = true;
            AddPoint(eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDrawing)
        {
            AddPoint(eventData.position);

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDrawing)
        {
            AudioManager.audioManager.Play("click");
            _isDrawing = false;
            DetectEndObject();
        }
    }

    private void AddPoint(Vector2 screenPos)
    {
        Vector3 worldPos = GetWorldPosition(screenPos);
        if (_drawnPoints.Count == 0 || Vector3.Distance(_drawnPoints[_drawnPoints.Count - 1], worldPos) > 0.1f)
        {
            _drawnPoints.Add(worldPos);
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
        worldPos.z = 0;
        return worldPos;
    }
    public void ResetLine()
    {
        _drawnPoints.Clear();
        _isDrawing = false;
        _isComplete = false;
        _lineRenderer.positionCount = 0;
    }
    private void OnEnable()
    {
        ResetLine();
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

            if (result.gameObject == img2.gameObject) // Ensure it's a UI element
            {
                DrawController.count++;
                _isDrawing = true;
                DrawAndPaint.count++;
             //   UpdateLineToConnectedObject();
                if (DrawAndPaint.count == DrawAndPaint.totalCount-1)
                {
                  EventManager.GameComplete();
                }
                return;
            }
        }
        _lineRenderer.positionCount = 0;
    }



    //private void UpdateLineToConnectedObject()
    //{
    //    if (_connectedObject != null)
    //    {
    //        _endPos = _connectedObject.position; // Snap line end to detected object
    //        _lineRenderer.SetPosition(1, _endPos);
    //        // UpdateCollider();
    //    }
    //}
}
