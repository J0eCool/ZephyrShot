using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	private static Vector3 screenScaleVec = new Vector3(1.0f / Screen.width, 1.0f / Screen.height);
	private const float kScreenBoundary = 0.05f;

	private Vector3 velocity;

	public void Init(Vector3 vel) {
		velocity = vel;
	}

	void Update() {
		transform.position += velocity * Time.deltaTime;

		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		point.Scale(screenScaleVec);
		if (point.x < -kScreenBoundary || point.x > 1.0f + kScreenBoundary
				|| point.y < -kScreenBoundary || point.y > 1.0f + kScreenBoundary) {
			GameObject.Destroy(gameObject);
		}
	}
}
