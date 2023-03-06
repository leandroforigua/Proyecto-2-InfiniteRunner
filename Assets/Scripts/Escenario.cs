using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escenario : MonoBehaviour
{
	[SerializeField] private Vector2 velocidadEscena;
	private Vector2 offset;
	private Material material;
	//private Rigidbody2D CavemanRB;
	
	
	private void Awake()
	{
		material = GetComponent<SpriteRenderer>().material;
		//CavemanRB = GameObject.FindGameObjectWithTag ("PJs").GetComponent<Rigidbody2D>();
    }

	private void Update()
    {
	    offset = velocidadEscena * Time.deltaTime;
	    //offset = (CavemanRB.velocity.x * 0.1f) * velocidadEscena * Time.deltaTime;
	    material.mainTextureOffset += offset;
    }
}
