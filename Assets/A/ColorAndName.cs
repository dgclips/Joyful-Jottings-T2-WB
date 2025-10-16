using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAndName : MonoBehaviour
{
   [SerializeField] List<ColorChange> images;
   Color _currentColor = Color.yellow;
   public Color lightBlue;
   [SerializeField] List<ColorButton> colors;
   public bool IsNotAlpha;

 

   [System.Serializable]
   public class ColorChange
   {
      public Image img;
      public Button button;    
   }
   int count = 0;
   int imageCount = 0;
   public string name;
   public InputField field;
   [System.Serializable]
   public class ColorButton
   {
      public Color color;
      public Button button;
   }

   public void Start()
   {
      field.onValueChanged.AddListener(CheckName);
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

   void CheckName(string value)
   {
      count = 0;
         if (field.text.ToLower().Replace(" ", "") == name.ToLower())
         {
            count++;
      }

      if ((imageCount == images.Count) && (count == 1))
      {
         EventManager.GameComplete();
      }
   }

   public void CheckWhite()
   {
       imageCount = 0;
      foreach (var image in images)
      {
         if (image.img.color != Color.white)
         {
            imageCount++;
         }

         if ((imageCount == images.Count)&&(count == 1))
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
      field.text = string.Empty;
   }
   private void OnEnable()
   {
      Reset();
   }
}
