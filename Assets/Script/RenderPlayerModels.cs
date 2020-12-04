using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPlayerModels : MonoBehaviour {

	public Transform models;
	public FloatUpAndDown floatSpaceship;

	public void RenderPlayer(int ind)
	{ int i = 0;
		int cInd;
		foreach (Transform t in models) {

			cInd = i;
			MeshRenderer m = t.GetComponent<MeshRenderer> ();
			floatSpaceship = t.GetComponent<FloatUpAndDown>();

			if (ind == cInd)
			{m.enabled = true;
				floatSpaceship.enabled = true;}
			else
			{m.enabled = false;
				floatSpaceship.enabled = false;}
		
			i++;
		}

	}

}
