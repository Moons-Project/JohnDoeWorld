using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

  public GameObject player;
  public static InputManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  void FixedUpdate() {
    float horizontalAxis = Input.GetAxis("Horizontal");
    bool jumpButtonDown = Input.GetButtonDown("Jump");
    if (player) {
      player.GetComponent<Move>().move(horizontalAxis, jumpButtonDown);
    }
  }
}