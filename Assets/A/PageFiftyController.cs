using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFiftyController : MonoBehaviour
{
    public static int count;
    public static int totalCount;
    public int totalItem;
    [SerializeField] List<ULDrag> dragItems;

    private void Start()
    {

    }
    public void Reset()
    {
        foreach (ULDrag drag in dragItems)
        {

            drag.Reset();
            count = 0;
        }
    }
    private void OnEnable()
    {
        count = 0;
        totalCount = totalItem;
        Reset();
    }
}
