﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour {
	public Vector3 velocity;

	void Update() {
		transform.position += velocity * Time.deltaTime;
	}
}
