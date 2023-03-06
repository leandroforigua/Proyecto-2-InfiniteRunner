using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
	[SerializeField] private float velocidadX  = 0;
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		//Si el objeto entra en contacto con el suelo, se agrega una fuerza de rebote
		if (collision.gameObject.tag == "Personaje")
		{
			//Personaje gameManager = GameObject.FindGameObjectWithTag("Personaje").GetComponent<Personaje>();
			//gameManager.saludActual -= Vidas;
			Destroy(gameObject,Time.deltaTime);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Personaje")) // Comprueba si el objeto que entra en el trigger es el personaje
		{
			//Personaje gameManager = GameObject.FindGameObjectWithTag("Personaje").GetComponent<Personaje>();
			//gameManager.saludActual -= Vidas;
			
			Destroy(gameObject,Time.deltaTime);
			
			other.GetComponent<Animator>().SetTrigger("Colision"); // Accede al componente de animación del personaje y activa la animación deseada
		}
	}
	void Update()
	{
		transform.Translate (velocidadX * Time.deltaTime, 0, 0);
		{
			//
			//transform.position = new Vector3(posicionInical, 720f, 0f);
		}
	    
	}
}
