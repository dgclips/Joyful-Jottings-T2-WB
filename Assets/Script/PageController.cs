using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public List<GameObject> pages;
    public List<GameObject> pageSet;
    public static PageController instance;
    public static int pageNumber ;
    int pageSetCount=1;
    public int num;
    public GameObject coverPage;
    public GameObject setCanvas;
    public GameObject closeButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public RandomBackground randomBackground;
    public List<string> overlayCanvas = new List<string>();
    public List<string> cameraCanvas = new List<string>();
    public GameObject overlayCanvasGameObject;
    public GameObject cameraCanvasGameObject;
    public List<GameObject> instantiatedObject = new List<GameObject>();
    private void Awake()
    {
        instance = this;
        pageNumber = num;
    }
    public void StartActivity()
    {
        AudioManager.audioManager.Play("click");
        coverPage.SetActive(false);
        //background.SetActive(true);
        setCanvas.SetActive(true);
        num = 1;
        pageNumber = num;
        EnableSet();
        FullscreenController.instance.GoFullscreen();
        //EnablePage();
    }


    public void DisableAllPage()
    {
        foreach (var page in instantiatedObject)
        {
            if (page.activeInHierarchy)
            {
                page.SetActive(false);
            }
        }
        AudioManager.audioManager.Play("click");
    }

    public void DisableAllSet()
    {
        foreach (var page in pageSet)
        {
            if (page.activeInHierarchy)
            {
                page.SetActive(false);
            }
        }
    }

    public void EnablePage()
    {
        int index = 0;
        foreach (var page in pages)
        {
            index++;
            if(pageNumber == index)
            {
                page.SetActive(true);
                
            }
        }
    }
    public void EnableSet()
    {
        int index = 0;
        foreach (var page in pageSet)
        {
            index++;
            if (pageSetCount == index)
            {              
                page.SetActive(true);
                
                // AudioManager.audioManager.Play(page.name);
            }
        }
    }
    public void RightButton()
    {      
        AudioManager.audioManager.Play("flip");
        if (pageSetCount == 33)
            return;

        leftButton.SetActive(true);
        rightButton.SetActive(true);
        if (pageSetCount >= 0)
        {
            pageSetCount++;
            DisableAllSet();
            EnableSet();
        }
        if (pageSetCount == 33)
            rightButton.SetActive(false);
    }
    public void LeftButton()
    {
        AudioManager.audioManager.Play("flip");
        if (pageSetCount == 1)
            return;
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        if (pageSetCount >= 0)
        {
            pageSetCount--;
            DisableAllSet();
            EnableSet();
        }
        if (pageSetCount == 1)
            leftButton.SetActive(false);
    }

    public void GoToGame(string pageName)
    {

        if (overlayCanvas.Contains(pageName) && !instantiatedObject.Any(s => s.name == pageName))
        {

            GameObject overlayActivity =Instantiate(Resources.Load<GameObject>(pageName),overlayCanvasGameObject.transform);
            overlayActivity.name = pageName;
            instantiatedObject.Add(overlayActivity);
            overlayActivity.SetActive(true);
           
        } else if (cameraCanvas.Contains(pageName) && !instantiatedObject.Any(s => s.name == pageName))
        {
            GameObject cameraActivity = Instantiate(Resources.Load<GameObject>(pageName), cameraCanvasGameObject.transform);
            cameraActivity.name = pageName;
            instantiatedObject.Add(cameraActivity);
            cameraActivity.SetActive(true);
        } else if(instantiatedObject.Any(s => s.name == pageName))
        {
            foreach (GameObject page in instantiatedObject)
            {
                if (page.name == pageName)
                {
                    page.SetActive(true);
                    
                }
            }
           
        }else
        {
            return;
        }
       // AudioManager.audioManager.Play(pageName);
        closeButton.SetActive(true);
        randomBackground.ChangeBackground();
        setCanvas.SetActive(false);
        AudioManager.audioManager.Play("click");
        #if UNITY_WEBGL && !UNITY_EDITOR
        FullscreenController.instance.GoFullscreen();
#endif


    }

    public void StopSound()
    {
          AudioManager.audioManager.StopSound();
    }
   private void Update()
   {
#if UNITY_EDITOR
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         DisableAllPage();
      }
#endif
   }
}
