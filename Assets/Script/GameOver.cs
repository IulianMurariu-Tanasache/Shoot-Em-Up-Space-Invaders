using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public Animator animator;
	public GameController movement;


	private void Update()
	{
		if (movement.dead == true) 
			animator.SetBool ("uDed", true);

		else 
			animator.SetBool ("uDed", false);
	}
}
