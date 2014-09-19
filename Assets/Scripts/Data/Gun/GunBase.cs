using UnityEngine;

public class GunBase {
    public float speedPerLevel = 7.0f;

    private Transform transform;
    private BoxCollider2D box;

    public GunRuntimeData gun;

    public static GunBase Build(GunData data, GameObject player) {
        return new GunBase(data, player);
    }

    private GunBase(GunData data, GameObject player) {
        gun = new GunRuntimeData(data);

        transform = player.transform;
        box = player.GetComponent<BoxCollider2D>();
    }

    public float FireRate() {
        float rate = gun.data.fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, gun.powerupLevel + gun.bulletLevel);
        if (gun.data.type == GunType.SideShooting) {
            rate *= gun.bulletLevel + 1;
        }
        return rate;
    }

    public int NumBullets() {
        if (gun.data.type == GunType.SideShooting) {
            return 1;
        }
        return gun.data.numBulletsBase + gun.bulletLevel;
    }

    public float BulletSpeed() {
        if (gun.data.type == GunType.Straight) {
            return gun.data.bulletSpeedBase * (1.0f + gun.bulletLevel / 25.0f);
        }
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
        switch (gun.data.type) {
        case GunType.Straight:
            ShootStraight();
            break;
        case GunType.Helixing:
            ShootHelixing();
            break;
        case GunType.SideShooting:
            ShootSide();
            break;
        }
    }

    private void ShootStraight() {
        int numBullets = NumBullets();
        float bulletSpeed = BulletSpeed();

        float w = (numBullets - 1) / 2.0f;
        for (int i = 0; i < numBullets; i++) {
            Vector3 offset = (i - w) * gun.data.offsetPerBullet;
            offset.y *= Mathf.Sign(i - w);
            offset += gun.data.offset;
            offset.Scale(box.size * 0.5f);
            offset.Scale(transform.localScale);

            Vector3 vel = Vector3.up * bulletSpeed;

            GameObject obj = GameObject.Instantiate(gun.data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet>().Init(vel);

            SpawnFolder.SetParent(obj, "Bullets");
            gun.bulletsFired++;
        }
    }

    private void ShootHelixing() {
        int numBullets = NumBullets();
        float bulletSpeed = BulletSpeed();

        float totalAngle = (numBullets * gun.data.spreadPerBullet + gun.data.spreadBase) * Mathf.Deg2Rad;
        for (int i = 0; i < numBullets; i++) {
            float rot = Mathf.PI * 2.0f * i / numBullets;
            float angPerTime = Mathf.PI;
            float angle = Mathf.Sin(rot + gun.timeFiring.Elapsed() * angPerTime) * totalAngle;

            Vector3 offset = gun.data.offset;
            offset.Scale(box.size * 0.5f);
            offset.Scale(transform.localScale);

            Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

            GameObject obj = GameObject.Instantiate(gun.data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet>().Init(vel);

            SpawnFolder.SetParent(obj, "Bullets");
            gun.bulletsFired++;
        }
    }

    private void ShootSide() {
        float bulletSpeed = BulletSpeed();

        Vector3 offset = gun.data.offset;
        if (gun.bulletsFired % 2 == 0) {
            offset.x *= -1.0f;
        }
        offset.Scale(box.size * 0.5f);
        offset.Scale(transform.localScale);

        Vector3 vel = Vector3.up * bulletSpeed;

        GameObject obj = GameObject.Instantiate(gun.data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
        obj.GetComponent<Bullet>().Init(vel);

        SpawnFolder.SetParent(obj, "Bullets");
        gun.bulletsFired++;
    }
}