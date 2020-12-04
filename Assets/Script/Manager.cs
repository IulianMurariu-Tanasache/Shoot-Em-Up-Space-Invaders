using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public static Manager Instance { set; get;}
	public Transform player;
	private int sens;

	private void Awake()
	{ 	DontDestroyOnLoad (gameObject);
		Instance = this;
	}

	public int currentLevel = 0; //Folosit cand schimbam intre Meniu si Joc
	public int menuFocus = 0;    //Folosit intre Joc si Meniu
	public int playToMenu;

	public void GetPlayerInput()
	{
		//Folosim accelerometer
		if (SaveManager.Instance.state.usingAccelerometer) {
			
			sens = SaveManager.Instance.state.sensitivityTilt / 10;
			player.Translate (Input.acceleration.x * sens, 0, 0);
		} 

	}


}
