using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver3 : MonoBehaviour {

	public Animator animator;
	public GameController movement;
	public GameScene game;
	public GameObject exitButton;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject fireButton;
	public GameObject gameoverButton;
	public GameObject health1;
	public GameObject health2;
	public GameObject shield;

	private void Start()
	{
		exitButton.SetActive (true);
		leftButton.SetActive (true);
		rightButton.SetActive (true);
		fireButton.SetActive (true);
		gameoverButton.SetActive (false);
	}

	private void Update()
	{
		if (movement.dead == true) {
			animator.SetBool ("touch", true);
			exitButton.SetActive (false);
			leftButton.SetActive (false);
			rightButton.SetActive (false);
			fireButton.SetActive (false);
			gameoverButton.SetActive (true);
			shield.SetActive (false);
			health1.SetActive (false);
			health2.SetActive (false);

		} 

		else {
			animator.SetBool ("touch", false);
		
		
		}
			
	}


	public void OnGameOverClick()
	{
		if( animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 )
		{
			game.ExitScene();
		}

	}
}
