using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSeventyTwoController : MonoBehaviour
{
   [SerializeField] List<ColorButton> buttonColors;
   Color _currentColor;
   int _blueCount = 0;
   int _yellowCount = 0;
   int _redCount = 0;
   int _blackCount = 0;

   public int totalBlue=0;
   public int totalRed=0;
   public int totalYellow=0;
   public int totalBlack = 0;


   public string item1;
   public string item2;
   public string item3;
   public string item4;

   [System.Serializable]
   public class ColorButton
   {
      public Color color;
      public Button button;
   }

   [SerializeField] List<Numbers> numbers;

   [System.Serializable]
   public class Numbers
   {
      public Button button;
      public Image image;
   }

   private void Start()
   {
      _currentColor = buttonColors[0].color;
      foreach (var col in buttonColors)
      {
         col.button.onClick.AddListener(() => SetColor(col.color));
      }
      foreach (var num in numbers)
      {
         num.button.onClick.AddListener(() => ColorNumber(num.button.name, num.button.image));
      }
   }

   void SetColor(Color color)
   {
      _currentColor = color;
   }

   void ColorNumber(string name, Image img)
   {
      if (name == item1)
      {
         if (_currentColor == buttonColors[0].color)
         {
            _redCount++;
         }
         img.color = _currentColor;
      }
      else if (name == item2)
      {
         if (_currentColor == buttonColors[1].color)
         {
            _blueCount++;
         }
         img.color = _currentColor;
      }
      else if (name == item3)
      {
         if (_currentColor == buttonColors[2].color)
         {
            _blackCount++;
         }
         img.color = _currentColor;
      }
      else if (name == item4)
      {
         if (_currentColor == buttonColors[3].color)
         {
            _yellowCount++;
         }
         img.color = _currentColor;
      }
      Debug.Log("sdfe");
      if (_blueCount == totalBlue && _redCount == totalRed && _yellowCount == totalYellow && _blackCount == totalBlack)
      {
         EventManager.GameComplete();
      }
   }

   public void Reset()
   {
      foreach (var num in numbers)
      {
         num.image.color = Color.white;
      }
      _blueCount = 0;
      _yellowCount = 0;
      _redCount = 0;
      _blackCount = 0;
   }
   private void OnEnable()
   {
      Reset();
   }
}
