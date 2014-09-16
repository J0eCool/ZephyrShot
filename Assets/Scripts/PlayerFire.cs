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
	private int shotsFired = 0;
	private int bulletsFired = 0;
	private BoxCollider2D box;


	void Start() {
		shotTimer = new Timer();
		timeFiring = new Timer();
		box = GetComponent<BoxCollider2D>();
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
			shotsFired++;
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
			Vector3 offset = (i - w) * gun.offsetPerBullet;
			offset.y *= Mathf.Sign(i - w);
			offset += gun.offset;
			offset.Scale(box.size * 0.5f);
			offset.Scale(transform.localScale);

			Vector3 vel = Vector3.up * bulletSpeed;

			GameObject obj = GameObject.Instantiate(gun.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(vel);

			SpawnFolder.SetParent(obj, "Bullets");
			bulletsFired++;
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

			Vector3 offset = gun.offset;
			offset.Scale(box.size * 0.5f);
			offset.Scale(transform.localScale);

			Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

			GameObject obj = GameObject.Instantiate(gun.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(vel);

			SpawnFolder.SetParent(obj, "Bullets");
			bulletsFired++;
		}
	}

	private void ShootSide() {
		int numBullets = NumBullets();
		float bulletSpeed = BulletSpeed();

		float totalAngle = (numBullets * gun.spreadPerBullet + gun.spreadBase) * Mathf.Deg2Rad;
		//for (int i = 0; i < numBullets; i++) {
			float angle = 0.0f;// Mathf.Sin(rot + timeFiring.Elapsed() * angPerTime) * totalAngle;

			Vector3 offset = gun.offset;
			if (bulletsFired % 2 == 0) {
				offset.x *= -1.0f;
			}
			offset.Scale(box.size * 0.5f);
			offset.Scale(transform.localScale);

			Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

			GameObject obj = GameObject.Instantiate(gun.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(vel);

			SpawnFolder.SetParent(obj, "Bullets");
			bulletsFired++;
		//}
	}

	public float FireRate() {
		float rate = gun.fireRateBase * Mathf.Pow(1.0f + speedPerLevel / 100.0f, powerupLevel + bulletLevel);
		if (gun.type == GunType.SideShooting) {
			rate *= bulletLevel + 1;
		}
		return rate;
	}

	public int NumBullets() {
		if (gun.type == GunType.SideShooting) {
			return 1;
		}
		return gun.numBulletsBase + bulletLevel;
	}

	public float BulletSpeed() {
		if (gun.type == GunType.Straight) {
			return gun.bulletSpeedBase * (1.0f + bulletLevel / 25.0f);
		}
		return gun.bulletSpeedBase;
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
