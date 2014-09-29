using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowMouse : MonoBehaviour {
	public float followSpeed = 5.0f;

	new private Camera camera;
	private PlayerHealth health;

	void Start() {
		camera = Camera.main;
		health = GetComponent<PlayerHealth>();
	}
	
	void Update() {
		if (health.IsInactive()) {
			return;
		}

		Vector3 inputPos = Input.mousePosition;
		inputPos.x = Mathf.Clamp(inputPos.x, 0.0f, Screen.width);
		inputPos.y = Mathf.Clamp(inputPos.y, 0.0f, Screen.height);
		Vector3 mousePos = camera.ScreenToWorldPoint(inputPos);
		Vector3 moveDelta = mousePos - transform.position;
		moveDelta.z = 0.0f;
		float moveDist = followSpeed * Time.deltaTime;
		if (moveDist * moveDist > moveDelta.sqrMagnitude) {
			moveDist = moveDelta.magnitude;
		}
		Vector3 moveVec = moveDist * moveDelta.normalized;
		transform.position += moveVec;
	}
}
