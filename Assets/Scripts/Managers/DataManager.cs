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

    public GunRuntimeData(GunData gun) {
        data = gun;
        shotTimer = new Timer();
        timeFiring = new Timer();
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
}
