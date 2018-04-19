using UnityEngine;

public abstract class Interactable : MonoBehaviour {
  abstract public void Interact(Creature source);
}