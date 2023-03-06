using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destructor : MonoBehaviour
{
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Eggs"||collision.gameObject.tag == "Enemigos"
			||collision.gameObject.tag == "Solomillo"||collision.gameObject.tag == "Adds"
			||collision.gameObject.tag == "Personaje")
		{
			//collision.gameObject.SetActive (false);
			Destroy(collision.gameObject);
		}
	}
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Eggs"||other.gameObject.tag == "Enemigos"
			||other.gameObject.tag == "Solomillo"||other.gameObject.tag == "Adds")
		{
			Destroy(other.gameObject);
		}
	}
}
