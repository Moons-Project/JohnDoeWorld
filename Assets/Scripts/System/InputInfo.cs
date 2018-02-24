using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputInfo {
  public float horizontalAxis;
  public float verticalAxis;
  public bool jumpButtonDown;
  public bool fire0ButtonDown;
  public bool fire1ButtonDown;
  public bool fire2ButtonDown;
  public bool fire3ButtonDown;
  public bool fire4ButtonDown;
  public bool fire5ButtonDown;

  public static InputInfo getInputInfo() {
    InputInfo inputInfo = new InputInfo();
    inputInfo.horizontalAxis = Input.GetAxis("Horizontal");
    inputInfo.verticalAxis = Input.GetAxis("Vertical");
    inputInfo.jumpButtonDown = Input.GetButtonDown("Jump");
    inputInfo.fire0ButtonDown = Input.GetButtonDown("Fire0");
    inputInfo.fire1ButtonDown = Input.GetButtonDown("Fire1");
    inputInfo.fire2ButtonDown = Input.GetButtonDown("Fire2");
    inputInfo.fire3ButtonDown = Input.GetButtonDown("Fire3");
    inputInfo.fire4ButtonDown = Input.GetButtonDown("Fire4");
    inputInfo.fire5ButtonDown = Input.GetButtonDown("Fire5");
    return inputInfo;
  }
}