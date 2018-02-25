using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : Attack {

  public void startSKill() {
    GetComponent<Collider2D>().enabled = true;
  }

  public void endSkill() {
    GetComponent<Collider2D>().enabled = false;
  }
}