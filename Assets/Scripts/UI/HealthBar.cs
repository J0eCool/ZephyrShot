using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour {
	public tk2dSlicedSprite sprite;
	public Health health;
	public bool hideWhenFull = true;

	private float baseWidth;
	private float cachedHealth = -1.0f;

	void Start() {
		baseWidth = sprite.dimensions.x;
	}

	void Update() {
		float t = health.GetHealthFraction();
		if (t != cachedHealth) {
			cachedHealth = t;
			if (hideWhenFull && t >= 1.0f) {
				SetChildrenActive(false);
			}
			else {
				SetChildrenActive(true);
				sprite.dimensions = new Vector2(baseWidth * t, sprite.dimensions.y);
			}
		}
	}

	void SetChildrenActive(bool active) {
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(active);
		}
	}
}
