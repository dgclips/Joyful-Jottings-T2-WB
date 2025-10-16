using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePicture : MonoBehaviour
{
   int nameCount;
   int count;

   

   public NameClimate[] nameClimates;
   void Start()
   {
      foreach (NameClimate nameClimate in nameClimates)
      {nameClimate.inputFields.textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
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
      if ((nameCount == nameClimates.Length))
      {
         EventManager.GameComplete();
      }
   }


   public void Reset()
   {
      nameCount = 0;
      count = 0;
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

[System.Serializable]
public class NameClimate
{
   public InputField inputFields;
   public string name;
}
