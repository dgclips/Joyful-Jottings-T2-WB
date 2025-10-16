using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCircleController : MonoBehaviour
{
   [SerializeField]CountAnCircle[] circles;
   [SerializeField] int _totalSet;
   int counter;
   public void OverAllCount()
   {
      counter=0;
     
      foreach (var circle in circles)
      {
         if(circle.Check())
         {
            counter++;
         }
      }

      if (_totalSet == counter)
      {
         EventManager.GameComplete();
      }
   }

   public void Reset()
   {
      counter = 0;
      foreach (var circle in circles)
      {
         circle.Reset();
      }
   }
}
