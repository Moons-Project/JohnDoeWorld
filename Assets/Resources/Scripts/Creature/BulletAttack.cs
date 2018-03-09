using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : Attack {
  private Rigidbody2D rb;

  void Start() {
    Debug.Log("Start in BulletAttack");
    rb = GetComponent<Rigidbody2D>();
  }
  
  public override void UseSkill(CreatureSkill skill, Creature attackSource) {
    base.UseSkill(skill, attackSource);
    Animator animator = GetComponent<Animator>();
    animator.runtimeAnimatorController = BulletDict.instance.animatorControllerDict[skill.skill.idName];
    if (animator.runtimeAnimatorController != null) {
      animator.Play("Flying");
    }
  }

  public override void DestroyMe() {
    Animator animator = GetComponent<Animator>();
    animator.SetBool("destroy", true);
    rb.Sleep();
  }

  public void ImmediateDestroyMe() {
    Destroy(gameObject);
  }
}