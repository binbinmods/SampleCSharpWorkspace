// Decompiled with JetBrains decompiler
// Type: ChallengeBossBanners
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ChallengeBossBanners : MonoBehaviour
{
  public SpriteRenderer banner0Sprite;
  public SpriteRenderer banner1Sprite;
  public SpriteRenderer banner2Sprite;
  public Transform banner0Killed;
  public Transform banner1Killed;
  public Transform banner2Killed;
  private List<string> bossNodes;
  private List<string> bossNames;

  private void Awake() => this.Hide();

  public bool IsActive() => this.gameObject.activeSelf;

  public void SetBosses()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      this.Hide();
    }
    else
    {
      this.bossNodes = new List<string>();
      this.bossNames = new List<string>();
      for (int _index = 0; _index < 3; ++_index)
      {
        Sprite _sprite = (Sprite) null;
        if (!GameManager.Instance.IsWeeklyChallenge())
        {
          string nodeSelectedId = "";
          string str1 = "";
          NodeData nodeData = (NodeData) null;
          CombatData combatData = (CombatData) null;
          foreach (KeyValuePair<string, NodeData> keyValuePair in Globals.Instance.NodeDataSource)
          {
            nodeData = keyValuePair.Value;
            string str2 = "";
            Enums.CombatTier combatTier = Enums.CombatTier.T0;
            switch (_index)
            {
              case 0:
                str2 = AtOManager.Instance.obeliskLow.ToLower();
                combatTier = Enums.CombatTier.T8;
                break;
              case 1:
                str2 = AtOManager.Instance.obeliskHigh.ToLower();
                combatTier = Enums.CombatTier.T9;
                break;
              case 2:
                str2 = AtOManager.Instance.obeliskFinal.ToLower();
                break;
            }
            if (nodeData.NodeZone.ZoneId.ToLower() == str2)
            {
              if (_index < 2)
              {
                if (nodeData.NodeCombatTier == combatTier && nodeData.NodeCombat.Length != 0 && (Object) nodeData.NodeCombat[0] != (Object) null)
                {
                  nodeSelectedId = nodeData.NodeId;
                  str1 = nodeData.NodeCombat[0].CombatId;
                  break;
                }
              }
              else if (nodeData.NodeId == "of1_10" || nodeData.NodeId == "of2_10")
              {
                nodeSelectedId = nodeData.NodeId;
                Random.InitState((nodeSelectedId + AtOManager.Instance.GetGameId() + "finalBoss").GetDeterministicHashCode());
                int index = Random.Range(0, nodeData.NodeCombat.Length);
                combatData = nodeData.NodeCombat[index];
                str1 = combatData.CombatId;
              }
            }
          }
          this.bossNodes.Add(nodeSelectedId);
          NPCData[] npcDataArray;
          if (_index < 2)
          {
            int deterministicHashCode = (nodeSelectedId + AtOManager.Instance.GetGameId() + str1).GetDeterministicHashCode();
            npcDataArray = Functions.GetRandomCombat(nodeData.NodeCombatTier, deterministicHashCode, nodeSelectedId, true);
          }
          else
            npcDataArray = combatData.NPCList;
          if (npcDataArray != null)
          {
            for (int index = 0; index < npcDataArray.Length; ++index)
            {
              if ((Object) npcDataArray[index] != (Object) null && (npcDataArray[index].IsNamed || npcDataArray[index].IsBoss))
              {
                _sprite = npcDataArray[index].SpriteSpeed;
                break;
              }
            }
          }
        }
        else
        {
          ChallengeData weeklyData = Globals.Instance.GetWeeklyData(AtOManager.Instance.GetWeekly());
          switch (_index)
          {
            case 0:
              _sprite = weeklyData.Boss1.SpriteSpeed;
              this.bossNames.Add(weeklyData.Boss1.NPCName);
              break;
            case 1:
              _sprite = weeklyData.Boss2.SpriteSpeed;
              this.bossNames.Add(weeklyData.Boss2.NPCName);
              break;
            case 2:
              _sprite = weeklyData.Boss2.SpriteSpeed;
              for (int index = 0; index < weeklyData.BossCombat.NPCList.Length; ++index)
              {
                if ((Object) weeklyData.BossCombat.NPCList[index] != (Object) null && (weeklyData.BossCombat.NPCList[index].IsNamed || weeklyData.BossCombat.NPCList[index].IsBoss))
                {
                  _sprite = weeklyData.BossCombat.NPCList[index].SpriteSpeed;
                  this.bossNames.Add(weeklyData.BossCombat.NPCList[index].NPCName);
                  break;
                }
              }
              break;
          }
        }
        this.SetBossSprite(_index, _sprite);
      }
      for (int index = 0; index < 3; ++index)
      {
        if (!GameManager.Instance.IsWeeklyChallenge())
          this.SetBossKilled(index, AtOManager.Instance.mapVisitedNodes.Contains(this.bossNodes[index]));
        else
          this.SetBossKilled(index, AtOManager.Instance.IsBossKilled(this.bossNames[index]));
      }
      this.Show();
    }
  }

  public void Show()
  {
    if (this.gameObject.activeSelf)
      return;
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    if (!this.gameObject.activeSelf)
      return;
    this.gameObject.SetActive(false);
  }

  public void SetBossKilled(int _index, bool _status)
  {
    switch (_index)
    {
      case 0:
        if (_status)
        {
          if (this.banner0Killed.gameObject.activeSelf)
            break;
          this.banner0Killed.gameObject.SetActive(true);
          break;
        }
        if (!this.banner0Killed.gameObject.activeSelf)
          break;
        this.banner0Killed.gameObject.SetActive(false);
        break;
      case 1:
        if (_status)
        {
          if (this.banner1Killed.gameObject.activeSelf)
            break;
          this.banner1Killed.gameObject.SetActive(true);
          break;
        }
        if (!this.banner1Killed.gameObject.activeSelf)
          break;
        this.banner1Killed.gameObject.SetActive(false);
        break;
      default:
        if (_status)
        {
          if (this.banner2Killed.gameObject.activeSelf)
            break;
          this.banner2Killed.gameObject.SetActive(true);
          break;
        }
        if (!this.banner2Killed.gameObject.activeSelf)
          break;
        this.banner2Killed.gameObject.SetActive(false);
        break;
    }
  }

  public void SetBossSprite(int _index, Sprite _sprite)
  {
    if (_index == 0)
      this.banner0Sprite.sprite = _sprite;
    else if (_index == 1)
      this.banner1Sprite.sprite = _sprite;
    else
      this.banner2Sprite.sprite = _sprite;
  }
}
