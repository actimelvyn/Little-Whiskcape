using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_StartSquashNStretch : MonoBehaviour
{
    [SerializeField] private bool startAnimation;
    // Start is called before the first frame update
    void Start()
    {
        //TimerToCall script linked to Main object, so
        StartCoroutine(gameObject.GetComponent<Script_SquashNStretch>().SquashAndStretchEffect());
    }
}
