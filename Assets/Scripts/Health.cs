using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        Bullet b = other.gameObject.GetComponent<Bullet>();
        if (b != null) {
            GameObject.Destroy(b.gameObject);
        }
    }
}
