using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPlayerShopPreview : MonoBehaviour
{
	private Vector3 startPosition;
	private Quaternion startRotation;

	private Vector3 desiredPosition;
	private Quaternion desiredRotation;

	public Transform shopWaypoint;

	private void Start()
	{
		startPosition = desiredPosition = transform.localPosition;
		startRotation = desiredRotation = transform.localRotation;

	}

	private void Update()
	{
		float x = transform.localPosition.x;
		transform.localPosition = Vector3.Lerp (transform.localPosition, desiredPosition + new Vector3(0,x,0) * 0.01f, 0.1f);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, desiredRotation, 0.1f);
	}

	public void Hide()
	{
		desiredPosition = startPosition;
		desiredRotation = startRotation;
	}

	public void Show ()
	{
		desiredPosition = shopWaypoint.localPosition;
		desiredRotation = shopWaypoint.localRotation;
	}

}
