using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Control : MonoBehaviour {

  public GameObject Singleton_;

  void SpawnSingleton(int progress) {
    if (progress == 13 || progress == 10)
      Singleton_.SetActive(true);
  }

  void Awake() {
    ScriptDisplayer.OnScriptDisplay += SpawnSingleton;
  }
  
  void OnDisable() {
    ScriptDisplayer.OnScriptDisplay -= SpawnSingleton;
  }

  public void OnJaneDead(Creature creature) {
    SaveDataManager.instance.saveData.progress++;
    SaveDataManager.instance.Save();
    creature.OnDead -= OnJaneDead;
    ScriptManager.Finished after13 = null;
    after13 = ()=>{
      ScriptManager.instance.FinishedEvent -= after13;
      GlobalEffectManager.instance.Flash();
      ScriptManager.instance.PlayScript("14");
      ScriptManager.Finished after14 = null;
      after14 = ()=>{
        ScriptManager.instance.FinishedEvent -= after14;
        HUDManager.instance.CloseHUD();
        SaveDataManager.instance.Clear();
        GameManager.instance.SwitchScene("main_menu_scene");
      };
      ScriptManager.instance.FinishedEvent += after14;
    };
    ScriptManager.instance.FinishedEvent += after13;
    ScriptManager.instance.PlayScript("13");
  }
}
