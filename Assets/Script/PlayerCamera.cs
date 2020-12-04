using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public float speedCam = 10.0f;
	public float orthographicSize ;
	public float aspect ;
	void Start()
	{
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			Camera.main.nearClipPlane, Camera.main.farClipPlane);
	}

	private void Update()
	{

		transform.Translate (Vector3.up* Time.deltaTime * speedCam);
	}
}
