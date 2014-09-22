using UnityEngine;

public class CardinalGun : GunBase {
    public CardinalGun(GunData data, GameObject player) : base(data, player) { }

    public override int NumBullets() {
        return 1;
    }

    public override float FireRate() {
        return base.FireRate() * (gun.bulletLevel + 1);
    }

    protected override Vector3 VelocityForBullet(int i, int numBullets) {
        int totToFire = gun.bulletLevel + 1;
        float angle = Mathf.PI * 2.0f * gun.bulletsFired / totToFire + Mathf.PI;
        // float angPerTime = Mathf.PI;
        // float totalAngle = (numBullets * gun.data.spreadPerBullet + gun.data.spreadBase) * Mathf.Deg2Rad;
        // float angle = Mathf.Sin(rot + gun.timeFiring.Elapsed() * angPerTime) * totalAngle;
        return new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * BulletSpeed();
    }
}
