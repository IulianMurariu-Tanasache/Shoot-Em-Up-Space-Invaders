using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderModelsGame : MonoBehaviour {

	public Transform modelsP;
	public Dead d;
	public GameController move;

	/*public void Start()
	{ int i = 0;
		foreach (Transform t in modelsP) {

			int cInd = i;
			if(i<9)
			{
				MeshRenderer m = t.GetComponent<MeshRenderer> ();
				d = t.GetComponent<Dead> ();
				CapsuleCollider[] myColliders = t.GetComponents<CapsuleCollider> ();

				if (SaveManager.Instance.state.activeSskin == cInd) {
					m.enabled = true;
					d.enabled = true;
					move.die = d;
					foreach (CapsuleCollider c in myColliders)
						c.enabled = true;
				} 

				else {
					m.enabled = false;
					d.enabled = false;
					foreach (CapsuleCollider c in myColliders)
						c.enabled = false;
				}
					
			}

			i++;
		}

	}*/

}
