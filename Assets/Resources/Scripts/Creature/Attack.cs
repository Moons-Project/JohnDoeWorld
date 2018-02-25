using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour {

  protected float damage;
  public float Damage { get { return damage; } protected set {damage = value;} }

  protected int skillIndex;
  protected SkillItem skill;
  protected Creature creature;

  public bool destroyGObjOnTriggerEnter = false;
  public bool disableScriptOnTriggerEnter = false;

  public virtual void UseSkill(SkillItem skill, Creature creature) {
    // skillIndex = index;
    this.creature = creature;
    // this.skill = GameManager.instance.skillDict.itemDict[creature.skillList[skillIndex]];
    this.skill = skill;
    Debug.Log(skill.idName);

    Animator animator = GetComponent<Animator>();
    if (animator.runtimeAnimatorController != null)
      animator.Play(skill.idName);
  }

  public void startSKill() {
    // Damage = skill.damage[creature.skillLevel[skillIndex] - 1] * creature.currentInfo.sword;
  }

  public void endSkill() {
    // Damage = 0.0f;
  }

  void OnTriggerEnter2D(Collider2D other) {
    // 檢測tag為Body 且不是自身Creature 
    if (other.CompareTag("Body") && 
        creature != null && 
        creature != other.transform.parent.gameObject.GetComponent<Creature>()) {
      // 音效（動畫）可以和技能相關，故放置在此
      GameManager.instance.musicManager.PlaySE("main_menu_hover");
      other.transform.parent.gameObject.GetComponent<Animator>().Play("BeHit");
      // 技能結果
      other.transform.parent.gameObject.GetComponent<Creature>().Damage(Damage);

      // 碰撞后动作
      if (destroyGObjOnTriggerEnter) {
        Debug.Log("Destroy On Trigger Enter");
        DestroyMe();
        return;
      }
      if (disableScriptOnTriggerEnter) {
        // this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
      }
    }
  }

  public virtual void DestroyMe() {
    Destroy(gameObject);
  }
}