using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UpgradeType {
	FireRate,
	BulletSpeed,
	ShotNumber,
}

public class AdjustGunButton : BaseUiComponent {
	public bool positive;
	public UpgradeType type;
	public float increment;

	PlayerFire fire;

	void Start() {
		fire = GameObject.Find("Player").GetComponent<PlayerFire>();
	}

	protected override void OnClick() {
		float amount = positive ? increment : -increment;

		switch (type) {
		case UpgradeType.BulletSpeed:
			fire.bulletSpeedBase += amount;
			break;
		case UpgradeType.FireRate:
			fire.fireRateBase += amount;
			break;
		case UpgradeType.ShotNumber:
			fire.numBulletsBase += (int)amount;
			break;
		}
	}
}
