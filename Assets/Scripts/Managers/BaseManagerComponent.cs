using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseManagerComponent<T> : MonoBehaviour where T : MonoBehaviour{
	public static T instance { get; set; }

	void Start() {
		instance = this as T;
	}
}
