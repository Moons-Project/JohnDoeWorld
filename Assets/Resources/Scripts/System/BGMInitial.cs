using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMInitial : MonoBehaviour {

public string JohnBGM = "John";
public string JaneBGM = "Jane";
public string SlarmBGM = "slarm_go_away";


  // Use this for initialization
  void Start() {
    GameManager.instance.musicManager.PlayBGM(GetBGM());
  }

  // Update is called once per frame
  void Update() {

  }

  string GetBGM() {
    var type = GameManager.instance.saveDataManager.saveData.playerRoleType;
    var list = new string[] {JohnBGM, JaneBGM, SlarmBGM};
    return list[(int)type];
  }
}