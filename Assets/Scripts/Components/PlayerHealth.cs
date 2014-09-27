using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    public int health { get; private set; }

    public float invincibleTime = 1.0f;
    public GameObject explosionPrefab;

    private Timer invincibilityTimer;

    void Start() {
        health = maxHealth;
        invincibilityTimer = new Timer();
    }

    void Update() {
        renderer.enabled = CanBeHit();
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
        GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private bool CanBeHit() {
        return invincibilityTimer.HasPassed(invincibleTime);
    }
}
