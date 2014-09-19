using UnityEngine;

public class GunBase {
    public float speedPerLevel = 7.0f;

    private Transform transform;
    private BoxCollider2D box;

    public GunRuntimeData gun;

    public static GunBase Build(GunData data, GameObject player) {
        switch (data.type) {
        case GunType.Straight:
            return new StraightGun(data, player);
        case GunType.Helixing:
            return new HelixGun(data, player);
        case GunType.SideShooting:
            return new SideGun(data, player);
        }
        return new GunBase(data, player);
    }

    public GunBase(GunData data, GameObject player) {
        gun = new GunRuntimeData(data);

        transform = player.transform;
        box = player.GetComponent<BoxCollider2D>();
    }

    public virtual float FireRate() {
        return gun.data.fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, gun.powerupLevel + gun.bulletLevel);
    }

    public virtual int NumBullets() {
        return gun.data.numBulletsBase + gun.bulletLevel;
    }

    public virtual float BulletSpeed() {
        return gun.data.bulletSpeedBase;
    }

    public float MaxLevel() {
        return 3 + gun.bulletLevel;
    }

    public void CollectPowerup(Powerup powerup) {
        if (gun.powerupLevel < 25) {
            gun.powerupLevel++;
        }

        if (gun.powerupLevel > MaxLevel() && gun.bulletLevel < 4) {
            gun.powerupLevel = 0;
            gun.bulletLevel++;
        }
    }

    public void TryShoot() {
        float fireRate = FireRate();
        float timePerShot = 1.0f / fireRate;
        if (gun.shotTimer.HasPassed(timePerShot)) {
            gun.shotTimer.Advance(timePerShot);

            Shoot();
            gun.shotsFired++;
        }
    }

    private void Shoot() {
        int numBullets = NumBullets();

        for (int i = 0; i < numBullets; i++) {
            Vector3 offset = OffsetForBullet(i, numBullets);
            offset.Scale(box.size * 0.5f);
            offset.Scale(transform.localScale);

            Vector3 vel = VelocityForBullet(i, numBullets);

            GameObject obj = GameObject.Instantiate(gun.data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet>().Init(vel);

            SpawnFolder.SetParent(obj, "Bullets");
            gun.bulletsFired++;
        }
    }

    protected virtual Vector3 OffsetForBullet(int i, int numBullets) {
        return gun.data.offset;
    }

    protected virtual Vector3 VelocityForBullet(int i, int numBullets) {
        return Vector3.up * BulletSpeed();
    }
}