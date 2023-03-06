using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
	public Transform Personaje;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    transform.position = new Vector3(Personaje.position.x, transform.position.y, transform.position.z);
    }
}
