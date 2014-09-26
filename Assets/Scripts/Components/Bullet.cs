using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	public int damage { get; private set; }

	protected Vector3 velocity;

	public void Init(Vector3 vel, int dam = 5) {
		damage = dam;
		velocity = vel;
		UpdateRotation();
	}

	void Update() {
		OnUpdate();

		transform.position += velocity * Time.deltaTime;
	}

	protected virtual void OnUpdate() { }

	protected void UpdateRotation() {
		float angle = Mathf.Atan2(velocity.y, velocity.x);
		transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90.0f, Vector3.forward);
	}
}
