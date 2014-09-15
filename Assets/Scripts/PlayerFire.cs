using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	public float speedPerLevel = 5.0f;

	private GunData gun;

	private int powerupLevel = 0;

	private float timer = 0.0f;

	void Start() {
		gun = DataManager.instance.gunTypes[0];
	}

	void Update() {
		timer += Time.deltaTime;
		float fireRate = GetFireRate();
		float timePerShot = 1.0f / fireRate;
		if (timer >= timePerShot) {
			timer -= timePerShot;

			float totalAngle = gun.numBulletsBase * gun.spreadPerBullet * Mathf.Deg2Rad;
			float w = (gun.numBulletsBase - 1) / 2.0f;
			for (int i = 0; i < gun.numBulletsBase; i++) {
				float angle = (float)(i - w) / gun.numBulletsBase * totalAngle;
				Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * gun.bulletSpeedBase;

				GameObject obj = GameObject.Instantiate(gun.bulletPrefab, transform.position, Quaternion.identity) as GameObject;
				obj.GetComponent<Bullet>().Init(vel);

				GameObject bulletFolder = GameObject.Find("Bullets");
				if (bulletFolder == null) {
					bulletFolder = new GameObject("Bullets");
				}
				obj.transform.parent = bulletFolder.transform;
			}
		}
	}

	public float GetFireRate() {
		return gun.fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, powerupLevel);
	}

	public void CollectPowerup(Powerup powerup) {
		powerupLevel++;
	}
}
