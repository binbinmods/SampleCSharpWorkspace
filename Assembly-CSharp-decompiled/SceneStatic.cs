// Decompiled with JetBrains decompiler
// Type: SceneStatic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneStatic
{
  public static string CrossScene { get; set; }

  public static string GetSceneName() => SceneManager.GetActiveScene().name;

  public static void LoadByName(string scene, bool showMask = true)
  {
    if ((Object) AlertManager.Instance != (Object) null)
    {
      AlertManager.buttonClickDelegate = (AlertManager.OnButtonClickDelegate) null;
      AlertManager.Instance.HideAlert();
    }
    if ((Object) MadnessManager.Instance != (Object) null)
      MadnessManager.Instance.CloseMadness();
    if ((Object) CardScreenManager.Instance != (Object) null && CardScreenManager.Instance.IsActive())
      CardScreenManager.Instance.ShowCardScreen(false);
    Cursor.visible = true;
    switch (scene)
    {
      case "MainMenu":
        SceneStatic.LoadMainMenu();
        break;
      case "Lobby":
        SceneStatic.LoadLobby();
        break;
      case "Combat":
        SceneStatic.LoadCombat();
        break;
      case "IntroNewGame":
        SceneStatic.LoadIntroNewGame();
        break;
      case "Town":
        SceneStatic.LoadTown();
        break;
      case "Map":
        SceneStatic.LoadMap();
        break;
      case "HeroSelection":
        SceneStatic.LoadHeroSelection();
        break;
      case "TomeOfKnowledge":
        SceneStatic.LoadTome();
        break;
      case "TeamManagement":
        SceneStatic.LoadTeamManagement();
        break;
      case "Rewards":
        SceneStatic.LoadRewards(showMask);
        break;
      case "Loot":
        SceneStatic.LoadLoot(showMask);
        break;
      case "FinishRun":
        SceneStatic.LoadFinishRun();
        break;
      case "ChallengeSelection":
        SceneStatic.LoadChallengeSelection();
        break;
      case "TrailerEnd":
        if ((Object) GameManager.Instance == (Object) null)
        {
          SceneStatic.CrossScene = "TrailerEnd";
          SceneManager.LoadScene("Game");
          break;
        }
        GameManager.Instance.ChangeScene("TrailerEnd");
        break;
      case "TrailerPoster":
        if ((Object) GameManager.Instance == (Object) null)
        {
          SceneStatic.CrossScene = "TrailerPoster";
          SceneManager.LoadScene("Game");
          break;
        }
        GameManager.Instance.ChangeScene("TrailerPoster");
        break;
      case "CardPlayer":
        if ((Object) GameManager.Instance == (Object) null)
        {
          SceneStatic.CrossScene = "CardPlayer";
          SceneManager.LoadScene("Game");
          break;
        }
        GameManager.Instance.ChangeScene("CardPlayer");
        break;
      case "CardPlayerPairs":
        SceneStatic.LoadCardPlayerPairs();
        break;
      case "Cinematic":
        SceneStatic.LoadCinematic();
        break;
    }
  }

  public static void LoadMainMenu()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "MainMenu";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("MainMenu");
  }

  public static void LoadLobby()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Lobby";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Lobby");
  }

  public static void LoadCombat()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Combat";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Combat");
  }

  public static void LoadIntroNewGame()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "IntroNewGame";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("IntroNewGame");
  }

  public static void LoadTown()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Town";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Town");
  }

  public static void LoadCinematic()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Cinematic";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Cinematic");
  }

  public static void LoadMap()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Map";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Map");
  }

  public static void LoadHeroSelection()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "HeroSelection";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("HeroSelection");
  }

  public static void LoadTome()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "TomeOfKnowledge";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("TomeOfKnowledge");
  }

  public static void LoadTeamManagement()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "TeamManagement";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("TeamManagement");
  }

  public static void LoadRewards(bool showMask = true)
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Rewards";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Rewards", showMask);
  }

  public static void LoadLoot(bool showMask = true)
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "Loot";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("Loot", showMask);
  }

  public static void LoadFinishRun()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "FinishRun";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("FinishRun");
  }

  public static void LoadChallengeSelection()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "ChallengeSelection";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("ChallengeSelection");
  }

  public static void LoadCardPlayerPairs(bool showMask = true)
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.CrossScene = "CardPlayerPairs";
      SceneManager.LoadScene("Game");
    }
    else
      GameManager.Instance.ChangeScene("CardPlayerPairs", showMask);
  }
}
