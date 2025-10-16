using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleted : MonoBehaviour
{
    public GameObject congrateImage;
    public GameObject pageCanvas;
    public GameObject closeButton;
    void OnEnable()
    {
       EventManager.OnComplete += Showed;
    }
    void OnDisable()
    {
       EventManager.OnComplete -= Showed;
    }
    
    void Showed()
    {
        Invoke("Show",1f);
    }

    void Show()
    {
        closeButton.SetActive(false);
        PageController.instance.DisableAllPage();
        congrateImage.SetActive(true);
        AudioManager.audioManager.Play("win");
        //PageController.pageNumber++;
        Invoke("Hide", 5f);
    }

    void Hide()
    {
        congrateImage.SetActive(false);
        PageController.instance.EnableSet();
        pageCanvas.SetActive(true);
    }
}
