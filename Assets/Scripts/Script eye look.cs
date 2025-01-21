using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripteyelook : MonoBehaviour
{
    public Transform eyeDist;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(eyeDist);
    }
}
