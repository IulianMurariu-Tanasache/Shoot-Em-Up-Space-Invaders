using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityTilt : MonoBehaviour {

	public Text sensitivity;

	private void Start()
	{
		sensitivity.text = " Sensitivity \n   Tilt : " + SaveManager.Instance.state.sensitivityTilt.ToString ();

	}

	public void OnRight()
	{
		if(SaveManager.Instance.state.sensitivityTilt <= 50)
		{	
			SaveManager.Instance.state.sensitivityTilt += 1;
			sensitivity.text = " Sensitivity \n   Tilt : " + SaveManager.Instance.state.sensitivityTilt.ToString ();
		}

		if (SaveManager.Instance.state.sensitivityArrows > 50) {

			SaveManager.Instance.state.sensitivityArrows = 50;
			sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();
		}
	}

	public void OnLeft()
	{
		if (SaveManager.Instance.state.sensitivityTilt >= 10) 
		{
			SaveManager.Instance.state.sensitivityTilt -= 1;
			sensitivity.text = " Sensitivity \n   Tilt : " + SaveManager.Instance.state.sensitivityTilt.ToString ();
		}

		if (SaveManager.Instance.state.sensitivityArrows < 10) 
		{
			SaveManager.Instance.state.sensitivityArrows = 10;
			sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();
		}
	}
}
