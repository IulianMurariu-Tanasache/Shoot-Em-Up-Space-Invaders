using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float tumble = 7.0f;
	public Rigidbody rb;
	public GameObject explosion;
	public float speed;
	private float z;
	public GameObject myObject;
	private float time = 0;
	private int chance;
	public GameObject coin;

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
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Shield" || other.tag == "Enemy" || other.tag == "coin")
			return;
		else 
			Destroy (other.gameObject);
		Destroy(gameObject);
		Instantiate (explosion, transform.position, transform.rotation);
		time += Time.deltaTime;
		if (time > 1.0f)
			Destroy (explosion);
		chance = Random.Range (1, 5);
		if (chance == 3) {
			Quaternion rotation = Quaternion.Euler (90, 0, 0);;
			Instantiate (coin, transform.position, rotation);
		}
	}

}
