using UnityEngine;

public class HomingBullet : Bullet {
    public float homingSpeed = 0.0f;

    private GameObject homingTarget = null;

    protected override void OnUpdate() {
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
    
    private void FindTarget() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0) {
            homingTarget = Util.ChooseRandom(enemies);
        }
    }
}