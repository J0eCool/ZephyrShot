using UnityEngine;

public class FlashOnHit : MonoBehaviour {
    public float timePerFlash = 0.1f;
    private Timer timer;
    private bool isFlashing = false;

    private PlayerHealth health;

    void Start() {
        timer = new Timer();
        health = GetComponent<PlayerHealth>();
    }

    void Update() {
        if (isFlashing) {
            if (timer.HasPassed(timePerFlash)) {
                renderer.enabled = !renderer.enabled;
                timer.Reset();
            }

            if (health.IsInvincible()) {
                renderer.enabled = true;
                isFlashing = false;
            }
        }
    }

    void OnHit() {
        isFlashing = true;
        renderer.enabled = false;
        timer.Reset();
    }
}
