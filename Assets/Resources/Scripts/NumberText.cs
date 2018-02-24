using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NumberText : MonoBehaviour {

  private Text text;

  // Use this for initialization
  void Start () {
    text = GetComponent<Text>();
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  public void SetNumber(float number) {
    text.text = number.ToString("F0");
  }
}
