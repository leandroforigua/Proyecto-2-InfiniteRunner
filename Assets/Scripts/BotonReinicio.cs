using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BotonReinicio : MonoBehaviour
{
	public void Recargar()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

