using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawAndPaint : MonoBehaviour
{
    public static DrawAndPaint instance;
    [SerializeField] List<DotDRaw> checkObjects;
    public static int count;
    public static int totalCount;
    public int tot;
    private Color _currentColor =Color.black;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalCount = tot;
    }


   
    public void ResetGame()
    {
        count = 0;
        foreach (var c in checkObjects)
        {
            c.ResetLine();
        }
    }

    void OnEnable()
    {
        ResetGame();
    }
}
