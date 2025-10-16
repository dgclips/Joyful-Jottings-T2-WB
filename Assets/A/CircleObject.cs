using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleObject : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Image _imageTwo;
    [SerializeField] Image _imageThree;
    [SerializeField] Button _buttonone;
    [SerializeField] Button _buttontwo;
    [SerializeField] Button _buttonthree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckOddOne(int odd)
    {

        _image.enabled = true;
      if (_imageTwo != null) _imageTwo.enabled = false;
       if(_imageThree!=null) _imageThree.enabled = false;
        if (odd == 1)
        {
            DisableButtons();
            CircleController.count++;
            _image.color = Color.green;
            if(CircleController.totalItem == CircleController.count)
            {
                EventManager.GameComplete();
            }
        }else
        {
            _image.color= Color.red;
        }
        AudioManager.audioManager.Play("click");
    }
    public void CheckOddTwo(int odd)
    {
        _imageTwo.enabled = true;
        _image.enabled = false;
        if (_imageThree != null) _imageThree.enabled= false;
        if (odd == 1)
        {
            DisableButtons();
            CircleController.count++;
            _imageTwo.color = Color.green;
            if (CircleController.totalItem == CircleController.count)
            {
                EventManager.GameComplete();
            }
        }
        else
        {
            _imageTwo.color = Color.red;
        }
        AudioManager.audioManager.Play("click");
    }
    public void CheckOddThree(int odd)
    {
        if (_imageThree != null) _imageThree.enabled = true;
        _image.enabled= false;
        _imageTwo.enabled= false;
        if (odd == 1)
        {
            DisableButtons();
            CircleController.count++;
            _imageThree.color = Color.green;
            if (CircleController.totalItem == CircleController.count)
            {
                EventManager.GameComplete();
            }
        }
        else
        {
            _imageThree.color = Color.red;
        }
        AudioManager.audioManager.Play("click");
    }
    void DisableButtons()
    {
        _buttonone.enabled = false;
      if (_imageTwo != null) _buttontwo.enabled = false;
        if (_imageThree != null) _buttonthree.enabled = false;
    }
    public void Reset()
    {
        _image.color = Color.white;
      if (_imageTwo != null) _imageTwo.color= Color.white;
        if (_imageThree != null) _imageThree.color= Color.white;
        _image.enabled = false;
      if (_imageTwo != null) _imageTwo.enabled= false;
        if (_imageThree != null) _imageThree.enabled= false;
        _buttonone.enabled = true;
      if (_imageTwo != null) _buttontwo.enabled = true;
        if (_buttonthree != null) _buttonthree.enabled = true;
        CircleController.count = 0;
        AudioManager.audioManager.Play("click");
    }
}
