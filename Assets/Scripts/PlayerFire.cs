using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	public float speedPerLevel = 7.0f;

	private GunData gun { get { return DataManager.instance.gunTypes[gunIndex]; } }

	private int powerupLevel = 0;
	private int bulletLevel = 0;

	private int gunIndex = 0;

	private Timer shotTimer;
	private Timer timeFiring;

	void Start() {
		shotTimer = new Timer();
		timeFiring = new Timer();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			gunIndex = (gunIndex + 1) % DataManager.instance.gunTypes.Length;
		}

		float fireRate = FireRate();
		float timePerShot = 1.0f / fireRate;
		if (shotTimer.HasPassed(timePerShot)) {
			shotTimer.Advance(timePerShot);

			Shoot();
		}
	}

	private void Shoot() {
		switch (gun.type) {
		case GunType.Straight:
			ShootStraight();
			break;
		case GunType.Helixing:
			ShootHelixing();
			break;
		}
	}

	private void ShootStraight() {
		int numBullets = NumBullets();
		float bulletSpeed = BulletSpeed();

		float w = (numBullets - 1) / 2.0f;
		for (int i = 0; i < numBullets; i++) {
			Vector3 offset = (i - w) * gun.offsetPerBullet + gun.offset;
			if (i % 2 == 0) {
				offset.x *= -1.0f;
			}
			BoxCollider2D box = GetComponent<BoxCollider2D>();
			offset.Scale(box.size * 0.5f);
			offset.Scale(transform.localScale);

			Vector3 vel = Vector3.up * bulletSpeed;

			GameObject obj = GameObject.Instantiate(gun.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(vel);

			SpawnFolder.SetParent(obj, "Bullets");
		}
	}

	private void ShootHelixing() {
		int numBullets = NumBullets();
		float bulletSpeed = BulletSpeed();

		float totalAngle = (numBullets * gun.spreadPerBullet + gun.spreadBase) * Mathf.Deg2Rad;
		for (int i = 0; i < numBullets; i++) {
			float rot = Mathf.PI * 2.0f * i / numBullets;
			float angPerTime = Mathf.PI;
			float angle = Mathf.Sin(rot + timeFiring.Elapsed() * angPerTime) * totalAngle;
			Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

			GameObject obj = GameObject.Instantiate(gun.bulletPrefab, transform.position, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(vel);

			SpawnFolder.SetParent(obj, "Bullets");
		}
	}

	public float FireRate() {
		return gun.fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, powerupLevel + bulletLevel);
	}

	public int NumBullets() {
		return gun.numBulletsBase + bulletLevel;
	}

	public float BulletSpeed() {
		if (gun.type == GunType.Straight) {
			return gun.bulletSpeedBase * (1.0f + bulletLevel / 25.0f);
		}
		return gun.bulletSpeedBase;
	}

	public float MaxLevel() {
		return 10 + bulletLevel;
	}

	public void CollectPowerup(Powerup powerup) {
		powerupLevel++;

		if (powerupLevel > MaxLevel()) {
			powerupLevel = 0;
			bulletLevel++;
		}
	}
}
