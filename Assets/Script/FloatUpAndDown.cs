using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpAndDown : MonoBehaviour {

	public bool floatup = false;
	public float time = 0;

	private void Update()
	{
		if (floatup) {
			FloatingUp ();
		} 

		else {
			FloatingDown ();
		}
	}

	private void FloatingUp()
	{
		
		transform.position = new Vector3 (transform.position.x, transform.position.y + 2 * Time.deltaTime, transform.position.z);
		time += Time.deltaTime;
		if (time >= 1) {
			floatup = false;
			time = 0;
		}
	}

	private void FloatingDown()
	{
		
		transform.position = new Vector3 (transform.position.x, transform.position.y - 2 * Time.deltaTime, transform.position.z);
		time += Time.deltaTime;
		if (time >= 1) {
			floatup = true;
			time = 0;
		}

	}
}
