using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatText : MonoBehaviour {
	public PlayerFire player;

	private tk2dTextMesh text;

	void Start() {
		text = GetComponent<tk2dTextMesh>();
	}
	
	void Update() {
		text.text = "FireRate: " + player.FireRate();
	}
}
