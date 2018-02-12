using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

  public static GameManager instance;

  public ItemManager itemManager { get { return ItemManager.instance; } }
  public InventoryManager inventoryManager { get { return InventoryManager.instance; } }
  public SysUIManager sysUIManager { get { return SysUIManager.instance; } }
  public TilemapManager tilemapManager { get { return TilemapManager.instance; } }

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }
}