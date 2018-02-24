using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class InventoryItemBehavior : MonoBehaviour {

  [HideInInspector]
  public int inventoryItemIndex = -1;

  private GameManager manager;

  private BoxCollider2D boxCollider2D;
  private RectTransform rectTransform;

  private int descriptionId = -1;

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;
    boxCollider2D = GetComponent<BoxCollider2D>();
    rectTransform = GetComponent<RectTransform>();
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

  void OnGUI() {
    boxCollider2D.size = rectTransform.sizeDelta;
  }
}