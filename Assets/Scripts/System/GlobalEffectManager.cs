using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffectManager : MonoBehaviour {

  public static GlobalEffectManager instance;
  public GameObject effectGObj;

  private Animator effectAnimator;
  private GameObject effectCanvasGObj;

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
    effectCanvasGObj = effectGObj.transform.parent.gameObject;
  }

  public void Flash() {
    effectAnimator.Play("flash");
  }

  public void SwitchScene() {
    effectAnimator.Play("switch_scene");
  }

  public void _SwitchScene(string sceneName) {
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

    AnimationEvent evt = new AnimationEvent();
    evt.stringParameter = sceneName;
    evt.objectReferenceParameter = clip;
    evt.time = 0.83f;
    evt.functionName = "__SwitchScene";
    
    Debug.Log(clip);
    clip.AddEvent(evt);
  }
}