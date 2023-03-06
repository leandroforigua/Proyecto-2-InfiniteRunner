using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Huevos : MonoBehaviour
{
	[SerializeField] private float velocidadX  = 0;
	[SerializeField] private float jumpForce = 0;
	//[SerializeField] private float velocidadZ  = 0;
	private Rigidbody2D rb;
	private int Eggs = 100;
	
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();	//Obtiene el componente Rigidbody del objeto
		rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode2D.Impulse);
		//rb.AddTorque(velocidadZ);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Si el objeto entra en contacto con el suelo, se agrega una fuerza de rebote
		if(collision.gameObject.tag == "Personaje")
		{
			Programacion gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Programacion>();
			gameManager.puntosTotales += Eggs; // Sumar los puntos del objeto al total de puntos del Game Manager
			
			Destroy(gameObject); // Destruir el objeto después de la colisión
		}
	}
	
	void FixedUpdate() 
	{
		transform.Translate (velocidadX * Time.deltaTime, 0, 0);
		//transform.Rotate(0,0,velocidadZ * Time.deltaTime);
		//transform.Rotate(Vector3.up * velocidadZ);
		
		if (Mathf.Abs(rb.velocity.y) < 0.1f) 
		{
			rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode2D.Impulse);
			//rb.AddTorque(velocidadZ);
		}
	}
}

