using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_fell : MonoBehaviour
{
    public AudioSource fell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void PlayFellDoor()
    {

        fell.Play();

    }
}
