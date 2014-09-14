using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	public float spawnTime = 2.0f;

	private float timer = 0.0f;

	void Update() {
		timer += Time.deltaTime;
		if (timer >= spawnTime) {
			timer -= spawnTime;
			GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		}
	}
}
