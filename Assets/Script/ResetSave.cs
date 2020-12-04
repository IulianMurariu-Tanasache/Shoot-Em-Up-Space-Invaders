using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSave : MonoBehaviour {

	public Animator animator;
	public MenuScene menu;

	private void Update()
	{	
		
		if (menu.reset == true) {
			
			animator.SetBool ("Resetsave", true);
			menu.reset = false;
		} 

		else if (menu.reset == false)
			animator.SetBool ("Resetsave", false);

		menu.reset = false;
	}

}
