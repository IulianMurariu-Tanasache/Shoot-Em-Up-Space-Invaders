using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject explosion;
	public float speed;
	private float z;
	public GameObject myObject;
	private float direction;
	private Vector3 desiredPosition;
	public GameObject bolt;
	public Transform spawnShot;
	public float fireRate = 2f;
	public GameObject controller;
	public GameObject coin;

	void Start ()
	{
		myObject = GameObject.Find("Player");
		z = myObject.GetComponent<PlayerMovement> ().player.position.z + 180;
		InvokeRepeating ("Fire", fireRate, fireRate);
		controller = GameObject.Find ("GameController");
	}

	private void Update()
	{	
		z = myObject.GetComponent<PlayerMovement> ().player.position.z + 180;
		if(transform.position.z < z)
		{
			direction = z - transform.position.z;
			transform.Translate (Vector3.back * direction);
		}

		if (myObject.GetComponent<PlayerMovement> ().dead == true) {
			Destroy(gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}


 	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "enemyBolt" || other.tag == "Asteroid" || other.tag == "coin")
			return;
		else {
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			controller.GetComponent<GameController> ().coordX = transform.position.x;
			controller.GetComponent<GameController> ().enemyDead = true;
			Quaternion rotation = Quaternion.Euler (90, 0, 0);
			Instantiate (coin, transform.position, rotation);
		}	
	}

	private void Fire()
	{
		Instantiate (bolt, spawnShot.position, spawnShot.rotation);
	}


}