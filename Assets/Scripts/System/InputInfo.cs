using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputInfo {
  public float horizontalAxis;
  public bool jumpButtonDown;
  public bool fire1ButtonDown;

  public static InputInfo getInputInfo() {
    InputInfo inputInfo = new InputInfo();
    inputInfo.horizontalAxis = Input.GetAxis("Horizontal");
    inputInfo.jumpButtonDown = Input.GetButtonDown("Jump");
    inputInfo.fire1ButtonDown = Input.GetButtonDown("Fire1");
    return inputInfo;
  }
}