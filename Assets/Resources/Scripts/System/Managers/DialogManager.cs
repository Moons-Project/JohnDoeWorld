using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

  public static DialogManager instance;

  public GameObject dialogPanel;
  public Image characterAvatar;
  public Text dialogText;

  public Image leftImage, rightImage;

  private TypewriterEffect dialogTypewriter;

  private Dictionary<string, Sprite> TachieDict, AvatarDict;

  public enum WhichImage {
    LeftImage,
    RightImage
  }

  void Awake() {
    if (instance == null) {
      instance = this;
    }

    TachieDict = new Dictionary<string, Sprite>();
    AvatarDict = new Dictionary<string, Sprite>();

    ImportResources();
  }

  // Use this for initialization
  void Start () {
    dialogTypewriter = dialogText.gameObject.GetComponent<TypewriterEffect>();

    HideDialog();
  }

  void ImportResources() {
    Object[] avatars = Resources.LoadAll("Sprites/Avatar", typeof(Sprite));
    Object[] tachies = Resources.LoadAll("Sprites/Tachie", typeof(Sprite));

    foreach (var img in avatars) {
      AvatarDict.Add(img.name, img as Sprite);
    }

    foreach (var img in tachies) {
      TachieDict.Add(img.name, img as Sprite);
    }
  }

  public void ShowDialog(string speaker, string content, WhichImage which = WhichImage.LeftImage) {
    characterAvatar.sprite = AvatarDict.ContainsKey(speaker) ? AvatarDict[speaker] : null;

    ShowTachie(speaker, which);

    string text = "<b>" + speaker + "</b>: ";
    dialogText.text = text;
    // dialogText.text = text;
    dialogPanel.SetActive(true);
    dialogTypewriter.SetText(content, true);
  }

  public void ShowTachie(string speaker, WhichImage which) {
    Sprite tachie = null;
    if (TachieDict.ContainsKey(speaker)) {
      tachie = TachieDict[speaker];
    }
    var useImage = (which == WhichImage.LeftImage ? leftImage : rightImage);
    var otherImage = (useImage == leftImage ? rightImage : leftImage);
    
    if (otherImage.sprite != null)
      otherImage.color = new Color(1f, 1f, 1f, 0.5f);
    useImage.sprite = tachie;

    if (tachie == null) {
      useImage.color = new Color(1f, 1f, 1f, 0f);
    } else {
      useImage.color = new Color(1f, 1f, 1f, 1f);
    }
  }

  public void ShowDialog(Sprite avatar, string content) {
    characterAvatar.sprite = avatar;
    // dialogText.text = content;
    dialogPanel.SetActive(true);
    dialogTypewriter.SetText(content);
  }

  public void HideDialog() {
    dialogPanel.SetActive(false);
  }

  public void SkipDialog() {
    dialogTypewriter.Skip();
  }

  public delegate void DialogEndHandler();
  public event DialogEndHandler DialogEnd;

  public void OnDialogPress() {
    if (dialogTypewriter.typing) {
      dialogTypewriter.Skip();
    } else {
      if (DialogEnd != null) {
        DialogEnd();
      }
    }
  }

  public void SystemDialog(string content) {
    ShowDialog(null, "<b>" + content + "</b>");
    DialogEnd += SystemDialogHide;
  }

  private void SystemDialogHide() {
    this.DialogEnd -= SystemDialogHide;
    this.HideDialog();
  }

  public void SetTypeWriterDelay(float delay) {
    dialogTypewriter.delay = delay;
  }
}
