using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	private int powerupLevel = 0;
	private int bulletLevel = 0;

	private int gunIndex = 0;
	private int sideIndex = 1;

	private BoxCollider2D box;

	private GunRuntimeData[] gunList;

	void Start() {
		box = GetComponent<BoxCollider2D>();

		gunList = new GunRuntimeData[DataManager.instance.gunTypes.Length];
		for (int i = 0; i < DataManager.instance.gunTypes.Length; i++) {
			gunList[i] = new GunRuntimeData(DataManager.instance.gunTypes[i]);
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {			
			sideIndex = (sideIndex + 1) % gunList.Length;
			gunIndex = (gunIndex + 1) % gunList.Length;

			GunForIndex(gunIndex).shotTimer.Reset();
			GunForIndex(sideIndex).shotTimer.Reset();
		}

		TryShoot(GunForIndex(gunIndex));
		TryShoot(GunForIndex(sideIndex));
	}

	private void TryShoot(GunRuntimeData gun) {
		float fireRate = gun.FireRate();
		float timePerShot = 1.0f / fireRate;
		if (gun.shotTimer.HasPassed(timePerShot)) {
			gun.shotTimer.Advance(timePerShot);

			Shoot(gun);
			gun.shotsFired++;
		}
	}

	private GunRuntimeData GunForIndex(int i) {
		return gunList[i];
	}

	private void Shoot(GunRuntimeData gun) {
		switch (gun.data.type) {
		case GunType.Straight:
			ShootStraight(gun);
			break;
		case GunType.Helixing:
			ShootHelixing(gun);
			break;
		case GunType.SideShooting:
			ShootSide(gun);
			break;
		}
	}

	private void ShootStraight(GunRuntimeData gun) {
		int numBullets = gun.NumBullets();
		float bulletSpeed = gun.BulletSpeed();

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

	private void ShootHelixing(GunRuntimeData gun) {
		int numBullets = gun.NumBullets();
		float bulletSpeed = gun.BulletSpeed();

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

	private void ShootSide(GunRuntimeData gun) {
		int numBullets = gun.NumBullets();
		float bulletSpeed = gun.BulletSpeed();

		// float totalAngle = (numBullets * gun.data.spreadPerBullet + gun.data.spreadBase) * Mathf.Deg2Rad;
		//for (int i = 0; i < numBullets; i++) {
			float angle = 0.0f;// Mathf.Sin(rot + gun.timeFiring.Elapsed() * angPerTime) * totalAngle;

			Vector3 offset = gun.data.offset;
			if (gun.bulletsFired % 2 == 0) {
				offset.x *= -1.0f;
			}
			offset.Scale(box.size * 0.5f);
			offset.Scale(transform.localScale);

			Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

			GameObject obj = GameObject.Instantiate(gun.data.bulletPrefab, transform.position + offset, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(vel);

			SpawnFolder.SetParent(obj, "Bullets");
			gun.bulletsFired++;
		//}
	}

	public void CollectPowerup(Powerup powerup) {
		foreach (var gun in gunList) {
			gun.CollectPowerup(powerup);
		}
	}

	public string StatsText() {
		return "FireRate: " + GunForIndex(gunIndex).FireRate();
	}
}
