using UnityEngine;

public class StraightGun : GunBase {
    public StraightGun(GunData data, GameObject player) : base(data, player) { }

    public override float BulletSpeed() {
        return gun.data.bulletSpeedBase * (1.0f + gun.bulletLevel / 25.0f);
    }

    protected override Vector3 OffsetForBullet(int i, int numBullets) {
        float w = (numBullets - 1) / 2.0f;
        Vector3 offset = (i - w) * gun.data.offsetPerBullet;
        offset.y *= Mathf.Sign(i - w);
        offset += gun.data.offset;
        return offset;
    }
}
