using UnityEngine;

public class HelixGun : GunBase {
    public HelixGun(GunData data, GameObject player) : base(data, player) { }

    protected override Vector3 VelocityForBullet(int i, int numBullets) {
        float rot = Mathf.PI * 2.0f * i / numBullets;
        float angPerTime = Mathf.PI;
        float totalAngle = (numBullets * gun.data.spreadPerBullet + gun.data.spreadBase) * Mathf.Deg2Rad;
        float angle = Mathf.Sin(rot + gun.timeFiring.Elapsed() * angPerTime) * totalAngle;
        return new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * BulletSpeed();
    }
}
