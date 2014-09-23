using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlicedBar : MonoBehaviour {
    public tk2dSlicedSprite sprite;
    public bool hideWhenFull = true;

    private float baseWidth;
    private float cachedVal = -1.0f;

    void Start() {
        baseWidth = sprite.dimensions.x;
    }

    public void SetFill(float t) {
        t = Mathf.Clamp01(t);
        if (t != cachedVal) {
            cachedVal = t;

            if (hideWhenFull && t >= 1.0f) {
                Util.SetChildrenActive(transform, false);
            }
            else {
                Util.SetChildrenActive(transform, true);
                sprite.dimensions = new Vector2(baseWidth * t, sprite.dimensions.y);
            }
        }
    }
}
