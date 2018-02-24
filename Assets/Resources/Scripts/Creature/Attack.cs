using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

  private float damage;
  public float Damage { get { return damage; } private set {damage = value;} }

  private int skillIndex;
  private SkillItem skill;
  private Creature creature;

  public void UseSkill(int index, Creature creature) {
    skillIndex = index - 1;
    this.creature = creature;
    this.skill = GameManager.instance.skillDict.itemDict[creature.skillList[skillIndex]];
    GetComponent<Animator>().Play(skill.idName);
  }

  public void startSKill() {
    Damage = skill.damage[creature.skillLevel[skillIndex] - 1] * creature.currentInfo.sword;
  }

  public void endSkill() {
    Damage = 0.0f;
  }

  void OnTriggerEnter2D(Collider2D other) {
    // 檢測tag為Body 且不是自身Creature 
    if (other.CompareTag("Body") && 
        creature != null && 
        creature != other.transform.parent.gameObject) {
      // 音效（動畫）可以和技能相關，故放置在此
      GameManager.instance.musicManager.PlaySE("main_menu_hover");
      other.transform.parent.gameObject.GetComponent<Animator>().Play("BeHit");
      // 技能結果
      other.transform.parent.gameObject.GetComponent<Creature>().skillResult(Damage);
    }
  }
}