using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemBehavior : MonoBehaviour {

	[HideInInspector]
	public int inventoryItemIndex = -1;

	private BaseSceneManager manager;

	private int descriptionId = -1;

	// Use this for initialization
	void Start() {
		manager = BaseSceneManager.instance;
	}

	// Update is called once per frame
	void Update() {

	}

	void OnMouseOver() {
		descriptionId = manager.ShowDescription(inventoryItemIndex);
	}

	void OnMouseExit() {
		manager.UnshowDescription(descriptionId);
	}

	public void UpdateData() {
		InventroyItem invItem = manager.inventoryDataset[inventoryItemIndex];
		// TODO: 改变图片
		// 改变数字
		Text text = GetComponentInChildren<Text>();
		text.text = invItem.count.ToString();
	}
}