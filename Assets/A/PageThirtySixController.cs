using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageThirtySixController : MonoBehaviour
{
   [SerializeField] List<ColorChange> images;
   [SerializeField]Color _currentColor;
   public Color lightBlue;
   [SerializeField] List<ColorButton> colors;
   public bool IsNotAlpha;
   [SerializeField]int counter=0;
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

         if (i == counter)
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
