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

  public void _SwitchScene(string sceneName) {
    AnimationEvent evt = new AnimationEvent();
    evt.stringParameter = sceneName;
    evt.time = 1.0f;
    evt.functionName = "__SwitchScene";
    
    effectAnimator.Play("switch_scene");
    Debug.Log("In _SwitchScene");
    // Find clip
    AnimationClip clip = null;
    foreach (var c in effectAnimator.runtimeAnimatorController.animationClips) {
      if (c.name == "switch_scene") {
        clip = c;
        break;
      }
    }
    Debug.Log(clip);
    clip.AddEvent(evt);
  }
}