using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public int id;

	public Item(int id) {
		this.id = id;
	}

	// 对目标使用的效果
	public void UseTo(GameObject target) {}
}
