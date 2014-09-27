using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	public float sideGunFirePercent = 40.0f;
	public int startGunIndex = 0;
	public bool useSideGun = true;

	private int gunIndex = 0;
	private int sideIndex = 1;

	private GunBase[] gunList;

	private PlayerHealth health;

	void Start() {
		gunList = new GunBase[DataManager.instance.gunTypes.Length];
		for (int i = 0; i < DataManager.instance.gunTypes.Length; i++) {
			gunList[i] = GunBase.Build(DataManager.instance.gunTypes[i], gameObject);
		}
		gunIndex = Mathf.Clamp(startGunIndex, 0, gunList.Length - 1);
		health = GetComponent<PlayerHealth>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			sideIndex = (sideIndex + 1) % gunList.Length;
			gunIndex = (gunIndex + 1) % gunList.Length;

			GunForIndex(gunIndex).gun.shotTimer.Reset();
			GunForIndex(sideIndex).gun.shotTimer.Reset();
		}

		GunForIndex(gunIndex).TryShoot();
		if (useSideGun) {
			GunForIndex(sideIndex).TryShoot(sideGunFirePercent / 100.0f);
		}
	}

	private GunBase GunForIndex(int i) {
		return gunList[i];
	}

	public void CollectPowerup(Powerup powerup) {
		foreach (var gun in gunList) {
			if (gun.gun.powerupLevel < MaxPowerupLevel()) {
				gun.gun.powerupLevel++;
			}
		}
	}

	public string StatsText() {
		return "FireRate: " + GunForIndex(gunIndex).FireRate();
	}

	public int MaxPowerupLevel() {
		return 10;
	}

	public float GetPowerupFraction() {
		return (float)GunForIndex(gunIndex).gun.powerupLevel / MaxPowerupLevel();
	}
}
