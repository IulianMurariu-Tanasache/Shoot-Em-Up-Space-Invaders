using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public bool fire = false;

	public void OnPointerDown()
	{
		fire = true;
		Debug.Log ("Fire");
	}

	public void OnPointerUp()
	{
		fire = false;
	}
}
