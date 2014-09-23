using UnityEngine;

public class Util {
    public static T ChooseRandom<T>(T[] list) {
        return list[Random.Range(0, list.Length)];
    }

    public static void SetChildrenActive(Transform transform, bool active) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}
