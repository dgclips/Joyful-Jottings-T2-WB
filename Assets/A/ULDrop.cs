using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ULDrop : MonoBehaviour
{
   public string img1;
   public string img2;

   // Called by ULDrag when the drag ends. Returns true if the drop was accepted.
   public bool TryReceive(ULDrag drag)
   {
      if (drag == null) return false;

      RectTransform dragRect = drag.GetComponent<RectTransform>();
      RectTransform dropRect = GetComponent<RectTransform>();

      if (IsOverlapping(dragRect, dropRect))
      {
         if (drag.CheckTarget(img1, img2))
         {
            AudioManager.audioManager.Play("click");
            PageFiftyController.count++;

            // Snap the dragged item to this drop slot
            drag.SetAnchorMax(dropRect.anchorMax);
            drag.SetAnchorMin(dropRect.anchorMin);
            drag.SetLocalPos(dropRect.anchoredPosition);

            if (PageFiftyController.count == PageFiftyController.totalCount)
            {
               EventManager.GameComplete();
            }

            return true;
         }
      }

      return false;
   }

   // Helper: world-space rectangle overlap
   private bool IsOverlapping(RectTransform a, RectTransform b)
   {
      Rect ra = GetWorldRect(a);
      Rect rb = GetWorldRect(b);
      return ra.Overlaps(rb);
   }

   private Rect GetWorldRect(RectTransform rt)
   {
      Vector3[] corners = new Vector3[4];
      rt.GetWorldCorners(corners);
      Vector3 min = corners[0];
      Vector3 max = corners[2];
      return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
   }
}
