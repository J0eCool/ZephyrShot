using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveWhenLeavingScreen : MonoBehaviour {
	private readonly static Vector3 screenScaleVec = new Vector3(1.0f / Screen.width, 1.0f / Screen.height);
	private const float kScreenBoundary = 0.05f;
	private const float kSafeScreenBoundary = 1.5f;

	private bool didEnterScreen = false;

	void Update() {
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		point.Scale(screenScaleVec);
		
		float destroyBoundary = kScreenBoundary;
		if (!didEnterScreen) {
			if (IsWithinBorder(point, kScreenBoundary)) {
				didEnterScreen = true;
			}
			destroyBoundary = kSafeScreenBoundary;
		}

		if (!IsWithinBorder(point, destroyBoundary)) {
			GameObject.Destroy(gameObject);
		}
	}

	private static bool IsWithinBorder(Vector3 point, float boundary) {
		return point.x > -boundary && point.x < 1.0f + boundary
			&& point.y > -boundary && point.y < 1.0f + boundary;
	}
}
