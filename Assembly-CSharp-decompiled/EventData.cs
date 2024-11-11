// Decompiled with JetBrains decompiler
// Type: EventData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event Data", order = 62)]
public class EventData : ScriptableObject
{
  [SerializeField]
  private string eventId;
  [SerializeField]
  private string eventUniqueId;
  [SerializeField]
  private string eventName;
  [SerializeField]
  private EventRequirementData requirement;
  [SerializeField]
  private SubClassData requiredClass;
  [TextArea]
  [SerializeField]
  private string description;
  [TextArea]
  [SerializeField]
  private string descriptionAction;
  [SerializeField]
  private Sprite eventSpriteMap;
  [SerializeField]
  private Sprite eventSpriteDecor;
  [SerializeField]
  private Enums.MapIconShader eventIconShader;
  [SerializeField]
  private Sprite eventSpriteBook;
  [SerializeField]
  private bool historyMode;
  [Header("Event Tier")]
  [SerializeField]
  private Enums.CombatTier eventTier;
  [Header("Reply List")]
  [SerializeField]
  private int replyRandom;
  [SerializeField]
  private EventReplyData[] replys;

  public void Init()
  {
    List<EventReplyData> eventReplyDataList = new List<EventReplyData>();
    for (int index = 0; index < this.replys.Length; ++index)
    {
      EventReplyData reply = this.replys[index];
      if (reply != null)
      {
        if (!reply.RepeatForAllCharacters)
        {
          reply.IndexForAnswerTranslation = index;
          eventReplyDataList.Add(reply);
        }
        else
        {
          foreach (KeyValuePair<string, SubClassData> keyValuePair in Globals.Instance.SubClass)
          {
            if ((Object) keyValuePair.Value != (Object) null && keyValuePair.Value.MainCharacter)
            {
              EventReplyData eventReplyData = reply.ShallowCopy();
              eventReplyData.RequiredClass = keyValuePair.Value;
              eventReplyData.IndexForAnswerTranslation = index;
              eventReplyDataList.Add(eventReplyData);
            }
          }
        }
      }
    }
    this.replys = eventReplyDataList.ToArray();
  }

  public string EventId
  {
    get => this.eventId;
    set => this.eventId = value;
  }

  public string EventName
  {
    get => this.eventName;
    set => this.eventName = value;
  }

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public string DescriptionAction
  {
    get => this.descriptionAction;
    set => this.descriptionAction = value;
  }

  public Sprite EventSpriteMap
  {
    get => this.eventSpriteMap;
    set => this.eventSpriteMap = value;
  }

  public Sprite EventSpriteBook
  {
    get => this.eventSpriteBook;
    set => this.eventSpriteBook = value;
  }

  public bool HistoryMode
  {
    get => this.historyMode;
    set => this.historyMode = value;
  }

  public EventReplyData[] Replys
  {
    get => this.replys;
    set => this.replys = value;
  }

  public EventRequirementData Requirement
  {
    get => this.requirement;
    set => this.requirement = value;
  }

  public SubClassData RequiredClass
  {
    get => this.requiredClass;
    set => this.requiredClass = value;
  }

  public Enums.MapIconShader EventIconShader
  {
    get => this.eventIconShader;
    set => this.eventIconShader = value;
  }

  public Enums.CombatTier EventTier
  {
    get => this.eventTier;
    set => this.eventTier = value;
  }

  public Sprite EventSpriteDecor
  {
    get => this.eventSpriteDecor;
    set => this.eventSpriteDecor = value;
  }

  public string EventUniqueId
  {
    get => this.eventUniqueId;
    set => this.eventUniqueId = value;
  }

  public int ReplyRandom
  {
    get => this.replyRandom;
    set => this.replyRandom = value;
  }
}
