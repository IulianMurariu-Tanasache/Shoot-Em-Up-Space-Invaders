using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {

	public bool dead = false;
	public GameObject explosionSpaceship;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bolt" || other.tag == "Shield" || other.tag == "coin") {

			return;
		}

		else {
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosionSpaceship, transform.position, transform.rotation);
			dead = true;
		}
	}
}