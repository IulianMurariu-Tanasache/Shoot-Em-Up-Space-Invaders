using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance { set; get;}
	public SaveState state;


	private void Awake()
	{	
		DontDestroyOnLoad (gameObject);
		Instance = this;
		Load ();

		//Folosim accelerometer si putem sa-l folosim
		if (state.usingAccelerometer == true && !SystemInfo.supportsAccelerometer)
		{state.usingAccelerometer = false;
			Save ();
	}
	}

	//salveaza SaveState in Player Pref
	public void Save()
	{
		PlayerPrefs.SetString ("save", Helper.Serialize<SaveState> (state));

	}

	//incarca SaveState din Player Pref
	public void Load()
	{
		//Avem un SaveState?
		if (PlayerPrefs.HasKey ("save")) {
			state = Helper.Deserialize<SaveState> (PlayerPrefs.GetString ("save"));
		}
		else 
		{
			state = new SaveState();
			state.music = state.soundE = true;
			for(int i = 0; i <= 11; i ++)
				state.upgradeUnlock[i] = 0;
			UnlockUpgrade (0);
			UnlockUpgrade (4);
			UnlockUpgrade (8);
			state.activeUp [0] = 0;
			state.activeUp [1] = 4;
			state.activeUp [2] = 8;
			Save();
			Debug.Log ("N-am mai salvat dar te salvam noi xD");
		}
	
	}

	//Daca nu ne jefuiesti de skin-uri
	public bool IsUpgradeOwned(int index)
	{
		//Verificam daca este devlocat
		if (state.upgradeUnlock [index] == 0)
			return false;
		else
			return true;
	
	}

	//Deblocheaza skin-uri
	public void UnlockUpgrade(int index)
	{
		//deblocam
		state.upgradeUnlock[index] = 1;
	}

	//Incercare de cumparare
	public bool BuyUpgrade(int index, int cost)
	{
		if (state.gold >= cost) {
			//Daca avem scadem costul din suma jucatorului si deblocam skin-ul
			state.gold -= cost;
			UnlockUpgrade (index);
			Save ();
			return true;
		}

		else 
		{
			//Daca nu avem, nu cumparam...
			return false;

		}

	}

	//CompleteLevel
	public void CompleteLevel(int index)
	{
		if (state.completedLevel == index) {
			state.completedLevel++;
			Save ();
		}
		}



}
