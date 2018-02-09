using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBehavior : MonoBehaviour {

	public Item item;

	private BaseSceneManager manager;

	private int descriptionId = -1;

	// Use this for initialization
	void Start () {
		manager = BaseSceneManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver() {
		descriptionId = manager.ShowDescription(item);
	}

	void OnMouseExit() {
		manager.UnshowDescription(descriptionId);
	}
}
