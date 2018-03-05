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

  void Awake() {
    manager = GameManager.instance;
  }

  // Use this for initialization
  void Start() {
    boxCollider2D = GetComponent<BoxCollider2D>();
    rectTransform = GetComponent<RectTransform>();
  }

  void OnMouseOver() {
    descriptionId = manager.sysUIManager.ShowDescription(inventoryItemIndex);
  }

  void OnMouseExit() {
    manager.sysUIManager.UnshowDescription(descriptionId);
  }

  void OnMouseDown() {
    manager.inventoryManager.UseItem(inventoryItemIndex);
  }

  public void UpdateData() {
    InventroyItem invItem = InventoryManager.instance.inventoryDataset[inventoryItemIndex];
    // 改变图片
    var icons = GetComponentsInChildren<Image>();
    Image icon = null;
    foreach (var i in icons) {
      if (i.gameObject != this.gameObject) {
        icon = i;
        break;
      }
    }
    Sprite sprite = null;
    JsonManager.instance.spriteDict.TryGetValue(invItem.item.idName, out sprite);
    icon.sprite = sprite;
    if (icon.sprite == null) {
      icon.color = new Color(1f, 1f, 1f, 0f);
    } else {
      icon.color = new Color(1f, 1f, 1f, 1f);
    }

    // 改变数字
    Text text = GetComponentInChildren<Text>();
    text.text = invItem.count.ToString();
  }

  void OnGUI() {
    boxCollider2D.size = rectTransform.sizeDelta;
  }
}