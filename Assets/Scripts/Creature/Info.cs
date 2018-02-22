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

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("weapon")) {
      
    }
  }
}
