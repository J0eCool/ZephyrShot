using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : SlicedBar {
	public Health health;

	void Update() {
		SetFill(health.GetHealthFraction());
	}
}
