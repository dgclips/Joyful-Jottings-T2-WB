using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSevenController : MonoBehaviour
{

    Color _currentColor = Color.white;
    [SerializeField] ColorItem[] _items;
   int counter;
   [SerializeField] Color _brown;
   [SerializeField] Color _black;
   [SerializeField] Color _red;
    private void Start()
    {
      foreach (var item in _items)
      {
         item.Image.alphaHitTestMinimumThreshold = 0.1f;
         item.Button.onClick.AddListener(() => ColorLeftHand(item.Image,item.Color,item.Button));
         
      }
     
    }
    public void RedButton()
   {
        _currentColor = _brown;
        AudioManager.audioManager.Play("click");
    }

    public void BlueButton()
    {
        _currentColor = _black;
        AudioManager.audioManager.Play("click");
    }
   public void BlackButton()
   {
      _currentColor = _red;
      AudioManager.audioManager.Play("click");
   }

   public void ColorLeftHand(Image image,Color color,Button button)
    {
        image.color = _currentColor;
        if(image.color == color)
       {
         button.interactable = false;

         counter++;
       }
        AudioManager.audioManager.Play("click");
        CheckColor();
    }

   
    public void CheckColor()
    {
        if(counter == _items.Length)
        {
            EventManager.GameComplete();
        }
    }
    public void ResetGame()
   {
      foreach (var item in _items)
      {
         item.Image.color = Color.white;     
         item.Button.interactable = true;
      }
       counter = 0;
        AudioManager.audioManager.Play("click");
    }
    private void OnEnable()
    {
        ResetGame();
    }
}
[System.Serializable]
public class ColorItem
{
   public Image Image;
   public Button Button;
   public Color Color;
}