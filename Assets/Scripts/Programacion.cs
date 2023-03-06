using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Image))]

public class Programacion : MonoBehaviour
{
	[Header ("Escenas")]
	[SerializeField]private float timeRemaining = 7;
	[SerializeField]private bool timerIsRunning = false;
	[SerializeField]private GameObject[] Intro;
	public GameObject[] cicloDiario; // Los nombres de las escenas de los paneles en orden
	public bool cambiarPanel; // El número total de paneles
	public int panelActual = 0; // El número de panel actual
	[SerializeField]private	GameObject [] EscenariosItemsYEnemigos;
	
	[SerializeField]private GameObject GameOver;
	[SerializeField]private TextMeshProUGUI PuntosTextoFinales;
	
	[Header ("Personaje")]
	public GameObject Personaje;
	[SerializeField]private TextMeshProUGUI PuntosTexto;
	[SerializeField] private GameObject[] Solomillos;
	public int puntosTotales = 0; // La cantidad total de puntos ganados
	public int puntosPorPanel = 500; // La cantidad de puntos necesaria para recargar un panel
	private bool personajeMuere = false;
	
	
	[Header ("Items y Enemigos")]
	public GameObject[] enemigosPrefab;	// Prefab del enemigo que queremos instanciar
	public GameObject [] solomilloPrefab;	// Prefab del solomillo que queremos instanciar
	public GameObject[] eggsPrefab;		// Prefab de los huevos que queremos instanciar
	public float[] spawnTimeEnemigos;   
	public float[] spawnTimeSolomillos;
	public float[] spawnTimeEggs;
	public Transform[] spawnPoint;    	// Punto de aparición de enemigos
	//public List<GameObject> spawnedObjects = new List<GameObject>();
	
	private class SpawnData 
	{
		public int[] indexRange;
		public string objectType;
	}
	
	void Start()
	{
		StartCoroutine(SpawnEnemigos());
		StartCoroutine(SpawnSolomillos());
		StartCoroutine(SpawnEggs());
	}

	IEnumerator SpawnEnemigos()
		{
			int index = 0;
			while (true) 
			{
				float spawnTime = spawnTimeEnemigos[index % spawnTimeEnemigos.Length];
				SpawnData data = new SpawnData { indexRange = new int[] { index % enemigosPrefab.Length, index % enemigosPrefab.Length }, objectType = "Enemigos" };
				yield return new WaitForSeconds(spawnTime);
				SpawnObject(data);
				//spawnedObjects.Add(obj);
				index++;
				if (personajeMuere) 
				{
					yield break; // Salir de la corutina si el jugador ha muerto
				}
			}
		}

	IEnumerator SpawnSolomillos() 
		{
			int index = 0;
			while (true) {
				float spawnTime = spawnTimeSolomillos[index % spawnTimeSolomillos.Length];
				SpawnData data = new SpawnData { indexRange = new int[] { index % solomilloPrefab.Length, index % solomilloPrefab.Length }, objectType = "Solomillo" };
				yield return new WaitForSeconds(spawnTime);
				SpawnObject(data);
				//spawnedObjects.Add(obj);
				index++;
				if (personajeMuere) 
				{
					yield break; // Salir de la corutina si el jugador ha muerto
				}
			}
		}

	IEnumerator SpawnEggs() 
		{
			int index = 0;
			while (true) 
			{
				float spawnTime = spawnTimeEggs[index % spawnTimeEggs.Length];
				SpawnData data = new SpawnData { indexRange = new int[] { index % eggsPrefab.Length, index % eggsPrefab.Length }, objectType = "Eggs" };
				yield return new WaitForSeconds(spawnTime);
				SpawnObject(data);
				//spawnedObjects.Add(obj);
				index++;
				if (personajeMuere) 
				{
					yield break; // Salir de la corutina si el jugador ha muerto
				}
			}
		}

