using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountAnCircle : MonoBehaviour
{

   [SerializeField] ButtonData[] _circles;
   int count;
   [SerializeField] int _totalCount;
   [SerializeField]CountCircleController _controller;
   Color defualtColor = new Color(0, 0, 0, 0);
   private void Start()
   {
      foreach (var circle in _circles)
      {
         circle.button.onClick.AddListener(()=> SelectIt(circle));
      }
   }

   void SelectIt(ButtonData buttonData)
   {
      if(buttonData.image.color.a==0) {
         
       buttonData.image.color = Color.green;
         count++;
      }else
      {
         count--;
         buttonData.image.color = defualtColor;
      }
      _controller.OverAllCount();
   }

   public bool Check()
   {
      bool a;
      if(count == _totalCount)
         a=true;
        else
        {
            a=false;
        }

        return a;
   }
    
   public void Reset()
   {
      count = 0;
      foreach (var circle in _circles)
      {
         circle.image.color = defualtColor;
      }
   }

}


