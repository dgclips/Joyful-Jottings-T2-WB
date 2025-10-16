using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleAndName : MonoBehaviour
{
   public int totalItem;
    int count;
   int nameCount;
    public ButtonName[] buttons;

    void Start()
    {
        foreach (ButtonName btnData in buttons)
        {
            //btnData.image.gameObject.SetActive(false); 
            btnData.button.onClick.AddListener(() => OnButtonClick(btnData));
            btnData.field.onValueChanged.AddListener(CheckName);
      }
    }
   void CheckName(string value)
   {
      nameCount = 0;
      foreach (ButtonName btnData in buttons)
      {

         if (btnData.field.text.ToLower().Replace(" ", "") == btnData.name.ToLower() && btnData.name!=string.Empty)
         {
            nameCount++;
         }
      }
      if ((count == totalItem) && (nameCount == totalItem))
      {
         EventManager.GameComplete();
      }
   }
   void OnButtonClick(ButtonName btnData)
    {
        if (btnData.isCorrect)
        {
            count++;
           // Debug.Log(count);
            btnData.image.enabled = true;
            btnData.button.interactable = false; 
            foreach(ButtonName but in buttons)
            {
                if(!but.isCorrect)
                {
                    but.image.enabled = false;
                }
            }
         if ((count == totalItem) && (nameCount == totalItem))
         {
            EventManager.GameComplete();
         }
      }
        else
        {
            btnData.image.enabled = true ;
        }
    }

    public void Reset()
    {
        count = 0;
      nameCount = 0;

        foreach (ButtonName but in buttons)
        {
            but.image.enabled=false;
            but.button.interactable = true;
         but.field.text = string.Empty;
        }
    }
    private void OnEnable()
    {
        Reset();
    }
}

[System.Serializable]
public class ButtonName
{
   public Button button;
   public Image image;
   public bool isCorrect;
   public string name;
   public InputField field;
}
