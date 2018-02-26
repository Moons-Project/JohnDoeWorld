using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour {

  // protected float damage;
  public float damage;

  protected Creature.CreatureSkill cSkill;
  protected Creature attackSource;

  public bool destroyGObjOnTriggerEnter = false;
  public bool disableScriptOnTriggerEnter = false;

  public virtual void UseSkill(Creature.CreatureSkill skill, Creature attackSource) {
    // skillIndex = index;
    this.attackSource = attackSource;
    // this.skill = GameManager.instance.skillDict.itemDict[creature.skillList[skillIndex]];
    this.cSkill = skill;

    // 计算伤害
    float basicDamage = 0f;
    if (skill.skill.damageType == Skill.DamageType.Sword) {
      basicDamage = attackSource.currentInfo.sword;
    } else if (skill.skill.damageType == Skill.DamageType.Magic) {
      basicDamage = attackSource.currentInfo.magic;
    }
    damage = skill.skill.damage[cSkill.level - 1] * basicDamage;
  }

  public virtual void startSkill() {
    Debug.Log("Attack startSkill");
  }

  public virtual void endSkill() {
    Debug.Log("Attack endSkill");
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
      if (1 <= cSkill.level && cSkill.level <= cSkill.skill.buffList.Length) {
        otherCreature.AddBuff(BuffDict.instance.itemDict[cSkill.skill.buffList[cSkill.level - 1]], 5f);
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