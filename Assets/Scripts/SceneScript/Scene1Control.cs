using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Scene1Control : MonoBehaviour {

  public Tilemap ladderTilemap;
  public Tilemap platformTilemap;
  public GameObject creaturePrefab;
  public Cinemachine.CinemachineVirtualCamera virtualCamera;

  GameManager manager;

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;

    manager.tilemapManager.ladderTilemap = ladderTilemap;
    manager.tilemapManager.platformTilemap = platformTilemap;

    // TODO: 根据游戏进度判断应该创建哪个角色

    // 创建creature GameObject
    GameObject creature = Instantiate(creaturePrefab);
    virtualCamera.LookAt = creature.transform;
    // TODO: 设置合适的位置、恢复前一个场景的状态

    // TODO: 根据游戏进度选择此处剧本(?)
  }

  // Update is called once per frame
  void Update() {

  }
}