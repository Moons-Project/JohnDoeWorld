using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  public static GameManager instance;

  public ItemManager itemManager { get { return ItemManager.instance; } }
  public InventoryManager inventoryManager { get { return InventoryManager.instance; } }
  public SysUIManager sysUIManager { get { return SysUIManager.instance; } }
  public TilemapManager tilemapManager { get { return TilemapManager.instance; } }
  public GlobalEffectManager globalEffectManager { get { return GlobalEffectManager.instance; } }

  void Awake() {
    if (instance == null) {
      instance = this;
    }
    DontDestroyOnLoad(gameObject.transform.parent.gameObject);
  }

  public void SwitchScene(string sceneName) {
    globalEffectManager._SwitchScene(sceneName);
  }
}