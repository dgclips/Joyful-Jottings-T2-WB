using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageFiftyFour : MonoBehaviour
{
   [SerializeField] ColorItem[] _items;
   [SerializeField] List<ColorButton> colors;
   int counter;
   Color _currentColor = Color.white;
   private void Start()
   {
      foreach (var item in _items)
      {
         item.Image.alphaHitTestMinimumThreshold = 0.1f;
         item.Button.onClick.AddListener(() => ColorLeftHand(item.Image, item.Color, item.Button));

      }
      foreach (var color in colors)
      {
         color.button.onClick.AddListener(() => Colors(color.color));
      }

   }
   public void ColorLeftHand(Image image, Color color, Button button)
   {
      image.color = _currentColor;
      if (image.color == color)
      {
         button.interactable = false;

         counter++;
      }
      AudioManager.audioManager.Play("click");
      CheckColor();
   }

   public void Colors(Color color)
   {
      _currentColor = color;
   }
   public void CheckColor()
   {
      if (counter == _items.Length)
      {
         EventManager.GameComplete();
      }
   }

   public void Reset()
   {
      foreach (var image in _items)
      {
         image.Image.color = Color.white;
         image.Button.interactable = true;

      }
      counter = 0;
   }
   private void OnEnable()
   {
      Reset();
   }
}
