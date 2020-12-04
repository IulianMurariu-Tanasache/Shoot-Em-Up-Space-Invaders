using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMove : MonoBehaviour {

	public bool goRight = false;

	public void OnPointerDown()
	{
		goRight = true;
	}

	public void OnPointerUp()
	{
		goRight = false;
	}
}
