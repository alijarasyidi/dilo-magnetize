using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public bool isPlay = false;

	[SerializeField] private GameObject[] tower;
	[SerializeField] private Transform player;
	private Rigidbody2D playerRb;
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private GameObject loadLevelPanel;
	[SerializeField] private Text loadLevelText;
	[SerializeField] private GameObject finishPanel;
	[SerializeField] private GameObject[] levels;
	private GameObject currentLevel;
	private Level levelScript;
	private int currentLevelIndex = 0;
	private AudioSource theme;

	void Awake ()
	{
		playerRb = player.gameObject.GetComponent<Rigidbody2D> ();
		theme = GetComponent <AudioSource> ();
	}

	// Use this for initialization
	void Start ()
	{
		loadLevelPanel.SetActive (true);
		loadLevelText.text = "Level " + (currentLevelIndex+1);
		theme.Play ();

		currentLevel = Instantiate (levels[currentLevelIndex], transform.position, transform.rotation);
		levelScript = currentLevel.GetComponent<Level> ();
		player.position = levelScript.playerPos;
		player.rotation = Quaternion.Euler (levelScript.playerRot);
		for (int i = 0; i < levelScript.towerNum; i++)
		{
			tower[i].SetActive (true);
			tower[i].transform.position = levelScript.towerPos[i];
		}
		Invoke ("IsPlay", 1.5f);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			PauseMode ();
		}		
	}

	void PauseMode ()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void Resume ()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public void Restart ()
	{
		SceneManager.LoadScene ("Game");
	}

	public void Goal ()
	{
		isPlay = false;
		playerRb.velocity = Vector3.zero;
		playerRb.angularVelocity = 0;

		if (currentLevelIndex + 1 >= levels.Length)
		{
			finishPanel.SetActive (true);
		} else
		{
			for (int i = 0; i < levelScript.towerNum; i++)
			{
				tower[i].SetActive (false);
			}
			Destroy (currentLevel);
			currentLevelIndex++;
			loadLevelPanel.SetActive (true);
			loadLevelText.text = "Level " + (currentLevelIndex+1);
			Invoke ("LoadLevel", 1.5f);
		}
	}

	void LoadLevel ()
	{
		currentLevel = Instantiate (levels[currentLevelIndex], transform.position, transform.rotation);
		levelScript = currentLevel.GetComponent<Level> ();
		player.position = levelScript.playerPos;
		player.rotation = Quaternion.Euler (levelScript.playerRot);
		for (int i = 0; i < levelScript.towerNum; i++)
		{
			tower[i].SetActive (true);
			tower[i].transform.position = levelScript.towerPos[i];
		}
		Invoke ("IsPlay", 1.5f);
	}

	void IsPlay ()
	{
		loadLevelPanel.SetActive (false);
		isPlay = true;
	}

	public void GameOver ()
	{
		gameOverPanel.SetActive (true);
		isPlay = false;
	}

	public void Retry ()
	{
		playerRb.velocity = Vector3.zero;
		playerRb.angularVelocity = 0;
		gameOverPanel.SetActive (false);
		player.position = levelScript.playerPos;
		player.rotation = Quaternion.Euler (levelScript.playerRot);
		loadLevelPanel.SetActive (true);
		loadLevelText.text = "Level " + (currentLevelIndex+1);
		Invoke ("IsPlay", 1.5f);
	}

}
