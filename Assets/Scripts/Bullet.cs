using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	private Vector3 velocity;

	public void Init(Vector3 vel) {
		velocity = vel;
	}
	
	void Update() {
		transform.position += velocity * Time.deltaTime;
		if (transform.position.y > 10) {
			GameObject.Destroy(gameObject);
		}
	}
}
