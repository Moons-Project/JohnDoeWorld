using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TypewriterEffect : MonoBehaviour {

  private Text textComponent;
  private string currentText = "";
  private int currentIndex = 0;
  private bool skip = false;

  [HideInInspector]
  public bool typing = false;

  public float delay = 0.5f;

  public delegate void TypeEndHandler();
  public event TypeEndHandler TypeEnd;

  void Awake() {
    textComponent = gameObject.GetComponent<Text>();
  }

  // Use this for initialization
  void Start () {

  }
  
  // Update is called once per frame
  void Update () {
    
  }

  public void SetText(string text, bool append = false) {
    StopAllCoroutines();
    
    if (!append)
      textComponent.text = "";
    currentText = text;
    currentIndex = 0;
    typing = true;
    skip = false;
    StartCoroutine(Type());
  }

  IEnumerator Type() {
    if (skip) {
      skip = false;
      typing = false;
      textComponent.text += currentText.Substring(currentIndex);
      if (TypeEnd != null)
        TypeEnd();
      yield return null;
    } else {
      textComponent.text += currentText[currentIndex++];
      yield return new WaitForSeconds(delay);
      if (currentIndex >= currentText.Length) {
        // Stop Here
        typing = false;
        if (TypeEnd != null)
          TypeEnd();
      } else {
        StartCoroutine(Type());
      }
    }
  }

  public void Skip() {
    skip = true;
  }
}
