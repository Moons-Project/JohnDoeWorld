using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectBehavior : MonoBehaviour {

  // Use this for initialization
  void Start () {
    
  }

  IEnumerator DestroyMe() {
    yield return new WaitForSeconds(0.5f);
    Destroy(gameObject);
  }
}
