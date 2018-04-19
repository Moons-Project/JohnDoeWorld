using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour {

  public float poisonDamage = 30f;

  void OnTriggerStay2D(Collider2D other) {
    Creature creature = other.GetComponent<Creature>();
    if (creature)
      creature.Damage(poisonDamage);
  }
}
