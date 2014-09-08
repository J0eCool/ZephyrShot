using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestButton : BaseUiComponent {
	public string writeStr = "foo";
	private int counter = 0;

	protected override void OnClick() {
		Debug.Log(writeStr + " : " + counter);
		counter++;
	}
}
