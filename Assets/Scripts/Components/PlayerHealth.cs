using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    public int health { get; private set; }

    public float invincibleTime = 2.0f;
    public float inactiveTime = 1.0f;
    public GameObject explosionPrefab;

    private Timer hitTimer;

    void Start() {
        health = maxHealth;
        hitTimer = new Timer();
        hitTimer.AdvanceWithoutReset(invincibleTime);
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
        hitTimer.Reset();
        GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SendMessage("OnHit", options: SendMessageOptions.DontRequireReceiver);
    }

    private bool CanBeHit() {
        return IsInvincible();
    }

    public bool IsInvincible() {
        return hitTimer.HasPassed(invincibleTime);
    }

    public bool IsInactive() {
        return !hitTimer.HasPassed(inactiveTime);
    }
}
