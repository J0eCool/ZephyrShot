using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	public GameObject bulletPrefab;
	public float fireRateBase = 2.5f;
	public float bulletSpeedBase = 7.5f;
	public int numBulletsBase = 1;
	public float spreadPerBullet = 5.0f;
	
	public float speedPerLevel = 5.0f;

	private int powerupLevel = 0;

	private float timer = 0.0f;

	void Update() {
		timer += Time.deltaTime;
		float fireRate = GetFireRate();
		float timePerShot = 1.0f / fireRate;
		if (timer >= timePerShot) {
			timer -= timePerShot;

			float totalAngle = numBulletsBase * spreadPerBullet * Mathf.Deg2Rad;
			float w = (numBulletsBase - 1) / 2.0f;
			for (int i = 0; i < numBulletsBase; i++) {
				float angle = (float)(i - w) / numBulletsBase * totalAngle;
				Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeedBase;

				GameObject obj = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
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
		return fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, powerupLevel);
	}

	public void CollectPowerup(Powerup powerup) {
		powerupLevel++;
	}
}
