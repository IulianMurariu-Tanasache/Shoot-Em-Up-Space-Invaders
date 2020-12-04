using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOnTrigger : MonoBehaviour {

	public Shield shield;
	public CapsuleCollider collider;

	private void Start()
	{
		if (shield.move.shield == true) {
			collider.enabled = true;
		} else
			collider.enabled = false;
	}

	private void Update()
	{
		if (shield.active == true) {
			collider.enabled = true;
		} else
			collider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bolt" || other.tag == "Player")
			return;
		else{
			Destroy (other.gameObject);
			shield.shieldParticles.GetComponent<MeshRenderer> ().enabled = false;
			shield.active = false;
			collider.enabled = false;
		}	
	}
}
