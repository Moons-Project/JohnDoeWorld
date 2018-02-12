using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemBehavior : MonoBehaviour {

  [HideInInspector]
  public int inventoryItemIndex = -1;

  private GameManager manager;

  private int descriptionId = -1;

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;
  }

  void OnMouseOver() {
    descriptionId = manager.sysUIManager.ShowDescription(inventoryItemIndex);
  }

  void OnMouseExit() {
    manager.sysUIManager.UnshowDescription(descriptionId);
  }

  public void UpdateData() {
    InventroyItem invItem = manager.inventoryManager.inventoryDataset[inventoryItemIndex];
    // TODO: 改变图片
    // 改变数字
    Text text = GetComponentInChildren<Text>();
    text.text = invItem.count.ToString();
  }
}