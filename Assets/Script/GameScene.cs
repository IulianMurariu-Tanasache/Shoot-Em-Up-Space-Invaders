using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float fadeInDuration = 2;
	private bool gameStarted;
	public AudioSource backGame;
	public Dead dead;


	private void Start()
	{	//Fade ca in MenuScene si Preloader
		fadeGroup = FindObjectOfType<CanvasGroup> ();
		fadeGroup.alpha = 1;
		if (SaveManager.Instance.state.music == true) {
			backGame.mute = false;
		} 

		else {
			backGame.mute = true;
		}

		/*if (soundE == true) {
			soundEOn.text = "Sound Effects ON";
			SoundE.GetComponent<Image> ().sprite = audioOn;

		} 

		else {
			SoundE.GetComponent<Image> ().sprite = audioOFF;
			soundEOn.text = "Sound Effects OFF";
		}*/
	}

	private void Update()
	{	//Fade
		if (Time.timeSinceLevelLoad <= fadeInDuration) {
			fadeGroup.alpha = 1 - (Time.timeSinceLevelLoad / fadeInDuration);
		} 

		else if (!gameStarted) {
			gameStarted = true;
			fadeGroup.alpha = 0;
		}

		if(dead.dead == true)
			backGame.mute = true;

	}


	public void CompleteLevel()
	{	//Termina nivelul + salveaza
		SaveManager.Instance.CompleteLevel (Manager.Instance.currentLevel);

		//Exit scene
		ExitScene();
	}

	public void ExitScene()
	{	//Focus pe selectorul de nivele
		Manager.Instance.menuFocus = Manager.Instance.playToMenu;
		
		SceneManager.LoadScene ("Menu");
	}

}
