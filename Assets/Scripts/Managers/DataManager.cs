using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataManager : BaseManagerComponent<DataManager> {
    public GunData[] gunTypes;
}

[Serializable]
public class GunData {
    public GameObject bulletPrefab;
    public GunType type;
    public float fireRateBase;
    public float bulletSpeedBase;
    public int numBulletsBase;
    public float spreadPerBullet;
    public float spreadBase;
    public Vector3 offset;
    public Vector3 offsetPerBullet;
}

public enum GunType {
    Straight,
    Helixing,
	SideShooting,
}

public class GunRuntimeData {
    public GunData data;
    public Timer shotTimer;
    public Timer timeFiring;
    public int shotsFired = 0;
    public int bulletsFired = 0;

    public int powerupLevel = 0;
    public int bulletLevel = 0;

    public float speedPerLevel = 7.0f;

    private Transform transform;
    private BoxCollider2D box;

    public GunRuntimeData(GunData gun, GameObject player) {
        data = gun;
        shotTimer = new Timer();
        timeFiring = new Timer();

        transform = player.transform;
        box = player.GetComponent<BoxCollider2D>();
    }

    public float FireRate() {
        float rate = data.fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, powerupLevel + bulletLevel);
        if (data.type == GunType.SideShooting) {
            rate *= bulletLevel + 1;
        }
        return rate;
    }

    public int NumBullets() {
        if (data.type == GunType.SideShooting) {
            return 1;
        }
        return data.numBulletsBase + bulletLevel;
    }

    public float BulletSpeed() {
        if (data.type == GunType.Straight) {
            return data.bulletSpeedBase * (1.0f + bulletLevel / 25.0f);
        }
        return data.bulletSpeedBase;
    }

    public float MaxLevel() {
        return 3 + bulletLevel;
    }

    public void CollectPowerup(Powerup powerup) {
        if (powerupLevel < 25) {
            powerupLevel++;
        }

        if (powerupLevel > MaxLevel() && bulletLevel < 4) {
            powerupLevel = 0;
            bulletLevel++;
        }
    }

    public void TryShoot() {
        float fireRate = FireRate();
        float timePerShot = 1.0f / fireRate;
        if (shotTimer.HasPassed(timePerShot)) {
            shotTimer.Advance(timePerShot);

            Shoot();
            shotsFired++;
        }
    }

    private void Shoot() {
        switch (data.type) {
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
            Vector3 offset = (i - w) * data.offsetPerBullet;
            offset.y *= Mathf.Sign(i - w);
            offset += data.offset;
            offset.Scale(box.size * 0.5f);
            offset.Scale(transform.localScale);

            Vector3 vel = Vector3.up * bulletSpeed;

            GameObject obj = GameObject.Instantiate(data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet>().Init(vel);

            SpawnFolder.SetParent(obj, "Bullets");
            bulletsFired++;
        }
    }

    private void ShootHelixing() {
        int numBullets = NumBullets();
        float bulletSpeed = BulletSpeed();

        float totalAngle = (numBullets * data.spreadPerBullet + data.spreadBase) * Mathf.Deg2Rad;
        for (int i = 0; i < numBullets; i++) {
            float rot = Mathf.PI * 2.0f * i / numBullets;
            float angPerTime = Mathf.PI;
            float angle = Mathf.Sin(rot + timeFiring.Elapsed() * angPerTime) * totalAngle;

            Vector3 offset = data.offset;
            offset.Scale(box.size * 0.5f);
            offset.Scale(transform.localScale);

            Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

            GameObject obj = GameObject.Instantiate(data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet>().Init(vel);

            SpawnFolder.SetParent(obj, "Bullets");
            bulletsFired++;
        }
    }

    private void ShootSide() {
        float bulletSpeed = BulletSpeed();

        Vector3 offset = data.offset;
        if (bulletsFired % 2 == 0) {
            offset.x *= -1.0f;
        }
        offset.Scale(box.size * 0.5f);
        offset.Scale(transform.localScale);

        Vector3 vel = Vector3.up * bulletSpeed;

        GameObject obj = GameObject.Instantiate(data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
        obj.GetComponent<Bullet>().Init(vel);

        SpawnFolder.SetParent(obj, "Bullets");
        bulletsFired++;
    }
}
