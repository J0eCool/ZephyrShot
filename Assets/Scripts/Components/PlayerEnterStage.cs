using UnityEngine;

public class PlayerEnterStage : MonoBehaviour {
    public Vector3 distToMove;
    public float timeToMove;

    private Vector3 startPos;
    private Timer moveTimer;

    private PlayerHealth health;

    void Start() {
        startPos = transform.position;
        moveTimer = new Timer();

        health = GetComponent<PlayerHealth>();
    }

    void Update() {
        if (health.IsInactive()) {
            moveTimer.Reset();
        }
        if (!moveTimer.HasPassed(timeToMove)) {
            transform.position = Vector3.Lerp(startPos, startPos + distToMove, moveTimer.Elapsed() / timeToMove);
        }
    }
}
