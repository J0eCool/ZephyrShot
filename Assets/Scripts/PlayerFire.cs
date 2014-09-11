using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFire : MonoBehaviour {
	public GameObject bulletPrefab;
	public float fireRate = 2.5f;
	public float bulletSpeed = 7.5f;
	public int numBullets = 1;
	public float spreadPerBullet = 5.0f;

	private float timer = 0.0f;

	void Update() {
		timer += Time.deltaTime;
		float timePerShot = 1.0f / fireRate;
		if (timer >= timePerShot) {
			timer -= timePerShot;

			float totalAngle = numBullets * spreadPerBullet * Mathf.Deg2Rad;
			float w = (numBullets - 1) / 2.0f;
			for (int i = 0; i < numBullets; i++) {
				float angle = (float)(i - w) / numBullets * totalAngle;
				Vector3 vel = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * bulletSpeed;

				GameObject obj = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
				obj.GetComponent<Bullet>().Init(vel);

				GameObject bulletFolder = GameObject.Find("Bullets");
				if (bulletFolder == null) {
					bulletFolder = new GameObject("Bullets");
				}
				obj.transform.parent = bulletFolder.transform;
			}
		}
	}
}
