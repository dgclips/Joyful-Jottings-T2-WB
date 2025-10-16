using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ULDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
   public CanvasGroup canvasGroup;
   Vector2 _anchorMin;
   Vector2 _anchorMax;
   Vector2 _localPos;
   RectTransform _rectTransform;
   public string target1;
   public string target2;
   public bool IsSnap;                // made public for easier debugging (optional)
   private Vector2 offset;

   void Awake()
   {
      _rectTransform = (RectTransform)transform;
      _anchorMax = _rectTransform.anchorMax;
      _anchorMin = _rectTransform.anchorMin;
      _localPos = _rectTransform.anchoredPosition;
   }

   public void SetAnchorMax(Vector2 max) => _rectTransform.anchorMax = max;
   public void SetAnchorMin(Vector2 min) => _rectTransform.anchorMin = min;
   public void SetLocalPos(Vector2 pos) => _rectTransform.anchoredPosition = pos;

   // FIXED: compare to both targets correctly
   public bool CheckTarget(string img, string img2)
   {
      if (img == target1 || img == target2 || img2 == target1 || img2 == target2)
      {
         IsSnap = true;
         return true;
      }
      return false;
   }

   public void OnPointerDown(PointerEventData eventData)
   {
      AudioManager.audioManager.Play("click");
   }

   public void OnBeginDrag(PointerEventData eventData)
   {
      if (!IsSnap)
      {
         // use the event camera (works for ScreenSpace-Camera and World canvases)
         RectTransformUtility.ScreenPointToLocalPointInRectangle(
             transform.parent as RectTransform,
             eventData.position,
             eventData.pressEventCamera,
             out Vector2 localPointerPosition);

         offset = _rectTransform.anchoredPosition - localPointerPosition;
         if (canvasGroup != null) canvasGroup.blocksRaycasts = false;
      }
   }

   public void OnDrag(PointerEventData eventData)
   {
      if (!IsSnap)
      {
         RectTransformUtility.ScreenPointToLocalPointInRectangle(
             transform.parent as RectTransform,
             eventData.position,
             eventData.pressEventCamera,
             out Vector2 localPointerPosition);

         _rectTransform.anchoredPosition = localPointerPosition + offset;
      }
   }

   public void OnEndDrag(PointerEventData eventData)
   {
      if (canvasGroup != null) canvasGroup.blocksRaycasts = true;

      if (IsSnap)
         return;

      // MANUAL overlap-check: find all ULDrop instances and ask them to accept this drag
      ULDrop[] drops = FindObjectsOfType<ULDrop>();
      foreach (var d in drops)
      {
         if (d.TryReceive(this))
         {
            // accepted & snapped - Stop checking others
            return;
         }
      }

      // nothing accepted it -> reset
      Reset();
   }

   public void Reset()
   {
      if (_rectTransform == null) return;
      _rectTransform.anchorMax = _anchorMax;
      _rectTransform.anchorMin = _anchorMin;
      _rectTransform.anchoredPosition = _localPos;
      IsSnap = false;
   }

   private void OnEnable()
   {
      //Reset(); // optional
   }

   public void OnPointerUp(PointerEventData eventData)
   {
      // no-op (we handle drop in OnEndDrag)
   }
}
