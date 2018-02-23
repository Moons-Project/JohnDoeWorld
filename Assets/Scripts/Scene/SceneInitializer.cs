using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneInitializer : MonoBehaviour {

  public Tilemap ladderTilemap;
  public Tilemap platformTilemap;
  public GameObject creaturePrefab;
  public Cinemachine.CinemachineVirtualCamera virtualCamera;

  [System.Serializable]
  public class VDoorNameToPos {
    // lastVDoorName是上一场景的名字，pos是对应的在该场景的出生点
    public string lastVDoorName;
    public Vector3 pos;
  }

  public VDoorNameToPos[] vDoorNameToSpawnPos;
  public Vector3 defaultSpawnPos = new Vector3();

  GameManager manager;

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;

    manager.tilemapManager.ladderTilemap = ladderTilemap;
    manager.tilemapManager.platformTilemap = platformTilemap;

    // TODO: 根据游戏进度判断应该创建哪个角色

    // 创建creature GameObject
    GameObject creature = Instantiate(creaturePrefab);
    creature.tag = "ControlPlayer";
    Vector3 newPos = defaultSpawnPos;
    foreach (var item in vDoorNameToSpawnPos) {
      if (item.lastVDoorName == manager.lastVDoorName) newPos = item.pos;
    }
    creature.transform.position = newPos;
    virtualCamera.Follow = creature.transform;
    manager.inputManager.player = creature;
    // TODO: 设置合适的位置、恢复前一个场景的状态

    // TODO: 根据游戏进度选择此处剧本(?)
  }

  // Update is called once per frame
  void Update() {

  }
}