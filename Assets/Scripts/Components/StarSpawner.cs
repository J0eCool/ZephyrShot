using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarSpawner : MonoBehaviour {
    public GameObject starPrefab;
    public float spawnRate = 5.0f;
    public float minSpawnSize = 0.25f;
    public float baseMoveSpeed = 20.0f;

    private List<StarData> spawnedStars = new List<StarData>();
    private Timer spawnTimer;
    private BoxCollider2D box;

    private class StarData {
        public GameObject star;
        public float size;
    }

	void Start() {
        box = GetComponent<BoxCollider2D>();
        spawnTimer = new Timer();
	}
	
	void Update() {
        if (spawnTimer.HasPassed(1.0f / spawnRate)) {
            spawnTimer.Reset();
            Vector3 offset = box.size;
            offset.x *= Random.Range(-0.5f, 0.5f);
            offset.y *= Random.Range(-0.5f, 0.5f);
            Vector3 pos = transform.position + offset;

            var star = GameObject.Instantiate(starPrefab, pos, Quaternion.identity) as GameObject;
            SpawnFolder.SetParent(star, "Stars");
            var size = Random.Range(minSpawnSize, 1.0f);
            star.transform.localScale *= size;
            var data = new StarData();
            data.star = star;
            data.size = size;
            spawnedStars.Add(data);
        }

        for (int i = spawnedStars.Count - 1; i >= 0; i--) {
            var star = spawnedStars[i];
            star.star.transform.position += Vector3.down * star.size * baseMoveSpeed * Time.deltaTime;

            if (star.star.transform.position.y < -transform.position.y) {
                spawnedStars.RemoveAt(i);
                GameObject.Destroy(star.star);
            }
        }
	}
}
