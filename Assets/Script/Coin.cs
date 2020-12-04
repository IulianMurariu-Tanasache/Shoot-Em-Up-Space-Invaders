using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public float tumble = 7.0f;
	public Rigidbody rb;
	public float speed;
	private float z;
	public GameObject myObject;

	void Start ()
	{
		myObject = GameObject.Find("Player");
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
		z = myObject.GetComponent<PlayerMovement> ().player.position.z;
	}

	private void FixedUpdate()
	{
		rb.AddForce (Vector3.back * speed);
	}

	private void Update()
	{
		if (transform.position.z < z - 30) {
			Destroy (gameObject);
		}

		if (myObject.GetComponent<PlayerMovement> ().dead == true) {
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Destroy (gameObject);
			SaveManager.Instance.state.gold++;
		
		}
	}


}
