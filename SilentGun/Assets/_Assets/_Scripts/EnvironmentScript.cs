using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnvironmentScript : MonoBehaviour
{
	//Other
	public PlayerMovementScript playerMovementScript;
	[SerializeField] private GameObject playerGO;
	[SerializeField] private GameObject[] levels;
	[SerializeField] private int levelCount = 0;

	//public TextMeshPro roundHolder;
	private int roundInt = 1;

	[HideInInspector] public bool shutUpdate;

	//public GameObject mysteryBox;
	//private Vector2 targetPosition = new Vector2 (0, 0);
	//public float speed = 5f;

	//public GameObject alreadyDoneTut;

	private GameObject newSpawnedLevel;

	private void Start()
	{
		Application.targetFrameRate = 60;

		LoadNextLevel();
		AlignPlayerToStart();
	}

	private void LoadNextLevel()
	{
		newSpawnedLevel = null;

		if (levelCount < levels.Length)
		{
			newSpawnedLevel = Instantiate(levels[levelCount], new Vector3(0, 0, levels[levelCount].transform.position.z), Quaternion.identity);
			roundInt++;

			shutUpdate = false;
		}
		else
		{
			Debug.Log("Levels are done");
			SceneManager.LoadScene("EndScene");
		}

		AlignPlayerToStart();
	}

	private void AlignPlayerToStart()
	{
		if (newSpawnedLevel == null) { /*Debug.LogWarning("newSpawnedLevel is null!");*/ return; }

		Transform newStartPoint = newSpawnedLevel.GetComponent<LevelPrefabScript>().startPos;

		playerMovementScript.MoveToTheNewSpawnPoint(newStartPoint);
	}

	public void PrepareForNextLevel()
	{
		Destroy(newSpawnedLevel);
		levelCount++;
		//playerMovementScript.rb.isKinematic = true;
		//playerMovementScript.rb.bodyType = RigidbodyType2D.Static;
		//StartCoroutine(MoveMysteryBoxToPosition(0));

		LoadNextLevel();

		//
		//		MOVE PLAYER TO DEFAULT PLACE
		//
	}

	public void RestartLevel()
	{
		Destroy(newSpawnedLevel);
		newSpawnedLevel = Instantiate(levels[levelCount], new Vector3(0, 0, levels[levelCount].transform.position.z), Quaternion.identity);
		shutUpdate = false;

		AlignPlayerToStart();
	}

	//public IEnumerator MoveMysteryBoxToPosition(int i)
	//{
	//	while (Vector2.Distance(mysteryBox.transform.position, targetPosition) > 0.1f)
	//	{
	//		float step = speed * Time.deltaTime;
	//		mysteryBox.transform.position = Vector2.MoveTowards(mysteryBox.transform.position, targetPosition, step);
	//		yield return null;
	//	}

	//	mysteryBox.transform.position = targetPosition;

	//	//playerMovementScript.rb.isKinematic = false;
	//	//playerMovementScript.rb.bodyType = RigidbodyType2D.Dynamic;

	//	if (i == 0)
	//	{
	//		LoadNextLevel();
	//	}
	//}







	private void Update()
	{
		//if (playerMovementScript.allEnemiesDied && !shutUpdate && !playerMovementScript.isRotating)
		//{
		//	shutUpdate = true;
		//	PrepareForNextLevel();
		//}

		//if (areWeInTutorial && PlayerPrefs.GetInt("TutDone") == 1)
		//{
		//	// Check if the Enter key (Return key) is pressed
		//	if (Input.GetKeyDown(KeyCode.Space))
		//	{
		//		isHoldingEnter = true;
		//		holdTime = 0f; // Reset hold time
		//	}

		//	// Check if the Enter key is being held down
		//	if (Input.GetKey(KeyCode.Space))
		//	{
		//		if (isHoldingEnter)
		//		{
		//			holdTime += Time.deltaTime; // Increment hold time

		//			// Check if the hold time meets the required hold time
		//			if (holdTime >= requiredHoldTime)
		//			{
		//				// Do something here after holding Enter for 2 seconds
		//				Debug.Log("Space key held for 2 seconds!");

		//				PlayerPrefs.SetInt("TutDone", 1);
		//				PlayerPrefs.Save();
		//				SceneManager.LoadScene("MainMenu");

		//				// Reset to prevent multiple triggers
		//				isHoldingEnter = false;
		//			}
		//		}
		//	}

		//	// Reset if the Enter key is released
		//	if (Input.GetKeyUp(KeyCode.Return))
		//	{
		//		isHoldingEnter = false;
		//		holdTime = 0f;
		//	}
		//}
	}
}
