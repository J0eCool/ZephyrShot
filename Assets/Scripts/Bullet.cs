using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	private Vector3 velocity;

	public void Init(Vector3 vel) {
		velocity = vel;
		float angle = Mathf.Atan2(vel.y, vel.x);
		transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90.0f, Vector3.forward);
	}

	void Update() {
		transform.position += velocity * Time.deltaTime;
	}
}
