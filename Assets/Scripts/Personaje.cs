using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Personaje : MonoBehaviour
{
	
	private Rigidbody2D rb2d;
	
	[Header ("Movimiento")]
	
	//private float movimientoHorizontal = 0f;
	//[SerializeField] private float velocidadDelMovimiento;
	//private bool mirandoDerecha = true;	
	[Range(0,0.3f)] [SerializeField]private float suavizadorDeMovimiento;
	private Vector3 velocidad = Vector3.zero;
	
	
	[Header ("Salto")]
	[SerializeField]private float fuerzaDeSalto;
	//[SerializeField]private LayerMask queEsSuelo;
	//[SerializeField]private Transform controladorSuelo;
	//[SerializeField]private Vector3 dimensionesCaja;
	[SerializeField]private bool enSuelo = true;
	[SerializeField]private bool salto = false;
	[SerializeField]private bool Colision = false;
	
	[Header ("Puntos y Salud")]
	public int saludMaxima = 3;
	public int saludActual = 3;
	
	[Header ("Animacion")]
	private Animator animator;
	
	private void Start()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		Colision = false;
	}

	public void Update()
	{ 
		if (saludActual > saludMaxima)
		{
			saludActual = saludMaxima;
		}
		
		if (saludActual <= 0)
		{ 
			//Destroy(gameObject);
			gameObject.SetActive(false);
		}	
		
		animator.SetFloat("VelocidadY", rb2d.velocity.y);
		
		if (Input.GetButtonDown("Jump") && enSuelo ==true)
		{
			rb2d.AddForce(Vector2.up * fuerzaDeSalto,ForceMode2D.Impulse);
			salto = true;
			enSuelo = false;
		}
    }
	
	private void FixedUpdate ()
	{
		animator.SetBool("Colision",Colision);
		Colision = false;
		animator.SetBool("enSuelo",enSuelo);
		mover (Time.fixedDeltaTime, salto);
		salto = false;
	}
	
	private void mover (float mover, bool saltar)
	{
		Vector3 velocidadObjetivo = new Vector2 (mover, rb2d.velocity.y);
		rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, velocidadObjetivo, ref velocidad, suavizadorDeMovimiento);
		
    }
 
	public void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Piso")
		{
			enSuelo = true;
			salto = false;
		}
		if (collision.gameObject.tag =="Enemigos")
		{
			saludActual -= 1;
			Colision = true;
			//enSuelo = true;
			//salto = false;
		}
		//else if (collision.gameObject.tag == "Solomillo")
		//{
			//saludActual += 1;
		//}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag =="Enemigos")
		{
			//saludActual -= 1;
			//Colision = true;
			//enSuelo = true;
			//salto = false;
		}
		else if (collision.gameObject.tag == "Solomillo")
		{
			saludActual += 1;
		}
	}
}
