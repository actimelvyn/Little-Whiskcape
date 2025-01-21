using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class bazooka : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject cheese;
    public GameObject currentCheese;

    public float speed = 20f;

    
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Launch();

    }

 
    // new cheese load 
    private void Load()
    {
        GameObject cheeseInstance = Instantiate(cheese, spawnPoint.position, spawnPoint.rotation);
        cheeseInstance.transform.parent = spawnPoint;
        currentCheese = cheeseInstance;
        Rigidbody rig_m = currentCheese.GetComponent<Rigidbody>();
        rig_m.isKinematic = true;

    }


    //launch cheese 

    private void Launch()
    {

        Rigidbody rig_m = currentCheese.GetComponent<Rigidbody>();
        currentCheese.transform.parent = null;
        rig_m.isKinematic = false;
        rig_m.AddForce(spawnPoint.right * speed, ForceMode.Impulse);
        
        Invoke("Load", 2f);

    }

}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class bazooka : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject cheese;
    public GameObject currentCheese;

    public float speed = 20f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Launch();
    }

    // new cheese load 
    private void Load()
    {
        GameObject cheeseInstance = Instantiate(cheese, spawnPoint.position, spawnPoint.rotation);
        cheeseInstance.transform.parent = spawnPoint;
        currentCheese = cheeseInstance;
        Rigidbody rig_m = currentCheese.GetComponent<Rigidbody>();
        rig_m.isKinematic = true;

    }


    //launch cheese 

    private void Launch()
    {
        Load();

        Rigidbody rig_m = currentCheese.GetComponent<Rigidbody>();
        currentCheese.transform.parent = null;
        rig_m.isKinematic = false;
        rig_m.AddForce(-spawnPoint.up * speed, ForceMode.Impulse);


        Invoke("Load", 2f);

    }

}*/
