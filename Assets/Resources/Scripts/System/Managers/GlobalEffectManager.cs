using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffectManager : MonoBehaviour {

  public static GlobalEffectManager instance;
  public GameObject effectGObj;
  public Camera effectCamera;

  private Animator effectAnimator;
  // private GameObject effectCanvasGObj;

  void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  // Use this for initialization
  void Start () {
    effectAnimator = effectGObj.GetComponent<Animator>();
    // effectCanvasGObj = effectGObj.transform.parent.gameObject;

    // Add an event to all the clips
    foreach (var clip in effectAnimator.runtimeAnimatorController.animationClips) {
      AnimationEvent evt = new AnimationEvent();
      evt.time = clip.length;
      evt.functionName = "EffectEnd";
      evt.messageOptions = SendMessageOptions.DontRequireReceiver;
      clip.AddEvent(evt);
    }
  }

  public void Flash() {
    effectCamera.depth = 10;
    effectAnimator.Play("flash");
  }

  public void SwitchScene() {
    effectCamera.depth = 10;
    effectAnimator.Play("switch_scene");
  }

  public void _SwitchScene(string sceneName) {
    effectCamera.depth = 10;
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