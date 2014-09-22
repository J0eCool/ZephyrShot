using UnityEngine;
using System;

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
    Cardinal,
}
