// Decompiled with JetBrains decompiler
// Type: MenuSaveButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveButton : MonoBehaviour
{
  public TMP_Text slotText;
  public TMP_Text usethisText;
  public TMP_Text typeText;
  public TMP_Text descriptionText;
  public TMP_Text playersText;
  public TMP_Text madnessText;
  public Image[] imgHero;
  public TMP_Text[] playerText;
  public Transform incompatibleT;
  public TMP_Text versionText;
  public Transform ngPlus;
  public Transform deleteButton;
  public Transform portraitIcons;
  private int slotNum = -1;
  private bool active;
  private GameData gameData;
  private Coroutine coExit;
  private Image imageBg;
  private Color colorHover;

  private void Awake()
  {
    this.ShowDeleteButton(false);
    this.imageBg = this.GetComponent<Image>();
    this.colorHover = new Color(1f, 1f, 1f, 0.5f);
  }

  public void SetActive(bool _state)
  {
    this.active = _state;
    this.ShowNGPlus(false);
    this.ShowPortraitIcons(_state);
    this.slotText.gameObject.SetActive(!_state);
    this.usethisText.gameObject.SetActive(!_state);
    this.descriptionText.gameObject.SetActive(_state);
    this.deleteButton.gameObject.SetActive(false);
    this.playersText.gameObject.SetActive(false);
    this.incompatibleT.gameObject.SetActive(false);
    this.versionText.text = "";
    RectTransform component = this.GetComponent<RectTransform>();
    if (!_state)
    {
      this.slotText.text = Texts.Instance.GetText("mainMenuCreateNewGame");
      component.sizeDelta = new Vector2(520f, 220f);
    }
    else
    {
      this.slotText.text = Texts.Instance.GetText("menuLoadGame");
      component.sizeDelta = new Vector2(520f, 310f);
    }
  }

  public string CheckIfSavegameIsCompatible(string _version = "")
  {
    int number1 = Functions.GameVersionToNumber(_version);
    foreach (KeyValuePair<string, string> keyValuePair in Globals.Instance.IncompatibleVersion)
    {
      int number2 = Functions.GameVersionToNumber(keyValuePair.Key);
      if (number1 < number2)
        return keyValuePair.Value;
    }
    int number3 = Functions.GameVersionToNumber(this.gameData.Version);
    return number1 > number3 && number1 - number3 > 100 ? "0" : "";
  }

  public void SetGameData(GameData _gameData)
  {
    this.gameData = _gameData;
    if (this.gameData.Version == null)
      this.gameData.Version = "0.6.82";
    if (this.CheckIfSavegameIsCompatible(this.gameData.Version) != "")
    {
      this.GetComponent<Button>().interactable = false;
      this.incompatibleT.gameObject.SetActive(true);
    }
    else
    {
      this.GetComponent<Button>().interactable = true;
      this.incompatibleT.gameObject.SetActive(false);
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("v.");
    stringBuilder.Append(this.gameData.Version);
    this.versionText.text = stringBuilder.ToString();
    stringBuilder.Clear();
    NodeData nodeData = Globals.Instance.GetNodeData(_gameData.CurrentMapNode);
    if (_gameData.GameType == Enums.GameType.Adventure)
    {
      if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null && (UnityEngine.Object) nodeData.NodeZone != (UnityEngine.Object) null)
      {
        stringBuilder.Append(nodeData.NodeName);
        stringBuilder.Append(" <voffset=3><color=#666>|</color></voffset> <color=#AAA>");
        if (Globals.Instance.ZoneDataSource.ContainsKey(nodeData.NodeZone.ZoneId.ToLower()))
          stringBuilder.Append(Texts.Instance.GetText(Globals.Instance.ZoneDataSource[nodeData.NodeZone.ZoneId.ToLower()].ZoneName));
        else
          stringBuilder.Append(Texts.Instance.GetText(nodeData.NodeZone.ZoneId.ToLower()));
        int num = _gameData.TownTier + 1;
        if (num > 4)
          num = 4;
        string str = string.Format(Texts.Instance.GetText("actNumber"), (object) num);
        stringBuilder.Append(" <size=-2>(");
        stringBuilder.Append(str);
        stringBuilder.Append(")</size>");
        stringBuilder.Append("</color>");
      }
    }
    else if (_gameData.GameType == Enums.GameType.WeeklyChallenge)
    {
      stringBuilder.Append(AtOManager.Instance.GetWeeklyName(_gameData.Weekly));
      stringBuilder.Append(" <voffset=3><color=#666>|</color></voffset> <color=#AAA>");
      if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null && (UnityEngine.Object) nodeData.NodeZone != (UnityEngine.Object) null)
      {
        if (nodeData.NodeZone.ObeliskLow)
          stringBuilder.Append(Texts.Instance.GetText("lowerObelisk"));
        else if (nodeData.NodeZone.ObeliskHigh)
          stringBuilder.Append(Texts.Instance.GetText("upperObelisk"));
        else
          stringBuilder.Append(Texts.Instance.GetText("finalObelisk"));
      }
      stringBuilder.Append("</color>");
    }
    else if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null && (UnityEngine.Object) nodeData.NodeZone != (UnityEngine.Object) null)
    {
      if (nodeData.NodeZone.ObeliskLow)
        stringBuilder.Append(Texts.Instance.GetText("lowerObelisk"));
      else if (nodeData.NodeZone.ObeliskHigh)
        stringBuilder.Append(Texts.Instance.GetText("upperObelisk"));
      else
        stringBuilder.Append(Texts.Instance.GetText("finalObelisk"));
    }
    string[] strArray = _gameData.GameDate.Split(' ', StringSplitOptions.None);
    stringBuilder.Append("   <nobr><size=-2><color=#ffffff>");
    stringBuilder.Append(strArray[0]);
    stringBuilder.Append("</color> <color=#aaaaaa>");
    stringBuilder.Append(strArray[1]);
    stringBuilder.Append("</color></nobr>");
    this.descriptionText.text = stringBuilder.ToString();
    int ngPlus = _gameData.NgPlus;
    string madnessCorruptors = _gameData.MadnessCorruptors;
    int num1 = MadnessManager.Instance.CalculateMadnessTotal(ngPlus, madnessCorruptors);
    if (num1 == 0)
      num1 = _gameData.ObeliskMadness;
    if (num1 > 0)
    {
      this.ShowNGPlus(true);
      if (madnessCorruptors == null)
        ;
      this.madnessText.text = "M" + num1.ToString();
    }
    if (this.gameData.GameMode == Enums.GameMode.Multiplayer)
    {
      stringBuilder.Clear();
      if (_gameData.Owner0 != null && _gameData.Owner0 != "")
      {
        int position1 = 0;
        string owner0 = _gameData.Owner0;
        foreach (KeyValuePair<string, string> keyValuePair in _gameData.PlayerNickRealDict)
        {
          if (!(keyValuePair.Value == owner0))
            ++position1;
          else
            break;
        }
        stringBuilder.Append("<color=");
        stringBuilder.Append(NetworkManager.Instance.ColorFromPosition(position1));
        stringBuilder.Append(">");
        stringBuilder.Append(_gameData.Owner0);
        stringBuilder.Append("</color>");
        this.playerText[0].text = stringBuilder.ToString();
        string owner1 = _gameData.Owner1;
        int position2 = 0;
        foreach (KeyValuePair<string, string> keyValuePair in _gameData.PlayerNickRealDict)
        {
          if (!(keyValuePair.Value == owner1))
            ++position2;
          else
            break;
        }
        stringBuilder.Clear();
        stringBuilder.Append("<color=");
        stringBuilder.Append(NetworkManager.Instance.ColorFromPosition(position2));
        stringBuilder.Append(">");
        stringBuilder.Append(_gameData.Owner1);
        stringBuilder.Append("</color>");
        this.playerText[1].text = stringBuilder.ToString();
        string owner2 = _gameData.Owner2;
        int position3 = 0;
        foreach (KeyValuePair<string, string> keyValuePair in _gameData.PlayerNickRealDict)
        {
          if (!(keyValuePair.Value == owner2))
            ++position3;
          else
            break;
        }
        stringBuilder.Clear();
        stringBuilder.Append("<color=");
        stringBuilder.Append(NetworkManager.Instance.ColorFromPosition(position3));
        stringBuilder.Append(">");
        stringBuilder.Append(_gameData.Owner2);
        stringBuilder.Append("</color>");
        this.playerText[2].text = stringBuilder.ToString();
        string owner3 = _gameData.Owner3;
        int position4 = 0;
        foreach (KeyValuePair<string, string> keyValuePair in _gameData.PlayerNickRealDict)
        {
          if (!(keyValuePair.Value == owner3))
            ++position4;
          else
            break;
        }
        stringBuilder.Clear();
        stringBuilder.Append("<color=");
        stringBuilder.Append(NetworkManager.Instance.ColorFromPosition(position4));
        stringBuilder.Append(">");
        stringBuilder.Append(_gameData.Owner3);
        stringBuilder.Append("</color>");
        this.playerText[3].text = stringBuilder.ToString();
        for (int index = 0; index < 4; ++index)
          this.playerText[index].gameObject.SetActive(true);
      }
      else
      {
        List<string> stringList = new List<string>();
        if (_gameData.PlayerNickRealDict != null)
        {
          int position = 0;
          for (int index = 0; index < 4; ++index)
            this.playerText[index].gameObject.SetActive(false);
          foreach (KeyValuePair<string, string> keyValuePair in _gameData.PlayerNickRealDict)
          {
            if (!stringList.Contains(keyValuePair.Value))
            {
              stringBuilder.Append("<color=");
              stringBuilder.Append(NetworkManager.Instance.ColorFromPosition(position));
              stringBuilder.Append(">");
              stringBuilder.Append(keyValuePair.Value);
              stringBuilder.Append("</color>, ");
              ++position;
              stringList.Add(keyValuePair.Value);
            }
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < 4; ++index)
        this.playerText[index].gameObject.SetActive(false);
    }
    Hero[] heroArray = JsonHelper.FromJson<Hero>(_gameData.TeamAtO);
    for (int index = 0; index < heroArray.Length; ++index)
    {
      if (heroArray[index].SubclassName != null && (UnityEngine.Object) Globals.Instance.GetSubClassData(heroArray[index].SubclassName) != (UnityEngine.Object) null)
      {
        SkinData skinData = heroArray[index].SkinUsed == null || heroArray[index].SkinUsed == "" ? Globals.Instance.GetSkinData(Globals.Instance.GetSkinBaseIdBySubclass(heroArray[index].SubclassName)) : Globals.Instance.GetSkinData(heroArray[index].SkinUsed);
        this.imgHero[index].sprite = !((UnityEngine.Object) skinData != (UnityEngine.Object) null) ? Globals.Instance.GetSubClassData(heroArray[index].SubclassName).SpriteSpeed : skinData.SpritePortrait;
        this.imgHero[index].gameObject.SetActive(true);
      }
      else
        this.imgHero[index].gameObject.SetActive(false);
    }
  }

  public void SetSlot(int _num)
  {
    this.slotNum = _num;
    this.GetComponent<Button>().interactable = true;
    this.incompatibleT.gameObject.SetActive(false);
  }

  private void ShowDeleteButton(bool _state) => this.deleteButton.gameObject.SetActive(_state);

  private void ShowNGPlus(bool _state) => this.ngPlus.gameObject.SetActive(_state);

  private void ShowPortraitIcons(bool _state) => this.portraitIcons.gameObject.SetActive(_state);

  public void SelectThis()
  {
    if (AlertManager.Instance.IsActive())
      return;
    AtOManager.Instance.SetSaveSlot(this.slotNum);
    if (!this.active)
    {
      if (this.slotNum < 6)
        SceneStatic.LoadByName("HeroSelection");
      else if (this.slotNum < 12)
        SceneStatic.LoadByName("Lobby");
      else if (this.slotNum < 18)
        SceneStatic.LoadByName("HeroSelection");
      else if (this.slotNum < 24)
        SceneStatic.LoadByName("Lobby");
      else if (this.slotNum < 30)
      {
        SceneStatic.LoadByName("HeroSelection");
      }
      else
      {
        if (this.slotNum >= 36)
          return;
        SceneStatic.LoadByName("Lobby");
      }
    }
    else
      AtOManager.Instance.LoadGame(this.slotNum);
  }

  public void DeleteThis()
  {
    if (AlertManager.Instance.IsActive())
      return;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.DeleteAction);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("wantToRemoveSave"));
    stringBuilder.Append(" <br><size=-4><color=#AAAAAA>");
    stringBuilder.Append(Texts.Instance.GetText("wantToRemoveSavePermanent"));
    stringBuilder.Append("</color></size>");
    AlertManager.Instance.AlertConfirmDouble(stringBuilder.ToString(), Texts.Instance.GetText("accept").ToUpper(), Texts.Instance.GetText("cancel").ToUpper());
  }

  private void DeleteAction()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.DeleteAction);
    if (!AlertManager.Instance.GetConfirmAnswer())
      return;
    SaveManager.DeleteGame(this.slotNum);
    this.ShowNGPlus(false);
    this.SetActive(false);
    this.GetComponent<Button>().interactable = true;
  }

  public void HoverOn()
  {
    if (AlertManager.Instance.IsActive())
      return;
    if (this.coExit != null)
      this.StopCoroutine(this.coExit);
    this.imageBg.color = this.colorHover;
    GameManager.Instance.PlayLibraryAudio("ui_menu");
    if (!this.active)
      return;
    this.ShowDeleteButton(true);
  }

  public void HoverOff()
  {
    if (AlertManager.Instance.IsActive())
      return;
    if (this.coExit != null)
      this.StopCoroutine(this.coExit);
    this.coExit = this.StartCoroutine(this.ExitButton());
  }

  private IEnumerator ExitButton()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.ShowDeleteButton(false);
    this.imageBg.color = new Color(1f, 1f, 1f, 0.0f);
  }
}
