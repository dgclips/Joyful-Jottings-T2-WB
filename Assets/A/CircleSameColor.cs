using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSameColor : MonoBehaviour
{
    

    public int totalItem;
    int count;
    public ButtonData[] buttons;

    void Start()
    {
        foreach (ButtonData btnData in buttons)
        {
            //btnData.image.gameObject.SetActive(false); 
            btnData.button.onClick.AddListener(() => OnButtonClick(btnData));
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
            foreach(ButtonData but in buttons)
            {
                if(!but.isCorrect)
                {
                    but.image.enabled = false;
                }
            }
            if(count==totalItem)
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

        foreach (ButtonData but in buttons)
        {
            but.image.enabled=false;
            but.button.interactable = true;
        }
    }
    private void OnEnable()
    {
        Reset();
    }
}

[System.Serializable]
public class ButtonData
{
   public Button button;
   public Image image;
   public bool isCorrect;
}
