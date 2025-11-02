using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAndType : MonoBehaviour
{
   public ButtonData[] buttons;
   public NameClimate[] nameClimates;
   public int totalCount;
   public int circleCount;
   int count = 0;
   int nameCount;
   void Start()
   {
      foreach (ButtonData btnData in buttons)
      {
         //btnData.image.gameObject.SetActive(false); 
         btnData.button.onClick.AddListener(() => OnButtonClick(btnData));
      }
      foreach (NameClimate nameClimate in nameClimates)
      {
         nameClimate.inputFields.textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
         nameClimate.inputFields.onValueChanged.AddListener(CheckName);
      }
   }
   void CheckName(string value)
   {
      nameCount = 0;
      foreach (NameClimate nameClimate in nameClimates)
      {

         if (nameClimate.inputFields.text.ToLower().Replace(" ", "") == nameClimate.name.ToLower() && nameClimate.name != string.Empty)
         {
            nameCount++;
         }
      }
      if ((nameCount + count) == totalCount)
      {
         EventManager.GameComplete();
      }
   }
   void OnButtonClick(ButtonData btnData)
   {
      if (btnData.isCorrect)
      {
         count++;
         // Debug.Log(count);
         btnData.image.enabled = true;
         btnData.button.interactable = false;
         foreach (ButtonData but in buttons)
         {
            if (!but.isCorrect)
            {
               but.image.enabled = false;
            }
         }
         if(count == circleCount)
         {
            foreach (ButtonData btnDatas in buttons)
            {
               //btnData.image.gameObject.SetActive(false); 
               btnDatas.button.interactable = false;
            }
         }

         if ((nameCount + count) == totalCount)
         {
            EventManager.GameComplete();
         }
      }
      else
      {
         btnData.image.enabled = true;
      }
   }

   public void Reset()
   {
      count = 0;

      foreach (ButtonData but in buttons)
      {
         but.image.enabled = false;
         but.button.interactable = true;
      }
      foreach (NameClimate nameClimate in nameClimates)
      {
         nameClimate.inputFields.text = string.Empty;
      }
   }
   private void OnEnable()
   {
      Reset();
   }

}
