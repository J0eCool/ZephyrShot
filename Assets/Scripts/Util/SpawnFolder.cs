using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnFolder {
    public static void SetParent(GameObject obj, string folderName) {
        GameObject folderObject = GameObject.Find(folderName);
        if (folderObject == null) {
            folderObject = new GameObject(folderName);
        }
        obj.transform.parent = folderObject.transform;
    }
}
