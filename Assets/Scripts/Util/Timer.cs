using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer {
    private float startTime;
    public Timer() {
        Reset();
    }

    public float Elapsed() {
        return Time.realtimeSinceStartup - startTime;
    }

    public bool HasPassed(float t) {
        return t < Elapsed();
    }

    public void Advance(float dt) {
        AdvanceWithoutReset(dt);

		if (HasPassed(dt)) {
			Reset();
		}
    }

	public void AdvanceWithoutReset(float dt) {
		startTime += dt;
	}

    public void Reset() {
        startTime = Time.realtimeSinceStartup;
    }
}
