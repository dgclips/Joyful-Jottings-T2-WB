using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAndDraw : MonoBehaviour
{
   [SerializeField] List<Line> lines;
   public static int count;
   public static int totalCount;
   public int totalItem;

   [SerializeField] List<ColorChange> images;
   Color _currentColor = Color.yellow;
   public Color lightBlue;
   [SerializeField] List<ColorButton> colors;
   public bool IsNotAlpha;
   bool colored;
   public void Start()
   {
      foreach (var image in images)
      {
         if (!IsNotAlpha)
         {
            image.img.alphaHitTestMinimumThreshold = 0.1f;
         }
         image.button.onClick.AddListener(() => ColorImage(image.img));
      }

      foreach (var color in colors)
      {
         color.button.onClick.AddListener(() => Colors(color.color));
      }
   }

   public void CheckWhite()
   {
      int i = 0;
      foreach (var image in images)
      {
         if (image.img.color != Color.white)
         {
            i++;
            if(i==images.Count && !colored)
            {
               colored = true;
               count++;
            }
         }

         if (count == totalCount)
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
   public void Colors(Color color)
   {
      _currentColor = color;
   }

   public void ResetGame()
   {
      foreach (var image in images)
      {
         image.img.color = Color.white;

      }
      foreach (var line in lines)
      {
         line.ResetLine();
      }
      count = 0;
      colored = false;
     // AudioManager.audioManager.Play("click");
   }
   private void OnEnable()
   {
      count = 0;
      totalCount = totalItem;
      ResetGame();
   }
}
