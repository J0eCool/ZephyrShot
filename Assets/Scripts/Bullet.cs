using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	public int damage { get; private set; }
	public float homingSpeed = 0.0f;

	private Vector3 velocity;
	private GameObject homingTarget = null;

	public void Init(Vector3 vel, int dam = 5) {
		damage = dam;
		velocity = vel;
	}

	void Update() {
		if (homingSpeed > 0.0f) {
			if (homingTarget == null) {
				FindTarget();
			}

			if (homingTarget != null) {
				var pos = homingTarget.transform.position;
				var delta = pos - transform.position;
				var angle = Time.deltaTime * homingSpeed * Mathf.Deg2Rad;
				velocity = Vector3.RotateTowards(velocity, delta, angle, 0.0f);
				UpdateRotation();
			}
		}

		transform.position += velocity * Time.deltaTime;
	}

	private void UpdateRotation() {
		float angle = Mathf.Atan2(velocity.y, velocity.x);
		transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90.0f, Vector3.forward);
	}

	private void FindTarget() {
		var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies.Length > 0) {
			homingTarget = Util.ChooseRandom(enemies);
		}
	}
}
