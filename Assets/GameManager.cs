using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
	public GameObject gameOverPanel;
	
	public void GameOver()
	{

		StartCoroutine(deathDelay(2f));


		//gameOverPanel.SetActive(true);
		//Time.timeScale = 0f;
	}


	private IEnumerator deathDelay(float delay)
	{
		yield return new WaitForSeconds(delay);

		gameOverPanel.SetActive(true);
		Time.timeScale = 0f;


	}


	
	public void RestartGame()
	{
		Time.timeScale = 1f;
		
		SceneManager.LoadScene(
			SceneManager.GetActiveScene().buildIndex
			);
	}
	
}
