using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    [SerializeField] List<CircleObject> _circleObjects;
    public static int totalItem = 0;
    public static int count = 0;
    public int total;

    private void Start()
    {

    }

    public void ResetGame()
    {
        foreach (var c in _circleObjects)
        {
            c.Reset();
        }
    }

    private void OnEnable()
    {
        count = 0;
        totalItem = total;
        ResetGame();
    }
}
