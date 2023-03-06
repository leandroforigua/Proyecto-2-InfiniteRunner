using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAdds : MonoBehaviour

{
	
	[SerializeField] private float velocidadX  = 0;
	//private float posicionInical =5700f;

    void Start()
    {
	   
    }

    void Update()
    {
	    transform.Translate (velocidadX * Time.deltaTime, 0, 0);
	    {
	    	
	    	{
	    		//Destroy(gameObject);
		    	//transform.position = new Vector3(posicionInical, 720f, 0f);
	    	}
	    }
	    
    }
}
