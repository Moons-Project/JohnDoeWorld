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

  public delegate void KeyHandler();
  public event KeyHandler PressSubmit;
  public event KeyHandler PressJump;

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
    if (Input.GetButtonDown("Submit")) {
      if (PressSubmit != null) {
        PressSubmit();
      }
      manager.dialogManager.OnDialogPress();
    }
    if (Input.GetButtonDown("Jump")) {
      if (PressJump != null) {
        PressJump();
      }
      manager.scriptManager.SkipScript();
    }
    if (Input.GetButtonDown("Interact")) {
      if (player) {
        // 搜索Interactable
        var collider = Physics2D.OverlapCircle(player.transform.position, 1f, LayerMask.GetMask("Interactable"));

        if (collider) {
          Debug.Log(collider.name);
          var inter = collider.GetComponent<SignBehavior>();
          if (inter != null)
            inter.Interact(player.GetComponent<Creature>());
          else
            Debug.Log("No inter");
        } else {
          Debug.Log("No collider");
        }
      }
    }
  }
}