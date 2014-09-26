using UnityEngine;
using System.Collections.Generic;

public class PlayerHealthBar : MonoBehaviour {
    public PlayerHealth player;
    public GameObject heartPrefab;
    public Vector3 offset;

    private List<GameObject> hearts = new List<GameObject>();

    void Update() {
        while (hearts.Count < player.health) {
            var pos = transform.position + offset * hearts.Count;
            GameObject obj = GameObject.Instantiate(heartPrefab, pos, Quaternion.identity) as GameObject;
            hearts.Add(obj);
        }

        while (hearts.Count > player.health && hearts.Count > 0) {
            var obj = hearts[hearts.Count - 1];
            GameObject.Destroy(obj);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }
}