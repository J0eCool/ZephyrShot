using UnityEngine;

public class SideGun : GunBase {
    public SideGun(GunData data, GameObject player) : base(data, player) { }

    public override int NumBullets() {
        return 1;
    }

    public override float FireRate() {
        return base.FireRate() * (gun.bulletLevel + 1);
    }

    protected override Vector3 OffsetForBullet(int i, int numBullets) {
        Vector3 offset = gun.data.offset;
        if (gun.bulletsFired % 2 == 0) {
            offset.x *= -1.0f;
        }
        return offset;
    }
}
