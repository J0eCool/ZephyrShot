using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    public int health { get; private set; }

    public float invincibleTime = 1.0f;

    private Timer invincibilityTimer;

    void Start() {
        health = maxHealth;
        invincibilityTimer = new Timer();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!CanBeHit()) {
            return;
        }

        if (other.gameObject.tag == "Enemy") {
            TakeHit();
        }

        Bullet b = other.gameObject.GetComponent<Bullet>();
        if (b != null) {
            TakeHit();
            GameObject.Destroy(b.gameObject);
        }
    }

    private void TakeHit() {
        health--;
        invincibilityTimer.Reset();
    }

    private bool CanBeHit() {
        return invincibilityTimer.HasPassed(invincibleTime);
    }
}
