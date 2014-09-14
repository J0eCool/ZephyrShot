using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Powerup : MonoBehaviour {
	public float amount = 0.1f;
	public Vector3 velocity;

	void Update() {
		transform.position += velocity * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		PlayerFire player = other.GetComponent<PlayerFire>();
		if (player != null) {
			player.fireRate += amount;
			GameObject.Destroy(gameObject);
		}
	}
}
