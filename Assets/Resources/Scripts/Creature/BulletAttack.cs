using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : Attack {
  private Animator animator;
  private Rigidbody2D rb;

  void Start() {
    Debug.Log("Start in BulletAttack");
    if (animator == null)
      animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
  }
  
  public override void UseSkill(SkillItem skill, Creature creature) {
    // skillIndex = index;
    this.creature = creature;
    // this.skill = GameManager.instance.skillDict.itemDict[creature.skillList[skillIndex]];
    this.skill = skill;
    Debug.Log(skill.idName);

    // Set Animation
    Debug.Log(animator);
    if (animator == null) animator = GetComponent<Animator>();
    animator.runtimeAnimatorController = BulletDict.instance.animatorControllerDict[skill.idName];
    
    if (animator.runtimeAnimatorController != null) {
      animator.Play("Flying");
    }
  }

  public override void DestroyMe() {
    animator.SetBool("destroy", true);
    rb.Sleep();
  }

  public void ImmediateDestroyMe() {
    Destroy(gameObject);
  }
}