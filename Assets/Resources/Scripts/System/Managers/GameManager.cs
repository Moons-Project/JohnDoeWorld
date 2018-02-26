using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

  public static GameManager instance;

  public GameObject bulletPrefab;

  public SaveDataManager saveDataManager { get { return SaveDataManager.instance; } }
  public InputManager inputManager { get { return InputManager.instance; } }
  public SkillDict skillDict { get { return SkillDict.instance; } }
  public ItemManager itemManager { get { return ItemManager.instance; } }
  public InventoryManager inventoryManager { get { return InventoryManager.instance; } }
  public SysUIManager sysUIManager { get { return SysUIManager.instance; } }
  public TilemapManager tilemapManager { get { return TilemapManager.instance; } }
  public GlobalEffectManager globalEffectManager { get { return GlobalEffectManager.instance; } }
  public MusicManager musicManager { get { return MusicManager.instance; } }
  public DialogManager dialogManager { get { return DialogManager.instance; } }
  public ScriptManager scriptManager { get { return ScriptManager.instance; } }

  public string lastVDoorName = "";

  void Awake() {
    if (instance == null) {
      instance = this;
    }
    DontDestroyOnLoad(gameObject.transform.parent.gameObject);
    // test 
    saveDataManager.Save();
  }

  public void SwitchScene(string sceneName) {
    globalEffectManager._SwitchScene(sceneName);
  }
}