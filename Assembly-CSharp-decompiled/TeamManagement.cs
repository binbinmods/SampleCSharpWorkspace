// Decompiled with JetBrains decompiler
// Type: TeamManagement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeamManagement : MonoBehaviour
{
  public Transform buttonLaunch;
  public Dropdown[] dropHero;
  public Dropdown[] dropNPC;
  private List<string> heroOptions = new List<string>();
  private List<string> npcOptions = new List<string>();
  private PhotonView photonView;

  public static TeamManagement Instance { get; private set; }

  [PunRPC]
  private void NET_ChangeComboValue(string dropdownName, int value) => GameObject.Find(dropdownName).GetComponent<Dropdown>().value = value;

  private void Awake()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.LoadByName(nameof (TeamManagement));
    }
    else
    {
      if ((Object) TeamManagement.Instance == (Object) null)
        TeamManagement.Instance = this;
      else if ((Object) TeamManagement.Instance != (Object) this)
        Object.Destroy((Object) this);
      GameManager.Instance.SetCamera();
      this.photonView = PhotonView.Get((Component) this);
      GameManager.Instance.SceneLoaded();
    }
  }

  private bool IsMaster() => !GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster();

  private void Start()
  {
    this.heroOptions.Add("-----------");
    this.npcOptions.Add("-----------");
    this.GenerateHeroes();
    this.GenerateNPCs();
    foreach (Dropdown dropdown in this.dropHero)
    {
      Dropdown drop = dropdown;
      if (!this.IsMaster())
        drop.interactable = false;
      drop.options.Clear();
      drop.AddOptions(this.heroOptions);
      drop.onValueChanged.AddListener((UnityAction<int>) (_param1 => this.myDropdownValueChangedHandler(drop)));
    }
    foreach (Dropdown dropdown in this.dropNPC)
    {
      Dropdown drop = dropdown;
      if (!this.IsMaster())
        drop.interactable = false;
      drop.options.Clear();
      drop.AddOptions(this.npcOptions);
      drop.onValueChanged.AddListener((UnityAction<int>) (_param1 => this.myDropdownValueChangedHandler(drop)));
    }
    if (!this.IsMaster())
      this.buttonLaunch.gameObject.SetActive(false);
    for (int index = 1; index < 5; ++index)
    {
      string str1 = SaveManager.LoadPrefsString("TeamHeroes" + index.ToString());
      int num1 = 0;
      if (str1 != "")
      {
        Dropdown component = GameObject.Find("Hero" + index.ToString()).GetComponent<Dropdown>();
        foreach (Dropdown.OptionData option in component.options)
        {
          if (option.text == str1)
          {
            component.value = num1;
            break;
          }
          ++num1;
        }
      }
      int num2 = 0;
      string str2 = SaveManager.LoadPrefsString("TeamNPCs" + index.ToString());
      if (str2 != "")
      {
        Dropdown component = GameObject.Find("NPC" + index.ToString()).GetComponent<Dropdown>();
        foreach (Dropdown.OptionData option in component.options)
        {
          if (option.text == str2)
          {
            component.value = num2;
            break;
          }
          ++num2;
        }
      }
    }
  }

  public void Draw()
  {
    int position1 = 0;
    foreach (Dropdown dropdown in this.dropHero)
    {
      if (dropdown.value > 0)
        AtOManager.Instance.SetTeamSingle(GameManager.Instance.GameHeroes[this.heroOptions[dropdown.value]], position1);
      else
        AtOManager.Instance.SetTeamSingle((Hero) null, position1);
      ++position1;
    }
    int position2 = 0;
    foreach (Dropdown dropdown in this.dropNPC)
    {
      if (dropdown.value > 0)
        AtOManager.Instance.SetTeamNPCSingle(this.npcOptions[dropdown.value], position2);
      else
        AtOManager.Instance.SetTeamNPCSingle("", position2);
      ++position2;
    }
    if (position1 <= 0 || position2 <= 0)
      return;
    for (int index = 1; index <= 4; ++index)
    {
      Dropdown component = GameObject.Find("Hero" + index.ToString()).GetComponent<Dropdown>();
      string text = component.options[component.value].text;
      SaveManager.SaveIntoPrefsString("TeamHeroes" + index.ToString(), text);
    }
    for (int index = 1; index <= PlayerManager.Instance.TeamNPCs.Length; ++index)
    {
      Dropdown component = GameObject.Find("NPC" + index.ToString()).GetComponent<Dropdown>();
      string text = component.options[component.value].text;
      SaveManager.SaveIntoPrefsString("TeamNPCs" + index.ToString(), text);
    }
    if (GameManager.Instance.IsMultiplayer())
      NetworkManager.Instance.LoadScene("Combat");
    else
      SceneStatic.LoadByName("Combat");
  }

  private void GenerateHeroes()
  {
    foreach (string key in GameManager.Instance.GameHeroes.Keys)
      this.heroOptions.Add(key);
    this.heroOptions.Sort();
  }

  private void GenerateNPCs()
  {
    foreach (string key in Globals.Instance.NPCs.Keys)
      this.npcOptions.Add(Globals.Instance.NPCs[key].Id);
    this.npcOptions.Sort();
  }

  private void Destroy()
  {
    foreach (Dropdown dropdown in this.dropHero)
      dropdown.onValueChanged.RemoveAllListeners();
  }

  private void myDropdownValueChangedHandler(Dropdown target)
  {
    string str = !target.name.Contains("Hero") ? "NPC" : "Hero";
    SpriteRenderer component1 = GameObject.Find("Item" + target.name).GetComponent<SpriteRenderer>();
    TMP_Text component2 = GameObject.Find("Item" + target.name + "/Name").GetComponent<TMP_Text>();
    if (target.value == 0)
    {
      component1.sprite = (Sprite) null;
      component2.text = "";
    }
    else
    {
      component1.sprite = !(str == "Hero") ? Globals.Instance.NPCs[target.options[target.value].text].Sprite : GameManager.Instance.GameHeroes[target.options[target.value].text].HeroSprite;
      component2.text = target.options[target.value].text;
    }
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_ChangeComboValue", RpcTarget.Others, (object) target.name, (object) target.value);
  }

  public void SetDropdownIndex(int index)
  {
  }
}
