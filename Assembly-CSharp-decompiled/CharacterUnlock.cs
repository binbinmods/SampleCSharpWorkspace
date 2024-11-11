// Decompiled with JetBrains decompiler
// Type: CharacterUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class CharacterUnlock : MonoBehaviour
{
  public TMP_Text nameTMP;
  public SpriteRenderer bgSPR;
  public SpriteRenderer characterSPR;
  public SpriteRenderer whirlSPR;

  private void Start()
  {
  }

  public void ShowUnlock(SubClassData _scd, SkinData _skd = null)
  {
    Color color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) _scd.HeroClass)]);
    this.nameTMP.text = _scd.CharacterName;
    if ((UnityEngine.Object) _skd != (UnityEngine.Object) null)
      this.nameTMP.text = Texts.Instance.GetText("characterSkin");
    if (_scd.CharacterName.ToLower() == "thuls")
      this.characterSPR.transform.localPosition = new Vector3(-0.5f, 0.0f, 0.0f);
    else if (_scd.CharacterName.ToLower() == "malukah")
      this.characterSPR.transform.localPosition = new Vector3(-0.75f, 0.0f, 0.0f);
    else if (_scd.CharacterName.ToLower() == "ottis")
    {
      if ((UnityEngine.Object) _skd != (UnityEngine.Object) null && _skd.SkinId == "ottiswolfwars")
        this.characterSPR.transform.localPosition = new Vector3(-0.37f, -0.13f, 0.0f);
      else
        this.characterSPR.transform.localPosition = new Vector3(0.0f, 0.2f, 0.0f);
    }
    else if (_scd.CharacterName.ToLower() == "heiner")
      this.characterSPR.transform.localPosition = new Vector3(-0.2f, -0.1f, 0.0f);
    else if (_scd.CharacterName.ToLower() == "grukli")
      this.characterSPR.transform.localPosition = new Vector3(-0.2f, 0.0f, 0.0f);
    else if (_scd.CharacterName.ToLower() == "wilbur")
      this.characterSPR.transform.localPosition = new Vector3(-0.4f, 0.0f, 0.0f);
    else if (_scd.CharacterName.ToLower() == "nezglekt")
      this.characterSPR.transform.localPosition = new Vector3(0.2f, -0.1f, 0.0f);
    else if (_scd.CharacterName.ToLower() == "yogger")
      this.characterSPR.transform.localPosition = new Vector3(-0.2f, 0.0f, 0.0f);
    else
      this.characterSPR.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.bgSPR.color = color;
    this.whirlSPR.color = new Color(color.r, color.g, color.b, 0.7f);
    SkinData skinData = !((UnityEngine.Object) _skd == (UnityEngine.Object) null) ? _skd : Globals.Instance.GetSkinData(Globals.Instance.GetSkinBaseIdBySubclass(_scd.Id));
    if (!((UnityEngine.Object) skinData != (UnityEngine.Object) null))
      return;
    this.characterSPR.sprite = skinData.SpriteSiluetaGrande;
  }
}
