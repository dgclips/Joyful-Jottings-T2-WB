using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawContinous : MonoBehaviour
{
    public float lineWidth = 0.1f;
    public Material lineMaterial;

    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();
    private List<Transform> ouline;
    public GameObject lineParent;
    public List<ColorDot> colorDot;
    Color _currentColor = Color.black;
    [System.Serializable]
    public class ColorDot
    {
        public Button button;
        public Color color;
    }
    
    private void Start()
    {
        foreach (var p in colorDot)
        {
            p.button.onClick.AddListener(()=>SetColor(p.color));
        }
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            CreateNewLine();
            AddPoint(GetWorldPoint());
        }
        else if (Input.GetMouseButton(0)) 
        {
            Vector3 point = GetWorldPoint();
            if (Vector3.Distance(point, points[points.Count - 1]) > 0.01f)
                AddPoint(point);
        }
    }

    void CreateNewLine()
    {
        GameObject lineObj = new GameObject("Line");
        currentLine = lineObj.AddComponent<LineRenderer>();
        currentLine.transform.SetParent(lineParent.transform);
        currentLine.positionCount = 0;
        currentLine.startColor = _currentColor;
        currentLine.endColor = _currentColor;
        currentLine.material = lineMaterial;
        currentLine.widthMultiplier = lineWidth;
        currentLine.useWorldSpace = true;
        currentLine.sortingOrder = 1;
        currentLine.numCapVertices = 10; // smooth line edges
        points.Clear();
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);
        currentLine.positionCount = points.Count;
        currentLine.SetPositions(points.ToArray());

    }

    Vector3 GetWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Distance from the camera
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void Reset()
    {
       for(int i=0;i<lineParent.transform.childCount;i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }
    }
    private void OnEnable()
    {
        Reset();
    }
}
