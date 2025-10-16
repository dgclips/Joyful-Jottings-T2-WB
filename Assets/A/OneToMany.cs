using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OneToMany : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Setup")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform img1; // Starting object
    [SerializeField] private List<RectTransform> targetImages; // Target objects
    [SerializeField] private LineRenderer lineRendererPrefab;

    private LineRenderer currentLine;
    private List<LineRenderer> activeLines = new();
    private List<RectTransform> connectedTargets = new();

    private bool isDragging = false;
    private Vector3 startPos, endPos;

    [Header("Progress Tracking")]
    public int count = 0;
    public int totalCount = 0;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        // Keep all lines attached to img1
        foreach (var line in activeLines)
        {
            if (line != null)
            {
                line.SetPosition(0, ConvertToWorldPosition(img1));
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (count >= totalCount)
            return;

        AudioManager.audioManager?.Play("click");

        startPos = ConvertToWorldPosition(img1);
        currentLine = Instantiate(lineRendererPrefab, transform);
        currentLine.useWorldSpace = true;
        currentLine.positionCount = 2;
        currentLine.startWidth = 0.1f;
        currentLine.endWidth = 0.1f;
        currentLine.material = new Material(Shader.Find("Sprites/Default"));
        currentLine.startColor = Color.green;
        currentLine.endColor = Color.green;
        currentLine.SetPosition(0, startPos);
        currentLine.SetPosition(1, startPos);

        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && currentLine != null)
        {
            endPos = GetWorldPosition(eventData.position);
            currentLine.SetPosition(1, endPos);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging || currentLine == null)
            return;

        AudioManager.audioManager?.Play("click");

        TryConnectToTarget();
        isDragging = false;
    }

    private void TryConnectToTarget()
    {
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            foreach (var target in targetImages)
            {
                if (result.gameObject == target.gameObject && !connectedTargets.Contains(target))
                {
                    connectedTargets.Add(target);
                    activeLines.Add(currentLine);
                    UpdateLineToTarget(currentLine, target);
                    count++;

                    if (count == totalCount)
                        EventManager.GameComplete();

                    return;
                }
            }
        }

        // No valid match — remove the line
        Destroy(currentLine.gameObject);
    }

    private void UpdateLineToTarget(LineRenderer line, RectTransform target)
    {
        Vector3 endPos = ConvertToWorldPosition(target);
        line.SetPosition(1, endPos);
    }

    private Vector3 ConvertToWorldPosition(RectTransform rect)
    {
        Vector3 screenPos = rect.position;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
        worldPos.z = 0;
        return worldPos;
    }

    private Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
        worldPos.z = 0;
        return worldPos;
    }

    public void ResetLines()
    {
        foreach (var line in activeLines)
        {
            if (line != null)
                Destroy(line.gameObject);
        }

        activeLines.Clear();
        connectedTargets.Clear();
        count = 0;
        isDragging = false;
        currentLine = null;
    }

    private void OnEnable()
    {
        ResetLines();
    }
}
