using UnityEngine;

public class Util {
    public static T ChooseRandom<T>(T[] list) {
        return list[Random.Range(0, list.Length)];
    }
}
