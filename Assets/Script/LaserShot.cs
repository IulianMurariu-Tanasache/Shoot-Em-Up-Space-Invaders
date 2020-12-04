using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour {

	private Rigidbody rigidbody;
	private float speed = 100;
	public GameObject myObject;
	private float z;

	private void Start()
	{
		myObject = GameObject.Find("Player");
		rigidbody = GetComponent<Rigidbody> ();
		rigidbody.velocity = transform.up * speed;
		z = myObject.GetComponent<PlayerMovement> ().player.position.z;
	}

	private void Update()
	{
		if (transform.position.z > z + 250 || transform.position.z < z - 20) {
			Debug.Log ("NoMoreBolt");
			Destroy (gameObject);
		}
	}



}

