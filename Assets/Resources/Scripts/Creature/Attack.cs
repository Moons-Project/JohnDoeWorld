using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour {

  // protected float damage;
  public float damage;

  protected int skillLevel;
  protected Skill skill;
  protected Creature attackSource;

  public bool destroyGObjOnTriggerEnter = false;
  public bool disableScriptOnTriggerEnter = false;

  public virtual void UseSkill(Skill skill, int skillLevel, Creature attackSource) {
    // skillIndex = index;
    this.skillLevel = skillLevel;
    this.attackSource = attackSource;
    // this.skill = GameManager.instance.skillDict.itemDict[creature.skillList[skillIndex]];
    this.skill = skill;
    Debug.Log(skill.idName);

    Animator animator = GetComponent<Animator>();
    if (animator.runtimeAnimatorController != null)
      animator.Play(skill.idName);

    // 计算伤害
    float basicDamage = 0f;
    if (skill.damageType == Skill.DamageType.Sword) {
      basicDamage = attackSource.currentInfo.sword;
    } else if (skill.damageType == Skill.DamageType.Magic) {
      basicDamage = attackSource.currentInfo.magic;
    }
    damage = skill.damage[skillLevel - 1] * basicDamage;
  }

  public void startSKill() {
    // Damage = skill.damage[creature.skillLevel[skillIndex] - 1] * creature.currentInfo.sword;
  }

  public void endSkill() {
    // Damage = 0.0f;
  }

  void OnTriggerEnter2D(Collider2D other) {
    Creature otherCreature;
    // 檢測tag為Body 且不是自身Creature 
    if (other.CompareTag("Body") && 
        attackSource != null && 
        attackSource != (otherCreature = other.transform.parent.gameObject.GetComponent<Creature>())) {
      // 音效（動畫）可以和技能相關，故放置在此
      GameManager.instance.musicManager.PlaySE("main_menu_hover");
      // TODO: 受击动画不应由此触发吧
      other.transform.parent.gameObject.GetComponent<Animator>().Play("BeHit");
      // 技能結果
      otherCreature.Damage(damage);
      // BUFF
      if (1 <= skillLevel && skillLevel <= skill.buffList.Length) {
        otherCreature.AddBuff(BuffDict.instance.itemDict[skill.buffList[skillLevel - 1]], 5f);
      }

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