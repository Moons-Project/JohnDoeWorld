using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {
  public BasicInfo basicInfo;
  public int[] skillLevel = new int[1] {1};
  public int[] skillList = new int[1] {1};

  // Use this for initialization
  void Start () {
    
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  public BasicInfo getFinalInfo() {
    // add equiment info
    // add buff info
    return this.basicInfo;
  } 

  void OnTriggerEnter2D(Collider2D other) {
    // can't cause damage to self
    if (other.CompareTag("Weapon") && other.gameObject.transform.parent.gameObject != transform.parent.gameObject) {
      float defense = basicInfo.rigidity;
      float finalDamage = other.gameObject.GetComponent<Attack>().Damage - defense;

      basicInfo.life -= finalDamage;
      transform.parent.gameObject.GetComponent<Animator>().Play("BeHit");

      Debug.Log(finalDamage);
    }
  }
}
