using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityArrows : MonoBehaviour {

	public Text sensitivity;

	private void Start()
	{
		sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();

	}

	public void OnRight()
	{
		if(SaveManager.Instance.state.sensitivityArrows <= 40)
		{	
			SaveManager.Instance.state.sensitivityArrows += 1;
			sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();
		}

		if (SaveManager.Instance.state.sensitivityArrows > 40) {
		
			SaveManager.Instance.state.sensitivityArrows = 40;
			sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();
		}
	}

	public void OnLeft()
	{
		if (SaveManager.Instance.state.sensitivityArrows >= 5) 
		{
			SaveManager.Instance.state.sensitivityArrows -= 1;
			sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();
		}


		if (SaveManager.Instance.state.sensitivityArrows < 5) 
		{
			SaveManager.Instance.state.sensitivityArrows = 5;
			sensitivity.text = " Sensitivity \n  Arrows : " + SaveManager.Instance.state.sensitivityArrows.ToString ();
		}
	}

}
