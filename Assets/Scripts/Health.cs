using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour {
	public int maxHealth = 10;
	
	private int health;

	void Start() {
		health = maxHealth;
	}

    void OnTriggerEnter2D(Collider2D other) {
        Bullet b = other.gameObject.GetComponent<Bullet>();
        if (b != null) {
			health -= b.damage;
            GameObject.Destroy(b.gameObject);

			if (health <= 0) {
				GameObject.Destroy(gameObject);
			}
        }
    }
}
