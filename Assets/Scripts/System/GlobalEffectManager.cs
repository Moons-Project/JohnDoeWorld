using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffectManager : MonoBehaviour {

  public static GlobalEffectManager instance;
  public GameObject effectGObj;

  private Animator effectAnimator;

  private GameManager manager;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start () {
    manager = GameManager.instance;
    effectAnimator = effectGObj.GetComponent<Animator>();
  }

  public void Flash() {
    effectAnimator.Play("flash");
  }

  public void SwitchScene() {
    effectAnimator.Play("switch_scene");
  }
}