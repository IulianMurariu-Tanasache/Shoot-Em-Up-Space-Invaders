using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour  {

	public PlayerCamera pCam;
	public Transform player;
	private Vector3 desiredPosition;
	public GameObject leftB, rightB, fire;
	private bool allowLeft = true, allowRight = true;
	private CharacterController controller;
	public LeftMove left;
	public RightMove right;
	public Fire shoot;
	public float fireRate;
	private float nextFire;
	public GameObject bolt;
	public Transform spawnBolt;
	public GameObject damageBolt;
	public GameObject spreadBolt;
	public GameObject minigunBolt;
	public GameObject defaultBolt;
	public float RotZ;
	public bool shield;
	public GameObject heart;
	public GameObject miniBolt;
	public bool dead;
	public GameController control;

	private void Start()
	{
		desiredPosition = player.transform.position;
		controller = GetComponent<CharacterController> ();
		if (SaveManager.Instance.state.usingAccelerometer == true) {

			leftB.SetActive (false);
			rightB.SetActive (false);
		}

		//daca nu folosim accelerometer
		else if (SaveManager.Instance.state.usingAccelerometer == false) {

			leftB.SetActive (true);
			rightB.SetActive (true);
		}

		switch (SaveManager.Instance.state.activeUp [0]) {
		default:
		case 0:
			fireRate = 0.3f;
			bolt = defaultBolt;
			break;
		case 1:
			fireRate = 0.35f;
			bolt = damageBolt;
			break;
		case 2:
			fireRate = 0.25f;
			bolt = minigunBolt;
			break;
		case 3:
			fireRate = 0.3f;
			bolt = spreadBolt;
			break;
		}

		switch (SaveManager.Instance.state.activeUp [1]) {
		default:
		case 4:
			shield = false;
			heart.SetActive (false);
			break;
		case 5:
			shield = true;
			heart.SetActive (false);
			break;
		case 6:
			shield = false;
			heart.SetActive (true);
			break;
		case 7:
			shield = false;
			heart.SetActive (false);
			break;
		}

		switch (SaveManager.Instance.state.activeUp [2]) {
		default:
		case 4:
			break;
		case 5:
			break;
		case 6:
			break;
		case 7:
			break;
		}

	}

	private void Update()
	{
		dead = control.dead;
		Debug.Log( SaveManager.Instance.state.activeUp[0].ToString() + SaveManager.Instance.state.activeUp[1].ToString() +  SaveManager.Instance.state.activeUp[2].ToString() );
		allowLeft = allowRight = true;
		//Folosim accelerometer daca putem
		Manager.Instance.GetPlayerInput();

		Vector3 move = transform.forward;

		if (SaveManager.Instance.state.usingAccelerometer == false) {
		
			if (left.goLeft == true) {
			
				if (player.transform.position.x < -170) {
				
					player.transform.position = new Vector3 (-170, 20, player.transform.position.z );
					allowLeft = false;
				}
				
				if (allowLeft == true) {

				
					player.transform.Translate (-SaveManager.Instance.state.sensitivityArrows / 10, 20, 0);
					player.position = new Vector3 (player.position.x, 20, player.position.z);

				}

			} else if (right.goRight == true) {
			
				if (player.transform.position.x > 170) {

					player.transform.position = new Vector3 (170, 20, player.transform.position.z);
					allowRight = false;
				}

				if (allowRight == true) {


					player.transform.Translate (SaveManager.Instance.state.sensitivityArrows / 10, 20, 0);
					player.position = new Vector3 (player.position.x, 20, player.position.z);

				}
			}
		}

		controller.Move (move * Time.deltaTime * pCam.speedCam );
	
		if (shoot.fire == true && nextFire < Time.time) {
			if (bolt == spreadBolt) {
					nextFire = Time.time + fireRate;
					Vector3 ceva = new Vector3 (0, 15, 0) + spawnBolt.transform.eulerAngles;
					Quaternion secondShotRot1 = Quaternion.Euler (ceva);
					ceva = new Vector3 (0, -15, 0) + spawnBolt.transform.eulerAngles;
					Quaternion secondShotRot2 = Quaternion.Euler (ceva);
					ceva = new Vector3 (0, 25, 0) + spawnBolt.transform.eulerAngles;
					Quaternion thirdShotRot1 = Quaternion.Euler (ceva);
					ceva = new Vector3 (0, -25, 0) + spawnBolt.transform.eulerAngles;
					Quaternion thirdShotRot2 = Quaternion.Euler (ceva);
					Instantiate (miniBolt, spawnBolt.position, spawnBolt.rotation);	
					Instantiate (miniBolt, spawnBolt.position, secondShotRot1);	
					Instantiate (miniBolt, spawnBolt.position, secondShotRot2);	
					Instantiate (miniBolt, spawnBolt.position, thirdShotRot1);	
					Instantiate (miniBolt, spawnBolt.position, thirdShotRot2);	

			} 

			else {
				nextFire = Time.time + fireRate;
				Instantiate (bolt, spawnBolt.position, spawnBolt.rotation);	
			}
		}
	

	}
}
	
		
		

