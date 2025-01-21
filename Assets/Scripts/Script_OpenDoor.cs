using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_OpenDoor : MonoBehaviour
{
    public GameObject door_closed, door_opened;

    // Start is called before the first frame update
    void Start()
    {
        door_opened.SetActive(false);
        door_closed.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (Input.GetKeyUp(KeyCode.E))
            {
                door_closed.SetActive(false);

                door_opened.SetActive(true);
            }
        }
    }
}
