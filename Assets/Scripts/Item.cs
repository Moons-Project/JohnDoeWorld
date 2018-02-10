using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public string name;
	public string description;

	public Item() {
		name = "";
		description = "无描述";
	}

	public Item(string name, string description) {
		this.name = name;
		this.description = description;
	}

	// 对目标使用的效果
	public void UseTo(GameObject target) {}
}
