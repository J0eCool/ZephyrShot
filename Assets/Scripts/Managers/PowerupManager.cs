using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupManager : BaseManagerComponent<PowerupManager> {
	public GameObject[] powerups;

	public GameObject TrySpawnRandomPowerup(Vector3 pos) {
		GameObject spawnedObj = null;
		if (Random.Range(0.0f, 100.0f) < 15.0f) {
			GameObject baseObj = powerups[Random.Range(0, powerups.Length)];
			spawnedObj = GameObject.Instantiate(baseObj, pos, Quaternion.identity) as GameObject;
		}
		return spawnedObj;
	}
}
