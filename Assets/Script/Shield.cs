using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

	public GameObject shieldParticles;
	public float timer = 0f;
	public float timeActive= 0f;
	public bool active = false;
	public float activeTime = 10f;
	public GameObject shieldB;
	public PlayerMovement move;

	private void Start()
	{
		if (move.shield == true) {
			shieldB.SetActive (true);
		}
	}

	private void Update()
	{

		if (move.shield == true) {
			timer = timer + Time.deltaTime;
			if (active == true) {
				activeTime -= Time.deltaTime;
				if (activeTime <= 0)
					active = false;
			} else {
				activeTime = 10.0f;
				shieldParticles.GetComponent<MeshRenderer> ().enabled = false;
			}

			if (timer - timeActive < 35.0f && timeActive > 0)
				shieldB.GetComponent<Image> ().color = Color.gray;
			else
				shieldB.GetComponent<Image> ().color = Color.white;
		} 

		else {
			shieldParticles.GetComponent<MeshRenderer> ().enabled = false;
			shieldB.SetActive (false);
		}
	}

	public void OnActive()
	{
		if (timer - timeActive > 35.0f || timeActive == 0) {
			shieldParticles.GetComponent<MeshRenderer> ().enabled = true;
			active = true;
			timeActive = timer;
		
		}
	}
		
}
