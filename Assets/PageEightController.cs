using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageEightController : MonoBehaviour
{

    [SerializeField]List<ColorImage> pictures;
    [SerializeField] List<Colors> colorButtons;
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    public Color _currentColor;
    int count;
    bool isCircle1;
    bool isCircle2;
    public int totalCount;
    int i = 0;
    [System.Serializable]
    public class ColorImage
    {
        public Button button;
        public Image image;
    }

    [System.Serializable]
    public class Colors
    {
        public Button button;
        public Color color;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var image in pictures)
        {
            image.image.alphaHitTestMinimumThreshold = 0.1f;
            image.button.onClick.AddListener(()=>ApplyColor(image.image));
        }

        foreach(var col in colorButtons)
        {
            col.button.onClick.AddListener(()=>SetColor(col.color));
        }
    }


    public void SetColor(Color col)
    {
        _currentColor = col;
    }

    public void ApplyColor(Image img)
    {
        img.color = _currentColor;
        CheckWhite();
    }
    

    public void CircleImage1()
    {
        img1.enabled = true;
        if (!isCircle1) {count++;}
        isCircle1 = true;
        if (i == pictures.Count && count == totalCount)
        {
            EventManager.GameComplete();
        }
    }
    public void CircleImage2()
    {
        img2.enabled = true;
        if (!isCircle2) { count++; }
        isCircle2 = true;
        if (i == pictures.Count && count == totalCount)
        {
            EventManager.GameComplete();
        }
    }
    public void CheckWhite()
    {
         i = 0;
        foreach (var image in pictures)
        {
            if (image.image.color != Color.white)
            {
                i++;
               
            }

            if (i == pictures.Count && count == totalCount)
            {
                EventManager.GameComplete();
            }
        }
    }
    public void Reset()
    {
        count = 0;
        img1.enabled=false;
        img2.enabled=false;
        foreach (var image in pictures)
        {

            image.image.color = Color.white;
        }
    }

    private void OnEnable()
    {
        Reset();
    }
}
