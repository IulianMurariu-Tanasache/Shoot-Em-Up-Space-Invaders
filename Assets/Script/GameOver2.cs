using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver2 : MonoBehaviour {


	public Animator animator;
	public GameController movement;

	private void Update()
	{
		if (movement.dead == true) 
			animator.SetBool ("gameOver", true);

		else 
			animator.SetBool ("gameOver", false);
	}
}
