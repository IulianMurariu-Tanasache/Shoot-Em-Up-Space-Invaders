using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LeftMove : MonoBehaviour {

	public bool goLeft = false;

	public void OnPointerDown()
	{
		goLeft = true;
	}

	public void OnPointerUp()
	{
		goLeft = false;
	}
}
