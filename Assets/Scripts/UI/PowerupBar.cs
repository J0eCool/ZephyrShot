using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupBar : SlicedBar {
    public PlayerFire fire;

    void Update() {
        SetFill(fire.GetPowerupFraction());
    }
}
