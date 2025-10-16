using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour
{
    [SerializeField] public Image _background;
    [SerializeField] Sprite[] sprites;
    public static RandomBackground randomBackground;
    private void Awake()
    {
        randomBackground = this;
        ChangeBackground();
    }
    public void ChangeBackground()
    {
        int index = Random.Range(0, sprites.Length);
        Sprite randomSprite = sprites[index];
       // _background.sprite = randomSprite;
        _background.overrideSprite = randomSprite;
    }
}
