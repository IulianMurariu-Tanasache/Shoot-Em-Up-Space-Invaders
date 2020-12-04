using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {

	Animator animator;
	public MenuScene menu;

	// Use this for initialization
	private void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool ("enterSettings", false);
	}
	
	private void Update()
	{
		if (menu.enterSettingsMenu == true) {
			animator.SetBool ("enterSettings", true);
			} 

		else {
			animator.SetBool ("enterSettings", false);
		}
	}
}
