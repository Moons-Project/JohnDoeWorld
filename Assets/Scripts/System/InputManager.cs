using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
  public GameObject player;
  public static InputManager instance;

  private GameManager manager;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start() {
    manager = GameManager.instance;
  }

  // Update is called once per frame
  void Update() {

  }

  void FixedUpdate() {
    if (player) {
      player.GetComponent<Creature>().act(InputInfo.getInputInfo());
    }
    if (Input.GetButtonDown("Console")) {
      manager.sysUIManager.ToggleCheat();
    }
  }
}