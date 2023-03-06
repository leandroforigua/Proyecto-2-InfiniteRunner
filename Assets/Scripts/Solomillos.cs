using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solomillos : MonoBehaviour
{
	[SerializeField] private float velocidadX  = 0;
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Personaje")
		{
			//Personaje gameManager = GameObject.FindGameObjectWithTag("Personaje").GetComponent<Personaje>();
			//gameManager.saludActual += 1;
			
			Destroy(gameObject,Time.deltaTime);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Personaje")) // Comprueba si el objeto que entra en el trigger es el personaje
		{
			//Personaje gameManager = GameObject.FindGameObjectWithTag("Personaje").GetComponent<Personaje>();
			//gameManager.saludActual += 1;
			
			Destroy(gameObject,Time.deltaTime);
		}
	}
	
	void Update()
	{
		transform.Translate (velocidadX * Time.deltaTime, 0, 0);
	}
}
