using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
	public AudioSource sonido;
	public AudioClip Musica;
	
    void Start()
    {
	    sonido.clip = Musica;
	    
    }

    // Update is called once per frame
	public void Reproducir ()
    {
	    sonido.Play	 ();
	    
    }
}
