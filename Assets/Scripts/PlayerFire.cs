using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	public GameObject bulletPrefab;
	public float fireRate = 2.5f;
	public float bulletSpeed = 7.5f;

	private float timer = 0.0f;

	void Update() {
		timer += Time.deltaTime;
		float timePerShot = 1.0f / fireRate;
		if (timer >= timePerShot) {
			timer -= timePerShot;
			GameObject obj = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
			obj.GetComponent<Bullet>().Init(new Vector3(0.0f, bulletSpeed, 0.0f));

			GameObject bulletFolder = GameObject.Find("Bullets");
			if (bulletFolder == null) {
				bulletFolder = new GameObject("Bullets");
			}
			obj.transform.parent = bulletFolder.transform;
		}
	}
}
