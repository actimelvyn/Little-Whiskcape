    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_vase : MonoBehaviour
{
    public AudioSource sfxfloor;
    public void PlayVaseJumpSound()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
    }
    public void vaseOnFloor()
    {
  
            sfxfloor.Play();
        
    }
}