	void SpawnObject(SpawnData data) 
	{
		int index = Random.Range(data.indexRange[0], data.indexRange[1] + 1);
		GameObject[] prefabs = null;

		switch (data.objectType) 
		{
		case "Enemigos":
			prefabs = enemigosPrefab;
			break;
		case "Solomillo":
			prefabs = solomilloPrefab;
			break;
		case "Eggs":
			prefabs = eggsPrefab;
			break;
		default:
			Debug.LogError("Tipo de objeto no reconocido: " + data.objectType);
			break;
		}
		
		GameObject obj = Instantiate(prefabs[index], spawnPoint[1]);
	}
	
	
	void Update()
	{
		
		//Introduccion
		if (Intro[1].activeInHierarchy)
		{
			timerIsRunning = true;
		}
			
		if (timerIsRunning)
		{
			if (timeRemaining > 0)
			{
					timeRemaining -= Time.deltaTime;
			}
			else
			{
				timeRemaining = 0;
				timerIsRunning = false;
				Intro[0].SetActive (false);
				Intro[1].SetActive (false);
				EscenariosItemsYEnemigos[0].SetActive(true);
				EscenariosItemsYEnemigos[1].SetActive(true);
			}
		}
		
		//Vidas en pantalla
		Personaje characterScript = Personaje.GetComponent<Personaje>();
		int Salud = characterScript.saludActual;
		if (Salud == 3)
		{
			Solomillos[2].SetActive(true);
		}
		else if (Salud <= 3)
		{
			Solomillos[2].SetActive(false);
		}
		
		if (Salud == 2)
		{
			Solomillos[1].SetActive(true);
		}
		else if (Salud <= 2)
		{
			Solomillos[1].SetActive(false);
		}
		
		if (Salud == 1)
		{
			Solomillos[0].SetActive(true);
		}
		else if (Salud <= 1)
		{
			Solomillos[0].SetActive(false);
		}
		
		//Limpiar Pantalla
		if (Salud == 0 && !personajeMuere)
		{
			personajeMuere = true;
			
			DestroyAllEnemigos();
			DestroyAllSolomillos();
			DestroyAllEggs();
			
			EscenariosItemsYEnemigos[0].SetActive(false);
			EscenariosItemsYEnemigos[1].SetActive(false);
			GameOver.SetActive(true);
		}
		
		// Puntos en pantalla
		PuntosTexto.text = puntosTotales.ToString();
		PuntosTextoFinales.text = puntosTotales.ToString();
		
		if (puntosTotales % puntosPorPanel == 0) // Verificar si la cantidad de puntos totales es un múltiplo de 500
		{
			if (puntosTotales != 0)
			{
				if (cambiarPanel)
				{
					RecargarPanel();
				}
			}	
		}
		else
		{
			cambiarPanel = true;
		}
	}
	
	void DestroyAllEnemigos ()
	{
		// Obtener todos los objetos de la escena
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		// Destruir todos los enemigos de la escena
		foreach (GameObject obj in allObjects)
		{
			if (obj.CompareTag("Enemigos"))
			{
				Destroy(obj);
			}
		}
	}

	void DestroyAllSolomillos() 
	{
		// Obtener todos los objetos de la escena
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		// Destruir todos los objetos de puntos de la escena
		foreach (GameObject obj in allObjects) 
		{
			if (obj.CompareTag("Solomillo")) 
			{
				Destroy(obj);
			}
		}
	}

	void DestroyAllEggs() 
	{
		// Obtener todos los objetos de la escena
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		// Destruir todos los objetos de vida de la escena
		foreach (GameObject obj in allObjects) {
			if (obj.CompareTag("Eggs")) {
				Destroy(obj);
			}
		}
	}
	
	public void RecargarPanel()
	{
		cambiarPanel=false;
		cicloDiario[panelActual].SetActive(false); // Desactivar el panel actual
		panelActual = (panelActual + 1) %cicloDiario.Length; // Cambiar al siguiente panel
		cicloDiario[panelActual].SetActive(true); // Activar el siguiente panel
	}
}