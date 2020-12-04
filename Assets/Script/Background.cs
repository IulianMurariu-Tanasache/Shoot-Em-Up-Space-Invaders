using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public Transform background1;
	public Transform background2;
	private bool next = true;
	public Transform cam;
	private float currentheight = 367.0f;

	private void Update()
	{
		if (currentheight < cam.position.z - 50) {
			if (next == true) {
				background1.localPosition = new Vector3 (0, background1.localPosition.y + 734, 0);
			} 
			else {
				background2.localPosition = new Vector3 (0, background2.localPosition.y + 734, 0);
			}
			currentheight += 367;
			next = !next;
			
		}

		if(currentheight > cam.position.z + 367 - 50)
		{
			if (next == true) {
				background2.localPosition = new Vector3 (0, background2.localPosition.y - 374, 0);
			} 
			else {
				background1.localPosition = new Vector3 (0, background1.localPosition.y - 734, 0);
			}
			currentheight -= 367;
			next = !next;

		}

	}
}
