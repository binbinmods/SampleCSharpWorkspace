// Decompiled with JetBrains decompiler
// Type: SaveManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveManager
{
  private static string savePlayerName = "player";
  private static string saveGameName = "gamedata_";
  private static string saveGameTurnExtension = "_turn";
  private static string saveRunsName = "runs";
  private static string saveDecksName = "decks";
  private static string savePerksName = "perks";
  private static string saveGameExtension = ".ato";
  private static string saveGameExtensionBK = ".bak";
  private static string backupName = "_backup";
  private static byte[] key = new byte[8]
  {
    (byte) 18,
    (byte) 54,
    (byte) 100,
    (byte) 160,
    (byte) 190,
    (byte) 148,
    (byte) 136,
    (byte) 3
  };
  private static byte[] iv = new byte[8]
  {
    (byte) 82,
    (byte) 242,
    (byte) 164,
    (byte) 132,
    (byte) 119,
    (byte) 197,
    (byte) 179,
    (byte) 20
  };
  private static PlayerData playerDataStatic;
  private static int backupLimitFiles = 50;

  public static bool ExistsProfileFolder(int _slot)
  {
    if (_slot > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Application.persistentDataPath);
      stringBuilder.Append("/");
      stringBuilder.Append((ulong) SteamManager.Instance.steamId);
      stringBuilder.Append("/");
      stringBuilder.Append("profile");
      stringBuilder.Append(_slot);
      if (!Directory.Exists(stringBuilder.ToString()))
        return false;
    }
    return true;
  }

  public static void CreateProfileFolder(int _slot, string _name)
  {
    if (_slot < 1 || _slot > 4 || _name == "")
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append("profile");
    stringBuilder.Append(_slot);
    if (Directory.Exists(stringBuilder.ToString()))
      Directory.Delete(stringBuilder.ToString());
    Directory.CreateDirectory(stringBuilder.ToString());
    if (Directory.Exists(stringBuilder.ToString()))
    {
      stringBuilder.Append("/");
      stringBuilder.Append("profile_name.ato");
      File.WriteAllText(stringBuilder.ToString(), _name);
      MainMenuManager.Instance.LoadProfiles();
      MainMenuManager.Instance.UseProfile(_slot);
    }
  }

  public static string[] GetProfileNames()
  {
    string[] profileNames = new string[5];
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append(Application.persistentDataPath);
    stringBuilder1.Append("/");
    stringBuilder1.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder1.Append("/");
    for (int index = 0; index < 5; ++index)
    {
      if (index == 0)
      {
        profileNames[index] = Texts.Instance.GetText("default");
      }
      else
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.Append(stringBuilder1.ToString());
        stringBuilder2.Append("profile");
        stringBuilder2.Append(index);
        if (!Directory.Exists(stringBuilder2.ToString()))
        {
          profileNames[index] = "";
        }
        else
        {
          stringBuilder2.Append("/");
          stringBuilder2.Append("profile_name.ato");
          if (!File.Exists(stringBuilder2.ToString()))
          {
            profileNames[index] = "";
          }
          else
          {
            FileStream fileStream = new FileStream(stringBuilder2.ToString(), FileMode.Open);
            if (fileStream.Length == 0L)
            {
              fileStream.Close();
              profileNames[index] = "";
            }
            else
            {
              string end = new StreamReader((Stream) fileStream, Encoding.UTF8).ReadToEnd();
              profileNames[index] = end;
              fileStream.Close();
            }
          }
        }
      }
    }
    return profileNames;
  }

  public static void SaveGame(int slot, bool backUp = false)
  {
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) ("******* SAVE GAME (" + slot.ToString() + ") *******"));
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append(Application.persistentDataPath);
    stringBuilder1.Append("/");
    stringBuilder1.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder1.Append("/");
    stringBuilder1.Append(GameManager.Instance.ProfileFolder);
    stringBuilder1.Append(SaveManager.saveGameName);
    stringBuilder1.Append(slot);
    StringBuilder stringBuilder2 = new StringBuilder();
    stringBuilder2.Append(stringBuilder1.ToString());
    stringBuilder2.Append(SaveManager.saveGameExtensionBK);
    stringBuilder1.Append(SaveManager.saveGameExtension);
    string str = stringBuilder1.ToString();
    string destFileName = stringBuilder2.ToString();
    if (backUp && File.Exists(str))
      File.Copy(str, destFileName, true);
    DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
    bool newGame = false;
    try
    {
      FileStream fileStream = new FileStream(str, FileMode.Create, FileAccess.Write);
      using (CryptoStream cryptoStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Write))
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        GameData gameData = new GameData();
        gameData.FillData(newGame);
        CryptoStream serializationStream = cryptoStream;
        GameData graph = gameData;
        binaryFormatter.Serialize((Stream) serializationStream, (object) graph);
        cryptoStream.Close();
      }
      fileStream.Close();
    }
    catch
    {
      GameManager.Instance.AbortGameSave();
    }
  }

  public static void LoadGame(int slot)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveGameName);
    stringBuilder.Append(slot);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    if (!File.Exists(path))
    {
      Debug.Log((object) "ERROR File does not exists");
    }
    else
    {
      FileStream fileStream = new FileStream(path, FileMode.Open);
      if (fileStream.Length == 0L)
      {
        fileStream.Close();
      }
      else
      {
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        try
        {
          CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
          (new BinaryFormatter().Deserialize((Stream) serializationStream) as GameData).LoadData();
          serializationStream.Close();
        }
        catch (SerializationException ex)
        {
          Debug.Log((object) ("Failed to deserialize LoadGame. Reason: " + ex.Message));
        }
        fileStream.Close();
      }
    }
  }

  public static void DeleteGame(int slot)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveGameName);
    stringBuilder.Append(slot);
    string path1 = stringBuilder.ToString() + SaveManager.saveGameExtension;
    if (File.Exists(path1))
      File.Delete(path1);
    string path2 = stringBuilder.ToString() + SaveManager.saveGameExtensionBK;
    Debug.Log((object) ("REMOVED file " + path2));
    if (File.Exists(path2))
      File.Delete(path2);
    SaveManager.DeleteSaveGameTurn(slot);
  }

  public static GameData[] SaveGamesList()
  {
    GameData[] gameDataArray = new GameData[36];
    StringBuilder stringBuilder = new StringBuilder();
    for (int index1 = 0; index1 < gameDataArray.Length; ++index1)
    {
      stringBuilder.Clear();
      stringBuilder.Append(Application.persistentDataPath);
      stringBuilder.Append("/");
      stringBuilder.Append((ulong) SteamManager.Instance.steamId);
      stringBuilder.Append("/");
      stringBuilder.Append(GameManager.Instance.ProfileFolder);
      stringBuilder.Append(SaveManager.saveGameName);
      stringBuilder.Append(index1);
      bool flag = false;
      for (int index2 = 0; index2 < 2; ++index2)
      {
        string str = index2 != 0 ? stringBuilder.ToString() + SaveManager.saveGameExtensionBK : stringBuilder.ToString() + SaveManager.saveGameExtension;
        if (File.Exists(str))
        {
          FileStream fileStream;
          try
          {
            fileStream = new FileStream(str, FileMode.Open);
            if (fileStream.Length == 0L)
            {
              fileStream.Close();
              continue;
            }
          }
          catch
          {
            continue;
          }
          DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
          try
          {
            CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            GameData gameData;
            try
            {
              gameData = binaryFormatter.Deserialize((Stream) serializationStream) as GameData;
            }
            catch
            {
              fileStream.Close();
              continue;
            }
            if (gameData != null)
            {
              gameDataArray[index1] = gameData;
              flag = true;
            }
            serializationStream.Close();
          }
          catch (SerializationException ex)
          {
            Debug.Log((object) ("Failed to deserialize LoadGame. Reason: " + ex.Message));
          }
          fileStream.Close();
          if (flag)
          {
            if (index2 != 0)
              File.Copy(str, stringBuilder.ToString() + SaveManager.saveGameExtension, true);
            else
              break;
          }
        }
      }
    }
    return gameDataArray;
  }

  public static void DeleteSaveGameTurn(int slot)
  {
    string path = SaveManager.PathSaveGameTurn(slot);
    if (!File.Exists(path))
      return;
    File.Delete(path);
  }

  public static void SaveGameTurn(int slot)
  {
    string path = SaveManager.PathSaveGameTurn(slot);
    DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
    try
    {
      FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
      using (CryptoStream cryptoStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Write))
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        GameTurnData gameTurnData = new GameTurnData();
        gameTurnData.FillData();
        CryptoStream serializationStream = cryptoStream;
        GameTurnData graph = gameTurnData;
        binaryFormatter.Serialize((Stream) serializationStream, (object) graph);
        cryptoStream.Close();
      }
      fileStream.Close();
    }
    catch
    {
    }
  }

  public static string LoadGameTurn(int slot)
  {
    string path = SaveManager.PathSaveGameTurn(slot);
    string str = "";
    if (File.Exists(path))
    {
      FileStream fileStream = new FileStream(path, FileMode.Open);
      if (fileStream.Length != 0L)
      {
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        try
        {
          CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
          (new BinaryFormatter().Deserialize((Stream) serializationStream) as GameTurnData).LoadData();
          serializationStream.Close();
        }
        catch (SerializationException ex)
        {
          if (GameManager.Instance.GetDeveloperMode())
            Debug.Log((object) ("Failed to deserialize LoadGame. Reason: " + ex.Message));
        }
      }
      fileStream.Close();
    }
    return str;
  }

  public static void CleanSavePlayerData()
  {
    SaveManager.SavePlayerData(true);
    SaveManager.SaveIntoPrefsInt("madnessLevel", 0);
    SaveManager.SaveIntoPrefsString("madnessCorruptors", "");
    SaveManager.SaveIntoPrefsInt("obeliskMadness", 0);
    PlayerManager.Instance.InitPlayerData();
    PlayerManager.Instance.PreBeginGame();
    PlayerManager.Instance.BeginGame();
    SaveManager.SavePlayerData();
  }

  public static void SavePlayerData(bool cleanThePlayerData = false, bool asBackup = false)
  {
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) nameof (SavePlayerData));
    PlayerData playerData;
    if (!cleanThePlayerData)
    {
      SaveManager.playerDataStatic = SaveManager.LoadPlayerData();
      playerData = SaveManager.ReBuildPlayerData(SaveManager.playerDataStatic);
    }
    else
    {
      playerData = new PlayerData();
      SaveManager.RestorePlayerData(playerData);
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    if (!Directory.Exists(stringBuilder.ToString()))
      Directory.CreateDirectory(stringBuilder.ToString());
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    if (!Directory.Exists(stringBuilder.ToString()))
      Directory.CreateDirectory(stringBuilder.ToString());
    stringBuilder.Append(SaveManager.savePlayerName);
    if (asBackup)
      stringBuilder.Append(SaveManager.backupName);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
    using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
    {
      using (CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Write))
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) playerData);
        serializationStream.Close();
      }
      fileStream.Close();
      Functions.DebugLogGD("SAVING PLAYER DATA", "save");
    }
  }

  public static void SavePlayerDataBackup()
  {
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) nameof (SavePlayerDataBackup));
    SaveManager.SavePlayerData(asBackup: true);
  }

  public static void BackupFromBackup()
  {
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) nameof (BackupFromBackup));
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append(Application.persistentDataPath);
    stringBuilder1.Append("/");
    stringBuilder1.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder1.Append("/");
    stringBuilder1.Append(GameManager.Instance.ProfileFolder);
    stringBuilder1.Append(SaveManager.savePlayerName);
    stringBuilder1.Append(SaveManager.backupName);
    string str = stringBuilder1.ToString() + SaveManager.saveGameExtension;
    if (!File.Exists(str))
      return;
    StringBuilder stringBuilder2 = new StringBuilder();
    int num = 0;
    for (int backupLimitFiles = SaveManager.backupLimitFiles; backupLimitFiles >= 0; --backupLimitFiles)
    {
      stringBuilder2.Clear();
      stringBuilder2.Append(stringBuilder1.ToString());
      stringBuilder2.Append("_");
      stringBuilder2.Append(backupLimitFiles);
      stringBuilder2.Append(SaveManager.saveGameExtension);
      if (!File.Exists(stringBuilder2.ToString()))
        num = backupLimitFiles;
      else
        break;
    }
    stringBuilder2.Clear();
    stringBuilder2.Append(stringBuilder1.ToString());
    stringBuilder2.Append("_");
    stringBuilder2.Append(num);
    stringBuilder2.Append(SaveManager.saveGameExtension);
    File.Copy(str, stringBuilder2.ToString(), true);
  }

  public static PlayerData LoadPlayerData(bool fromBackup = false)
  {
    if (fromBackup)
      SaveManager.BackupFromBackup();
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) ("LoadPlayerData frombackup=>" + fromBackup.ToString()));
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append(Application.persistentDataPath);
    stringBuilder1.Append("/");
    stringBuilder1.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder1.Append("/");
    stringBuilder1.Append(GameManager.Instance.ProfileFolder);
    stringBuilder1.Append(SaveManager.savePlayerName);
    if (fromBackup)
    {
      stringBuilder1.Append(SaveManager.backupName);
      if (GameManager.Instance.GetDeveloperMode())
        Debug.LogError((object) "Load player progress from backup");
    }
    StringBuilder stringBuilder2 = new StringBuilder();
    PlayerData playerData1 = (PlayerData) null;
    int num1 = -1;
    int num2 = -1;
    if (fromBackup)
      num1 = SaveManager.backupLimitFiles;
    for (int index = num1; index >= num2; --index)
    {
      stringBuilder2.Clear();
      stringBuilder2.Append(stringBuilder1.ToString());
      if (index > -1)
      {
        stringBuilder2.Append("_");
        stringBuilder2.Append(index);
      }
      stringBuilder2.Append(SaveManager.saveGameExtension);
      string path = stringBuilder2.ToString();
      if (GameManager.Instance.GetDeveloperMode())
        Debug.Log((object) ("Loading from -> " + path));
      if (File.Exists(path))
      {
        using (FileStream fileStream = new FileStream(path, FileMode.Open))
        {
          if (fileStream.Length == 0L)
          {
            fileStream.Close();
          }
          else
          {
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            PlayerData playerData2;
            try
            {
              CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
              BinaryFormatter binaryFormatter = new BinaryFormatter();
              try
              {
                playerData2 = binaryFormatter.Deserialize((Stream) serializationStream) as PlayerData;
                if (GameManager.Instance.GetDeveloperMode())
                  Debug.Log((object) ("Got GOOD PlayerData from => " + path));
              }
              catch (Exception ex)
              {
                Debug.LogWarning((object) ("AddCharges exception-> " + ex?.ToString()));
                if (GameManager.Instance.GetDeveloperMode())
                  Debug.Log((object) ("Corrupted Exception caught loading PlayerData from => " + path));
                fileStream.Close();
                File.Delete(path);
                continue;
              }
            }
            catch (SerializationException ex)
            {
              if (GameManager.Instance.GetDeveloperMode())
                Debug.Log((object) ("Failed to deserialize PlayerData. Reason: " + ex.Message));
              fileStream.Close();
              continue;
            }
            catch (DecoderFallbackException ex)
            {
              if (GameManager.Instance.GetDeveloperMode())
                Debug.Log((object) ("DecoderFallbackException. Reason: " + ex.Message));
              fileStream.Close();
              continue;
            }
            fileStream.Close();
            playerData1 = playerData2;
            break;
          }
        }
      }
    }
    return playerData1;
  }

  public static void SaveRuns(bool doBackup = false)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveRunsName);
    if (doBackup)
      stringBuilder.Append(SaveManager.backupName);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
    using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
    {
      using (CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Write))
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) PlayerManager.Instance.PlayerRuns);
        serializationStream.Close();
      }
      fileStream.Close();
    }
  }

  public static void LoadRuns(bool _useBackup = false)
  {
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) ("LoadRuns " + _useBackup.ToString()));
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveRunsName);
    if (!_useBackup)
    {
      stringBuilder.Append(SaveManager.saveGameExtension);
    }
    else
    {
      stringBuilder.Append(SaveManager.backupName);
      stringBuilder.Append(SaveManager.saveGameExtension);
    }
    string path = stringBuilder.ToString();
    if (File.Exists(path))
    {
      bool flag = false;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        if (fileStream.Length == 0L)
        {
          fileStream.Close();
          return;
        }
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        try
        {
          CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          try
          {
            PlayerManager.Instance.PlayerRuns = binaryFormatter.Deserialize((Stream) serializationStream) as List<string>;
            serializationStream.Close();
            flag = true;
          }
          catch (Exception ex)
          {
            if (GameManager.Instance.GetDeveloperMode())
              Debug.LogWarning((object) ("AddCharges exception-> " + ex?.ToString()));
            if (GameManager.Instance.GetDeveloperMode())
              Debug.Log((object) ("Corrupted Exception caught loading Runs from => " + path));
            if (!_useBackup)
            {
              fileStream.Close();
              SaveManager.LoadRuns(true);
              return;
            }
            fileStream.Close();
            SaveManager.SaveRuns();
            return;
          }
        }
        catch (SerializationException ex)
        {
          if (GameManager.Instance.GetDeveloperMode())
            Debug.Log((object) ("Failed to deserialize Runs. Reason: " + ex.Message));
        }
        fileStream.Close();
      }
      if (!flag)
        return;
      if (GameManager.Instance.GetDeveloperMode())
        Debug.Log((object) "We got Runs!");
      if (_useBackup)
        SaveManager.SaveRuns();
      else
        SaveManager.SaveRuns(true);
    }
    else if (!_useBackup)
      SaveManager.LoadRuns(true);
    else
      SaveManager.SaveRuns();
  }

  public static void SavePlayerDeck()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveDecksName);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
    using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
    {
      using (CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Write))
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) PlayerManager.Instance.PlayerSavedDeck);
        serializationStream.Close();
      }
      fileStream.Close();
    }
  }

  public static void LoadPlayerDeck()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveDecksName);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    if (File.Exists(path))
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        if (fileStream.Length == 0L)
        {
          fileStream.Close();
        }
        else
        {
          DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
          try
          {
            CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
            PlayerManager.Instance.PlayerSavedDeck = new BinaryFormatter().Deserialize((Stream) serializationStream) as PlayerDeck;
            serializationStream.Close();
          }
          catch (SerializationException ex)
          {
            if (GameManager.Instance.GetDeveloperMode())
              Debug.Log((object) ("Failed to deserialize Runs. Reason: " + ex.Message));
          }
          fileStream.Close();
        }
      }
    }
    else
    {
      if (!GameManager.Instance.GetDeveloperMode())
        return;
      Debug.Log((object) ("RunFile not found in " + path));
    }
  }

  public static void SavePlayerPerkConfig()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(SaveManager.savePerksName);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
    using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
    {
      using (CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateEncryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Write))
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) PlayerManager.Instance.PlayerSavedPerk);
        serializationStream.Close();
      }
      fileStream.Close();
    }
  }

  public static void LoadPlayerPerkConfig()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(SaveManager.savePerksName);
    stringBuilder.Append(SaveManager.saveGameExtension);
    string path = stringBuilder.ToString();
    if (File.Exists(path))
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        if (fileStream.Length == 0L)
        {
          fileStream.Close();
        }
        else
        {
          DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
          try
          {
            CryptoStream serializationStream = new CryptoStream((Stream) fileStream, cryptoServiceProvider.CreateDecryptor(SaveManager.key, SaveManager.iv), CryptoStreamMode.Read);
            PlayerManager.Instance.PlayerSavedPerk = new BinaryFormatter().Deserialize((Stream) serializationStream) as PlayerPerk;
            serializationStream.Close();
          }
          catch (SerializationException ex)
          {
            if (GameManager.Instance.GetDeveloperMode())
              Debug.Log((object) ("Failed to deserialize PlayerPerks. Reason: " + ex.Message));
          }
          fileStream.Close();
        }
      }
    }
    else
    {
      if (!GameManager.Instance.GetDeveloperMode())
        return;
      Debug.Log((object) ("PlayerPerks not found in " + path));
    }
  }

  public static void ResetTutorial()
  {
    PlayerManager.Instance.TutorialWatched = new List<string>();
    SaveManager.SavePlayerData();
  }

  public static void RestorePlayerData(PlayerData playerData)
  {
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) nameof (RestorePlayerData));
    string str1 = SteamManager.Instance.steamId.ToString();
    if (playerData.SteamUserId == null)
      playerData.SteamUserId = str1;
    if (playerData.SteamUserId != str1 && !GameManager.Instance.GetDeveloperMode())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Application.persistentDataPath);
      stringBuilder.Append("/");
      stringBuilder.Append((ulong) SteamManager.Instance.steamId);
      stringBuilder.Append("/");
      stringBuilder.Append(GameManager.Instance.ProfileFolder);
      stringBuilder.Append(SaveManager.savePlayerName);
      string str2 = stringBuilder.ToString();
      File.Move(str2 + SaveManager.saveGameExtension, str2 + "-error-" + Functions.GetTimestamp() + SaveManager.saveGameExtension);
    }
    else
    {
      PlayerManager.Instance.LastUsedTeam = playerData.LastUsedTeam;
      PlayerManager.Instance.TutorialWatched = playerData.TutorialWatched;
      PlayerManager.Instance.UnlockedHeroes = playerData.UnlockedHeroes == null ? new List<string>() : playerData.UnlockedHeroes;
      PlayerManager.Instance.UnlockedCards = playerData.UnlockedCards == null ? new List<string>() : playerData.UnlockedCards;
      PlayerManager.Instance.UnlockedNodes = playerData.UnlockedNodes == null ? new List<string>() : playerData.UnlockedNodes;
      PlayerManager.Instance.TreasuresClaimed = playerData.TreasuresClaimed == null ? new List<string>() : playerData.TreasuresClaimed;
      PlayerManager.Instance.UnlockedCardsByGame = playerData.UnlockedCardsByGame == null ? new Dictionary<string, List<string>>() : playerData.UnlockedCardsByGame;
      int num1 = playerData.NgUnlocked ? 1 : 0;
      PlayerManager.Instance.NgUnlocked = playerData.NgUnlocked;
      int ngLevel = playerData.NgLevel;
      PlayerManager.Instance.NgLevel = playerData.NgLevel;
      int playerRankProgress = playerData.PlayerRankProgress;
      PlayerManager.Instance.SetPlayerRankProgress(playerData.PlayerRankProgress);
      int adventureMadnessLevel = playerData.MaxAdventureMadnessLevel;
      PlayerManager.Instance.MaxAdventureMadnessLevel = playerData.MaxAdventureMadnessLevel;
      if (PlayerManager.Instance.MaxAdventureMadnessLevel == 0)
        PlayerManager.Instance.MaxAdventureMadnessLevel = PlayerManager.Instance.NgLevel;
      int obeliskMadnessLevel = playerData.ObeliskMadnessLevel;
      PlayerManager.Instance.ObeliskMadnessLevel = playerData.ObeliskMadnessLevel;
      PlayerManager.Instance.BossesKilled = playerData.BossesKilled;
      PlayerManager.Instance.BossesKilledName = playerData.BossesKilledName;
      PlayerManager.Instance.MonstersKilled = playerData.MonstersKilled;
      PlayerManager.Instance.ExpGained = playerData.ExpGained;
      PlayerManager.Instance.CardsCrafted = playerData.CardsCrafted;
      PlayerManager.Instance.CardsUpgraded = playerData.CardsUpgraded;
      PlayerManager.Instance.GoldGained = playerData.GoldGained;
      PlayerManager.Instance.DustGained = playerData.DustGained;
      PlayerManager.Instance.BestScore = playerData.BestScore;
      PlayerManager.Instance.PurchasedItems = playerData.PurchasedItems;
      PlayerManager.Instance.CorruptionsCompleted = playerData.CorruptionsCompleted;
      PlayerManager.Instance.SupplyGained = playerData.SupplyGained;
      PlayerManager.Instance.SupplyActual = playerData.SupplyActual;
      if (playerData.HeroProgress != null)
      {
        foreach (KeyValuePair<string, int> keyValuePair in playerData.HeroProgress)
          PlayerManager.Instance.HeroProgress.Add(keyValuePair.Key, keyValuePair.Value);
      }
      PlayerManager.Instance.HeroPerks = playerData.HeroPerks;
      if (PlayerManager.Instance.HeroPerks != null)
      {
        foreach (KeyValuePair<string, List<string>> heroPerk in PlayerManager.Instance.HeroPerks)
        {
          for (int index = heroPerk.Value.Count - 1; index >= 0; --index)
          {
            PerkData perkData = Globals.Instance.GetPerkData(heroPerk.Value[index]);
            if (!((UnityEngine.Object) perkData == (UnityEngine.Object) null))
            {
              int num2 = perkData.MainPerk ? 1 : 0;
              if (perkData.MainPerk)
                continue;
            }
            heroPerk.Value.RemoveAt(index);
          }
        }
      }
      PlayerManager.Instance.SupplyBought = playerData.SupplyBought;
      PlayerManager.Instance.SkinUsed = playerData.SkinUsed == null ? new Dictionary<string, string>() : playerData.SkinUsed;
      PlayerManager.Instance.CardbackUsed = playerData.CardbackUsed == null ? new Dictionary<string, string>() : playerData.CardbackUsed;
      if (GameManager.Instance.GetDeveloperMode())
      {
        PlayerManager.Instance.NgLevel = 8;
        PlayerManager.Instance.ObeliskMadnessLevel = 10;
      }
      SaveManager.LoadRuns();
      PlayerManager.Instance.NormalizePlayerRuns();
    }
  }

  private static PlayerData ReBuildPlayerData(PlayerData _playerData)
  {
    if (_playerData == null)
      _playerData = new PlayerData();
    _playerData.LastUsedTeam = PlayerManager.Instance.LastUsedTeam;
    _playerData.TutorialWatched = PlayerManager.Instance.TutorialWatched;
    _playerData.UnlockedHeroes = PlayerManager.Instance.UnlockedHeroes;
    _playerData.UnlockedCards = PlayerManager.Instance.UnlockedCards;
    _playerData.UnlockedNodes = PlayerManager.Instance.UnlockedNodes;
    _playerData.PlayerRuns = PlayerManager.Instance.PlayerRuns;
    _playerData.TreasuresClaimed = PlayerManager.Instance.TreasuresClaimed;
    _playerData.UnlockedCardsByGame = PlayerManager.Instance.UnlockedCardsByGame;
    _playerData.NgUnlocked = PlayerManager.Instance.NgUnlocked;
    _playerData.NgLevel = PlayerManager.Instance.NgLevel;
    _playerData.PlayerRankProgress = PlayerManager.Instance.GetPlayerRankProgress();
    _playerData.MaxAdventureMadnessLevel = PlayerManager.Instance.MaxAdventureMadnessLevel;
    _playerData.ObeliskMadnessLevel = PlayerManager.Instance.ObeliskMadnessLevel;
    _playerData.BossesKilled = PlayerManager.Instance.BossesKilled;
    _playerData.BossesKilledName = PlayerManager.Instance.BossesKilledName;
    _playerData.MonstersKilled = PlayerManager.Instance.MonstersKilled;
    _playerData.ExpGained = PlayerManager.Instance.ExpGained;
    _playerData.CardsCrafted = PlayerManager.Instance.CardsCrafted;
    _playerData.CardsUpgraded = PlayerManager.Instance.CardsUpgraded;
    _playerData.GoldGained = PlayerManager.Instance.GoldGained;
    _playerData.DustGained = PlayerManager.Instance.DustGained;
    _playerData.BestScore = PlayerManager.Instance.BestScore;
    _playerData.PurchasedItems = PlayerManager.Instance.PurchasedItems;
    _playerData.CorruptionsCompleted = PlayerManager.Instance.CorruptionsCompleted;
    _playerData.SupplyGained = PlayerManager.Instance.SupplyGained;
    _playerData.SupplyActual = PlayerManager.Instance.SupplyActual;
    _playerData.HeroProgress = PlayerManager.Instance.HeroProgress;
    _playerData.HeroPerks = PlayerManager.Instance.HeroPerks;
    _playerData.SupplyBought = PlayerManager.Instance.SupplyBought;
    _playerData.SkinUsed = PlayerManager.Instance.SkinUsed;
    _playerData.CardbackUsed = PlayerManager.Instance.CardbackUsed;
    return _playerData;
  }

  public static void SaveRun(PlayerRun _playerRun)
  {
    List<string> stringList = PlayerManager.Instance.PlayerRuns ?? new List<string>();
    string json = JsonUtility.ToJson((object) _playerRun);
    stringList.Add(json);
    PlayerManager.Instance.PlayerRuns = stringList;
    if (stringList.Count > 4)
      PlayerManager.Instance.AchievementUnlock("MISC_ADVENTURER");
    if (_playerRun.TotalPlayers > 1)
      PlayerManager.Instance.AchievementUnlock("MISC_ADVENTURERGUILD");
    SaveManager.SaveRuns();
  }

  public static string PathSaveGameTurn(int slot)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Application.persistentDataPath);
    stringBuilder.Append("/");
    stringBuilder.Append((ulong) SteamManager.Instance.steamId);
    stringBuilder.Append("/");
    stringBuilder.Append(GameManager.Instance.ProfileFolder);
    stringBuilder.Append(SaveManager.saveGameName);
    stringBuilder.Append(slot);
    stringBuilder.Append(SaveManager.saveGameTurnExtension);
    stringBuilder.Append(SaveManager.saveGameExtension);
    return stringBuilder.ToString();
  }

  public static bool PrefsHasKey(string key) => PlayerPrefs.HasKey(key);

  public static void PrefsRemoveKey(string key) => PlayerPrefs.DeleteKey(key);

  public static void SavePrefs() => PlayerPrefs.Save();

  public static void SaveIntoPrefsInt(string key, int value) => PlayerPrefs.SetInt(key, value);

  public static int LoadPrefsInt(string key) => PlayerPrefs.GetInt(key);

  public static void SaveIntoPrefsFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);

  public static float LoadPrefsFloat(string key) => PlayerPrefs.GetFloat(key);

  public static void SaveIntoPrefsBool(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);

  public static bool LoadPrefsBool(string key) => PlayerPrefs.GetInt(key) == 1;

  public static void SaveIntoPrefsString(string key, string value) => PlayerPrefs.SetString(key, value);

  public static string LoadPrefsString(string key) => PlayerPrefs.GetString(key);
}
