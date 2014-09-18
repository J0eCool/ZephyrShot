using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	private int gunIndex = 0;
	private int sideIndex = 1;

	private GunRuntimeData[] gunList;

	void Start() {
		gunList = new GunRuntimeData[DataManager.instance.gunTypes.Length];
		for (int i = 0; i < DataManager.instance.gunTypes.Length; i++) {
			gunList[i] = new GunRuntimeData(DataManager.instance.gunTypes[i], gameObject);
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {			
			sideIndex = (sideIndex + 1) % gunList.Length;
			gunIndex = (gunIndex + 1) % gunList.Length;

			GunForIndex(gunIndex).shotTimer.Reset();
			GunForIndex(sideIndex).shotTimer.Reset();
		}

		GunForIndex(gunIndex).TryShoot();
		GunForIndex(sideIndex).TryShoot();
	}

	private GunRuntimeData GunForIndex(int i) {
		return gunList[i];
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
