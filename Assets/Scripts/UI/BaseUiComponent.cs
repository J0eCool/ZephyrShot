using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseUiComponent : MonoBehaviour {
	private tk2dUIItem uiItem;

	void Awake() {
		uiItem = GetComponent<tk2dUIItem>();
	}

	void OnEnable() {
		uiItem.OnClick += OnClick;
	}

	void OnDisable() {
		uiItem.OnClick -= OnClick;
	}

	protected virtual void OnClick() { }
}
