using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageEight : MonoBehaviour
{
   [SerializeField] List<ColorChange> images;
   Color _currentColor = Color.white;
   public Color lightBlue;
   public bool IsNotAlpha;


   public void Start()
   {
      foreach (var image in images)
      {
         if (!IsNotAlpha)
         {
            image.img.alphaHitTestMinimumThreshold = 0.1f;
         }
         image.button.onClick.AddListener(() => ColorImage(image.img, image.button));
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
         }

         if (i == images.Count)
         {
            EventManager.GameComplete();
         }
      }
   }
   public void ColorImage(Image img,Button button)
   {
      AudioManager.audioManager.Play("click");
      img.color = button.colors.selectedColor;
      CheckWhite();
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

