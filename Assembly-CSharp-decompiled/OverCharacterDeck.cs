// Decompiled with JetBrains decompiler
// Type: OverCharacterDeck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverCharacterDeck : MonoBehaviour
{
  private OverCharacter overCharacter;
  private int heroIndex;
  private Transform deckImage;
  private Transform deckText;
  private TMP_Text textGO;
  private Vector3 oriImgScale;
  private Scene scene;
  private string botName;

  private void Awake()
  {
    this.overCharacter = this.transform.parent.transform.GetComponent<OverCharacter>();
    this.deckImage = this.transform.GetChild(0);
    this.deckText = this.transform.GetChild(1);
    this.textGO = this.deckText.GetComponent<TMP_Text>();
    this.oriImgScale = this.deckImage.localScale;
    this.scene = SceneManager.GetActiveScene();
    this.botName = this.gameObject.name;
  }

  public void SetIndex(int index) => this.heroIndex = index;
}
