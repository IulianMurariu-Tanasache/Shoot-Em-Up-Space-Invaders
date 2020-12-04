using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerButton : MonoBehaviour {
	
	public void Accelerometer()
	{
		if (SaveManager.Instance.state.usingAccelerometer == true) {
			transform.localPosition = new Vector3 (-282.1f, -6.4f, 0);
			transform.eulerAngles = new Vector3 (0, 0, 90);
		} 

		else {
			transform.localPosition = new Vector3 (-282.1f, -6.4f, 0);
			transform.eulerAngles = new Vector3 (0, 0, -90);
		}
	}
}
 