// Decompiled with JetBrains decompiler
// Type: SideCharacters
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SideCharacters : MonoBehaviour
{
  public OverCharacter[] charArray = new OverCharacter[4];
  private Transform[] charTransforms = new Transform[4];
  private int heroActive = -1;

  private void Awake()
  {
    for (int index = 0; index < 4; ++index)
      this.charTransforms[index] = this.charArray[index].transform;
  }

  private void Start()
  {
    if ((Object) AtOManager.Instance == (Object) null || AtOManager.Instance.GetTeam().Length == 0 || (bool) (Object) MatchManager.Instance)
      return;
    this.Show();
  }

  public void Show()
  {
    this.Resize();
    Hero[] team = AtOManager.Instance.GetTeam();
    for (int index = 0; index < 4; ++index)
    {
      if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1")
      {
        switch (index)
        {
          case 1:
          case 2:
            continue;
          case 3:
            this.charArray[index].transform.localPosition = new Vector3(0.0f, -1.24f * Globals.Instance.multiplierY, 0.0f);
            break;
        }
      }
      if (team != null && team[index] != null && (Object) team[index].HeroData != (Object) null)
      {
        this.charArray[index].gameObject.SetActive(true);
        this.charArray[index].Init(index);
        this.charArray[index].Enable();
      }
    }
    if (!(bool) (Object) TownManager.Instance && !(bool) (Object) ChallengeSelectionManager.Instance && !(bool) (Object) EventManager.Instance)
      return;
    this.InCharacterScreen(true);
  }

  public void InCharacterScreen(bool state)
  {
    for (int index = 0; index < 4; ++index)
      this.charArray[index].InCharacterScreen(state);
  }

  public void Hide()
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    for (int index = 0; index < 4; ++index)
      this.charArray[index].gameObject.SetActive(false);
  }

  public void Resize()
  {
    float num = (float) (1920.0 * (double) Screen.height / (1080.0 * (double) Screen.width));
    this.transform.position = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.5 + 0.38999998569488525 * (double) Globals.Instance.multiplierX * (double) num), (float) ((double) Globals.Instance.sizeH * 0.5 - 1.8999999761581421 * (double) Globals.Instance.multiplierY * (double) num), this.transform.position.z);
    for (int index = 0; index < 4; ++index)
      this.charTransforms[index].localPosition = new Vector3(0.0f, (float) index * -1.24f * Globals.Instance.multiplierY, 0.0f);
  }

  public Vector3 CharacterIconPosition(int index) => this.charArray[index].CharacterIconPosition();

  public void Refresh()
  {
    if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2")
    {
      this.Show();
    }
    else
    {
      for (int index = 0; index < 4; ++index)
        this.charArray[index].Init(index);
    }
    if (this.heroActive <= -1)
      return;
    this.charArray[this.heroActive].SetActive(true);
  }

  public void RefreshCards(int hero = -1)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (hero == -1 || hero == index)
        this.charArray[index].DoCards();
    }
  }

  public void ShowChallengeButtons(int hero = -1, bool state = true)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (hero == -1 || hero == index)
        this.charArray[index].ShowChallengeButtonReady(state);
    }
  }

  public void ShowUpgrade(int hero = -1)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (hero == -1 || hero == index)
        this.charArray[index].ShowUpgrade();
    }
  }

  public void ResetCharacters()
  {
    for (int index = 0; index < 4; ++index)
    {
      this.charArray[index].Enable();
      this.charArray[index].SetActive(false);
      this.charArray[index].SetClickable(true);
      this.charArray[index].SetClickable(true);
    }
    this.ShowLevelUpCharacters();
    this.heroActive = -1;
  }

  public void EnableOwnedCharacters(bool clickable = true)
  {
    Hero[] team = AtOManager.Instance.GetTeam();
    if (team == null || team.Length == 0)
      return;
    string playerNick = NetworkManager.Instance.GetPlayerNick();
    for (int index = 0; index < 4; ++index)
    {
      if (team[index] != null && !((Object) team[index].HeroData == (Object) null))
      {
        if (team[index].Owner == null || team[index].Owner == "" || team[index].Owner == playerNick)
        {
          this.charArray[index].Enable();
          this.charArray[index].SetClickable(clickable);
          if (clickable)
            this.charArray[index].EnableCards(false);
          else
            this.charArray[index].EnableCards(true);
        }
        else
        {
          this.charArray[index].Disable();
          this.charArray[index].EnableCards(false);
        }
        this.charArray[index].Enable();
        this.charArray[index].SetClickable(true);
      }
    }
  }

  public void ShowLevelUpCharacters()
  {
    for (int index = 0; index < 4; ++index)
      this.charArray[index].ShowLevelUp();
  }

  public void ShowActiveStatus(int characterIndex)
  {
    if (characterIndex <= -1)
      return;
    this.charArray[characterIndex].ShowActiveStatus(true);
  }

  public void SetActive(int characterIndex)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (index != characterIndex)
        this.charArray[index].SetActive(false);
      else
        this.charArray[index].SetActive(true);
    }
    this.heroActive = characterIndex;
  }

  public int GetFirstEnabledCharacter()
  {
    string playerNick = NetworkManager.Instance.GetPlayerNick();
    Hero[] team = AtOManager.Instance.GetTeam();
    if (team == null || team.Length == 0)
      return 0;
    for (int enabledCharacter = 0; enabledCharacter < 4; ++enabledCharacter)
    {
      if (team[enabledCharacter] != null && !((Object) team[enabledCharacter].HeroData == (Object) null) && (team[enabledCharacter].Owner == null || team[enabledCharacter].Owner == "" || team[enabledCharacter].Owner == playerNick))
        return enabledCharacter;
    }
    return 0;
  }

  public void EnableAll()
  {
    for (int index = 0; index < 4; ++index)
      this.charArray[index].Enable();
  }

  public void DisableAll(int _enableChar = -1)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (_enableChar != index)
        this.charArray[index].Disable();
    }
  }
}
