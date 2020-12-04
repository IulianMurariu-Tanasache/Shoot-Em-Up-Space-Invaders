using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour 
{
	public AccelerometerButton accButton;
	Animator animator;
	private CanvasGroup fadeGroup;
	// Viteza de tranzitie
	private float fadeInSpeed = 0.66f;

	public RenderPlayerModels render;
	public Transform upgrades;
	public Transform levelPanel;
	public RectTransform menuContainer;

	public Text SkinBuyText;
	public int[] upgradeCost = { 0 , 200 , 200 , 200 , 0 , 200 , 200 , 200 , 0 , 200 , 200 , 200 } ;
	private int selectedUpgradeIndex;
	private int selectedEnemyIndex;
	public Text goldText;
	private int selectedLevel;
	public Text accText;

	private Vector3 desiredMenuPosition;

	public AnimationCurve enterLevelZoomCurve;
	private bool isEnteringLevel = false;
	private bool isEnteringEndless = false;
	private float zoomDuration = 3.0f;
	private float zoomTransition;
	public bool reset = false;

	public GameObject sensArrows;
	public GameObject sensTilt;

	public Transform skins;
	public UpgradeShop upgrade;

	public Text musicOn;
	public Text soundEOn;
	public Sprite audioOn;
	public Sprite audioOFF;
	public GameObject SoundE;
	public GameObject Music;
	public AudioSource backMenu;
	public AudioSource click;

	private FloatPlayerShopPreview menuCam;

	private void Start ()
	{  	if (SaveManager.Instance.state.music == true) {
				musicOn.text = "Music ON";
				Music.GetComponent<Image> ().sprite = audioOn;
				backMenu.mute = false;
				SaveManager.Instance.state.music = true;
			} 

		else {
			Music.GetComponent<Image> ().sprite = audioOFF;
			musicOn.text = "Music OFF";
			backMenu.mute = true;
			SaveManager.Instance.state.music = false;
			}

	if (SaveManager.Instance.state.soundE == true) {
			soundEOn.text = "Sound Effects ON";
			SoundE.GetComponent<Image> ().sprite = audioOn;
			click.mute = true;

			} 

		else {
			SoundE.GetComponent<Image> ().sprite = audioOFF;
		click.mute = false;	
		soundEOn.text = "Sound Effects OFF";
			}
	
		//Deblocam upgrade-ul initial daca nu am mai salvat pana acum
		SaveManager.Instance.state.gold = 800;

		if (SaveManager.Instance.state.usingAccelerometer == true) {
			sensArrows.SetActive (false);
			sensTilt.SetActive (true);
		}

		else if (SaveManager.Instance.state.usingAccelerometer == false) {
			sensArrows.SetActive (true);
			sensTilt.SetActive (false);
		}
			

		//Setam butonul de tilt control
		accButton.Accelerometer();
		if (SaveManager.Instance.state.usingAccelerometer == true) {
			accText.text = "Tilt Control (On)";
		} 

		else { accText.text = "Tilt Control (Off)";
		} 

		menuCam = FindObjectOfType<FloatPlayerShopPreview> ();

		//cauta CanvasGroup
		fadeGroup = FindObjectOfType<CanvasGroup>();
	   
		//Camera pe focus pentru a nu ajunge mereu in meniul principal
		SetCameraTo (Manager.Instance.menuFocus);

		//culoarea de inceput
		fadeGroup.alpha = 1;

		InitWiki ();

		//Initializeaza butoanele din selectorul de nivele
		InitLevel();

		//Initializeaza banii
		UpdateGoldText();
		OnEnemySelect (0);

		for (int i = 0; i <= 2; i++) {
			OnUpgradeSelect (SaveManager.Instance.state.activeUp[i],SaveManager.Instance.state.activeUp[i]);
			SetUpgrade (SaveManager.Instance.state.activeUp[i]);
		}
	
	}

	private void Update()
	{	
	    //Tranzitia
		fadeGroup.alpha = 1 -Time.timeSinceLevelLoad * fadeInSpeed;

		//Navigare de meniu lina
		menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D,desiredMenuPosition,0.1f);

		//Zoom in cand intram intr-un nivel
		if (isEnteringLevel) {
			
			//Adaugam in tranzitia de la zoom
			zoomTransition += 1 / zoomDuration * Time.deltaTime;

			//Schimbam marimea + animatie
			menuContainer.localScale = Vector3.Lerp (Vector3.one, Vector3.one * 5, enterLevelZoomCurve.Evaluate (zoomTransition));

			//Facem ca, Canvas-ul sa urmeze zoom-ul
			//zoom in centru
			Vector3 newDesiredPosition = desiredMenuPosition * 5;

			RectTransform rt = levelPanel.GetChild (Manager.Instance.currentLevel).GetComponent<RectTransform> ();
			newDesiredPosition = rt.anchoredPosition3D * 5;

			menuContainer.anchoredPosition3D = Vector3.Lerp (desiredMenuPosition, newDesiredPosition, enterLevelZoomCurve.Evaluate (zoomTransition));

			fadeGroup.alpha = zoomTransition; 
			//Gata animatia
			if (zoomTransition >= 1) {
				//Intram in nivel
				SceneManager.LoadScene ("GameScene");
			}
		}

			if (isEnteringEndless) {

				//Adaugam in tranzitia de la zoom
				zoomTransition += 1 / zoomDuration * Time.deltaTime;

				//Schimbam marimea + animatie
				menuContainer.localScale = Vector3.Lerp( Vector3.one , Vector3.one * 5 ,enterLevelZoomCurve.Evaluate(zoomTransition)) ;

				//Facem ca, Canvas-ul sa urmeze zoom-ul
				//zoom in centru
				Vector3 newDesiredPosition = desiredMenuPosition * 5;

				RectTransform rt = levelPanel.GetChild (Manager.Instance.currentLevel).GetComponent<RectTransform> ();
				newDesiredPosition = rt.anchoredPosition3D * 5;

				menuContainer.anchoredPosition3D = Vector3.Lerp (desiredMenuPosition, newDesiredPosition, enterLevelZoomCurve.Evaluate (zoomTransition));

				fadeGroup.alpha = zoomTransition; 
				//Gata animatia
				if (zoomTransition >= 1) {
					//Intram in nivel
					SceneManager.LoadScene("GameScene");
				}
		}

	}

	private void InitWiki()
	{
		if (skins == null)
			Debug.Log ("Butoanele nu merg(no skins assigned in inspector)");

		//Pentru fiecare transform cauta buton + adauga onclick-event
		int i = 0;
		foreach (Transform t in skins) 
		{
			int cIndex = i;
			Button b = t.GetComponent<Button> ();
			b.onClick.AddListener (() => OnEnemySelect (cIndex));
			b.onClick.AddListener (() => click.Play ());
			//Render modelul selectat in preview
			b.onClick.AddListener (() => render.RenderPlayer(cIndex));

			i++;
		}

	}

	private void InitLevel()
	{
		if (levelPanel == null)
		Debug.Log ("Butoanele nu merg(no levels assigned in inspector)");

		//Pentru fiecare transform cauta buton + adauga onclick-event
		int i = 0;
		foreach (Transform t in levelPanel) {
			int cIndex = i;
			Button b = t.GetComponent<Button> ();
			b.onClick.AddListener (() => OnLevelSelect (cIndex));
			b.onClick.AddListener (() => click.Play ());
			Image img = t.GetComponent<Image> ();

			//Este nivelul deblochat?
			if (i <= SaveManager.Instance.state.completedLevel) {
				//Este deblochat, dar nu stim daca este jucat
				if (i == SaveManager.Instance.state.completedLevel) {
					//Nu este jucat
					img.color = Color.white;
				} else {
					//nivel jucat
					img.color = Color.green;
				
				}

			} 

			else {
				//Nivel nedeblochat
				b.interactable = false;
				img.color = Color.grey;

			}
				
			i++;
		}
	
	}

	//pozitioneaza camera pe focus
	private void SetCameraTo(int index)
	{
		NavigateTo (index);
		menuContainer.anchoredPosition3D = desiredMenuPosition;
	}

	public void NavigateTo (int menuIndex)
	{	
		switch (menuIndex) {

		//0 este meniul principal

		default:
		case 0:
			desiredMenuPosition = Vector3.zero;
			menuCam.Hide();
			break;

		//1 este magazinul de skin-uri

		case 1:
			desiredMenuPosition = new Vector3 (1280, -400, 0);
			menuCam.Show();
			break;

		//2 este selectorul de nivele

		case 2:
			desiredMenuPosition = new Vector3 (-1280, -400, 0);
			menuCam.Hide();
			break;

		//3 este endless mode
		case 3:
			menuCam.Hide();
			break;
		
		//4 este magazinul de upgrade-uri
		case 4:
			desiredMenuPosition = new Vector3 (1280, 400, 0);
			menuCam.Hide ();
			break;
		}
	}

	private void SetUpgrade(int index)
	{
		if (index >= 0 && index <= 3) 
			SaveManager.Instance.state.activeUp [0] = index;
		else if (index > 3 && index <= 7)
			SaveManager.Instance.state.activeUp [1] = index;
		else if (index > 7 && index <= 11)
			SaveManager.Instance.state.activeUp [2] = index;

		//Schimba Text-ul
		SkinBuyText.text = "Current";

		//Salveaza Preferinte
		SaveManager.Instance.Save();
	}

	private void UpdateGoldText()
	{
		goldText.text = SaveManager.Instance.state.gold.ToString();

	}
		

	//Butonie

	public void OnClickPlay()
	{
		Debug.Log ("Butonul de joc a fost apasat---bravo!");
		NavigateTo (2);
		click.Play ();
	}

	public void OnClickWiki()
	{
		Debug.Log ("Butonul de cumparat a fost vandut!");
		render.RenderPlayer (0);
		click.Play ();
		NavigateTo (1);
	}

	public void OnUpgradeSelect(int cIndex, int UIndex)
	{
		Debug.Log ("Ai ales upgrade-ul cu numarul" + cIndex);

		//Deja selectat -> nu se intampla nimic
		if (selectedUpgradeIndex == cIndex)
			return;

		//Facem iconita mai mare cand selectam
		upgrades.GetChild(cIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.110f;

		//Celalalt upgrade devine normal
		upgrades.GetChild(selectedUpgradeIndex).GetComponent<RectTransform>().localScale = Vector3.one;

		//Foloseste upgrade-ul selectat
		selectedUpgradeIndex = cIndex ;

		//Schimba Text-ul
		if (SaveManager.Instance.IsUpgradeOwned (UIndex) == true) {

			if(SaveManager.Instance.state.activeUp[0] == UIndex || SaveManager.Instance.state.activeUp[1] == UIndex || SaveManager.Instance.state.activeUp[2] == UIndex) {
				SkinBuyText.text = "Current";
			} 

			else {
				SkinBuyText.text = "Select";
			}
		} 

		else {
			SkinBuyText.text = "Buy: " + upgradeCost[UIndex].ToString();
		}
	}

	public void OnEnemySelect(int cIndex)
	{
		Debug.Log ("Ai ales upgrade-ul cu numarul" + cIndex);

		//Deja selectat -> nu se intampla nimic
		if (selectedEnemyIndex == cIndex)
			return;

		//Facem iconita mai mare cand selectam
		skins.GetChild(cIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.110f;


		//Celalalt upgrade devine normal
		skins.GetChild(selectedEnemyIndex).GetComponent<RectTransform>().localScale = Vector3.one;

		//Foloseste upgrade-ul selectat
		selectedEnemyIndex = cIndex ;
	
		
	}

	public void OnBuy()
	{
		Debug.Log( SaveManager.Instance.state.activeUp[0].ToString() + SaveManager.Instance.state.activeUp[1].ToString() +  SaveManager.Instance.state.activeUp[2].ToString() );
		//Este upgrade-ul selectat cumparat?
		if (SaveManager.Instance.IsUpgradeOwned (upgrade.index)) {
			SetUpgrade (upgrade.index);
		}

		else {
			//Sa incercam sa cumparam atunci
			if (SaveManager.Instance.BuyUpgrade (upgrade.index, upgradeCost [upgrade.index])) {
				//Succes!
				//Sound feedback
				SetUpgrade (upgrade.index);
				//seteaza upgrade-ul in functie de statutul sau (cumparat/nedetinut)
				upgrades.GetChild(selectedUpgradeIndex).GetComponent<Image>().color = Color.white;
				//Update Bani
				UpdateGoldText();
			}

			else 
			{
				//Treci la munca!
				//Sound feedback
				Debug.Log ("Treci la munca!");
			}
		
		}
	}

	private void OnLevelSelect(int cIndex)
	{
		Debug.Log ("Ai selectat un nivel cu numarul" + cIndex);
		//Deja selectat -> nu se intampla nimic
		if (selectedLevel == cIndex)
			return;
		else {
			
			//Facem iconita mai mare cand selectam
			levelPanel.GetChild (cIndex).GetComponent<RectTransform> ().localScale = Vector3.one * 1.111f;

			//Celalalt upgrade devine normal
			levelPanel.GetChild (selectedLevel).GetComponent<RectTransform> ().localScale = Vector3.one;

			selectedLevel = cIndex;

			Manager.Instance.currentLevel = cIndex;

		}


	}

	public void OnBack()
	{
		Debug.Log ("Ai dorinta de a te intoarce inapoi?");
		NavigateTo (0);
		click.Play ();

	}

	public void SelectLevel()
	{
		Debug.Log ("Bafta la joc!");
		Manager.Instance.playToMenu = 1;
		isEnteringLevel = true;

	}

	//conditie pentru animatia panoului cu setari
	public bool enterSettingsMenu = false;

	public void SettingButton()
	{
		click.Play ();

		if (enterSettingsMenu == false) {
			enterSettingsMenu = true;

		} 

		else 
		{ enterSettingsMenu = false;
			
		}

	}

	public void OnMusic()
	{
		SaveManager.Instance.state.music = !SaveManager.Instance.state.music;
		if (SaveManager.Instance.state.music == true) {
			musicOn.text = "Music ON";
			Music.GetComponent<Image> ().sprite = audioOn;
			backMenu.mute = false;
		} 

		else {
			Music.GetComponent<Image> ().sprite = audioOFF;
			musicOn.text = "Music OFF";
			backMenu.mute = true;
		}

	}

	public void OnSound()
	{
		SaveManager.Instance.state.soundE = !SaveManager.Instance.state.soundE;
		if (SaveManager.Instance.state.soundE == true) {
			soundEOn.text = "Sound Effects ON";
			SoundE.GetComponent<Image> ().sprite = audioOn;
			click.mute = false;

		} 

		else {
			SoundE.GetComponent<Image> ().sprite = audioOFF;
			soundEOn.text = "Sound Effects OFF";
			click.mute = true;
		}
	}

	public void OnTilt()
	{
		//Folosim sau nu accelerometer
		SaveManager.Instance.state.usingAccelerometer = !SaveManager.Instance.state.usingAccelerometer;
		accButton.Accelerometer ();
		if (SaveManager.Instance.state.usingAccelerometer == true) {
			accText.text = "Tilt Control (On)";

				sensArrows.SetActive (false);
				sensTilt.SetActive (true);

		} 

		else { accText.text = "Tilt Control (Off)";
				sensArrows.SetActive (true);
				sensTilt.SetActive (false);
		} 
		SaveManager.Instance.Save ();
	}

	public void OnEndless()
	{
		Manager.Instance.playToMenu = 0;
		isEnteringEndless = true;
	}

	public void OnClickUpgrade()
	{
		NavigateTo (4);
		click.Play ();
	}
	//Reseteaza salvarea
	public void ResetSave()
	{
		Debug.Log ("Reset");
		reset = true;
		PlayerPrefs.DeleteKey ("save");
	}




}
