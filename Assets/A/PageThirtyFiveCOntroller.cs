using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
public class PageThirtyFiveCOntroller : MonoBehaviour
{
    [SerializeField] List<ColorChange> images;
    Color _currentColor = Color.white;
    public Color lightBlue;
    [SerializeField] List<ColorButton> colors;
    public bool IsNotAlpha;
   

    public void Start()
    {
        foreach (var image in images)
        {
            if (!IsNotAlpha)
            {
                image.img.alphaHitTestMinimumThreshold = 0.1f;
            }
            image.button.onClick.AddListener(() =>ColorImage(image.img));
        }

        foreach (var color in colors)
        {
            color.button.onClick.AddListener(()=> Colors(color.color));
        }
    }

    public void CheckWhite()
    {
        int i = 0;
        foreach(var image in images)
        {  if(image.img.color != Color.white)
            {
                i++;
            }
            
            if(i==images.Count)
            {
                EventManager.GameComplete();
            }
        }
    }
    public void ColorImage(Image img)
    {
        AudioManager.audioManager.Play("click");
        img.color = _currentColor;
        CheckWhite();
    }

    public void RedColor()
    {
        AudioManager.audioManager.Play("click");
        _currentColor = Color.red;
    }
    public void GreenColor()
    {
        AudioManager.audioManager.Play("click");
        _currentColor = Color.green;
    }
    public void BlueColor()
    {
        AudioManager.audioManager.Play("click");
        _currentColor = Color.blue;
    }
    public void YellowColor()
    {
        AudioManager.audioManager.Play("click");
        _currentColor = Color.yellow;
    }
    public void LightBlueColor()
    {
        AudioManager.audioManager.Play("click");
        _currentColor = lightBlue;
    }

    public void Colors(Color color)
    {
        _currentColor = color;
    }

    public void Reset()
    {
        foreach (var image in images)
        {
            image.img.color = Color.white;
          
        }
    }
    private void OnEnable()
    {
        Reset();
    }
}

[System.Serializable]
public class ColorChange
{
   public Image img;
   public Button button;
}

[System.Serializable]
public class ColorButton
{
   public Color color;
   public Button button;
}

