using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	public float spawnTime = 2.0f;

	private Timer timer;

	void Start() {
		timer = new Timer();
	}

	void Update() {
		if (timer.HasPassed(spawnTime)) {
			timer.Reset();
			
			GameObject obj = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
			SpawnFolder.SetParent(obj, "Enemies");
		}
	}
}
