using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

  private float damage;
  public float Damage { get { return damage; } private set {damage = value;} }

  private SkillItem skill;
  private int skillIndex;
  private Info info;

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void UseSkill(int index, Info info) {
    skillIndex = index - 1;
    this.info = info;
    this.skill = GameManager.instance.skillDict.itemDict[info.skillList[skillIndex]];
    GetComponent<Animator>().Play(skill.idName);
  }

  public void startSKill() {
    BasicInfo finalInfo = info.getFinalInfo();
    Damage = skill.damage[info.skillLevel[skillIndex]] * finalInfo.sword;
  }

  public void endSkill() {
    Damage = 0.0f;
  }

  void OnTriggerEnter2D(Collider2D other) {
    // 檢測tag為Body且不是自身 
    if (other.CompareTag("Body") && other.gameObject != info.gameObject) {
      GameManager.instance.musicManager.PlaySE("main_menu_hover");
    }
  }
}