using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageThirtyFourController : MonoBehaviour
{
    [SerializeField] List<SelectRoom> rooms;
    public Image img;
    public int totalCount;
    int count;
    [System.Serializable]
    public class SelectRoom
    {
        public Button button;
        public Image image;
    }

    private void Start()
    {
        foreach (var room in rooms)
        {
            room.button.onClick.AddListener(()=>SelectIt(room.image));
        }
    }


    public void SelectIt(Image image)
    {
        AudioManager.audioManager.Play("click");
        count++;
        img.sprite = image.sprite;
        if(count == totalCount)
        {
            EventManager.GameComplete();
        }

    }

    public void Reset()
    {
        count = 0;
        img.sprite = null;
        AudioManager.audioManager.Play("click");
    }
}
