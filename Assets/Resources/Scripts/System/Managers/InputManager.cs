using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
  public GameObject player;
  public static InputManager instance;

  private GameManager manager;
  private bool disableInput = false;

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

  public void DisableInput() {
    disableInput = true;
  }

  public void RestartInput() {
    disableInput = false;
  }


  void FixedUpdate() {
    if (!disableInput) {
      if (player) {
        player.GetComponent<Creature>().Act(InputInfo.getInputInfo());
      }
    }
    if (Input.GetButtonDown("Console")) {
        manager.sysUIManager.ToggleCheat();
      }
    if (Input.GetButtonDown("Cancel")) {
        manager.sysUIManager.ToggleTab();
    }
  }
}