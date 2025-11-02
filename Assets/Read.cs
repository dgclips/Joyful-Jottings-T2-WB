using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Read : MonoBehaviour
{
   public void PlaySound(string sound)
   {
      AudioManager.audioManager.Play(sound);
   }
}
