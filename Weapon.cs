using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform spawn;
    public GameObject Prefab;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetButtonDown("Fire1"))
     {

        Shoot();
     }
    }

    void Shoot()
    {
        Instantiate(Prefab, spawn.position, spawn.rotation);
    }


}
