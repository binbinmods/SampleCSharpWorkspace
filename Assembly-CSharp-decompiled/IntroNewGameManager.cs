// Decompiled with JetBrains decompiler
// Type: IntroNewGameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroNewGameManager : MonoBehaviour
{
  public TMP_Text title;
  public TMP_Text body;
  public Transform buttonContinue;
  public Transform bgSenenthia;
  public Transform bgHatch;
  public Transform bgVelkarath;
  public Transform bgAquarfall;
  public Transform bgSpiderLair;
  public Transform bgVoid;
  public Transform bgFaeborg;
  public Transform bgUlminin;
  public Transform bgPyramid;
  public Transform bgEndEarly;
  public Transform bgBlackForge;
  public Transform bgFrozenSewers;
  public Transform bgWolfWars;
  private Coroutine coFade;

  public static IntroNewGameManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) IntroNewGameManager.Instance == (Object) null)
      IntroNewGameManager.Instance = this;
    else if ((Object) IntroNewGameManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.LoadByName("IntroNewGame");
    }
    else
    {
      GameManager.Instance.SetCamera();
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  private void Start()
  {
    string currentMapNode = AtOManager.Instance.currentMapNode;
    this.bgSenenthia.gameObject.SetActive(false);
    this.bgHatch.gameObject.SetActive(false);
    this.bgVelkarath.gameObject.SetActive(false);
    this.bgAquarfall.gameObject.SetActive(false);
    this.bgSpiderLair.gameObject.SetActive(false);
    this.bgFaeborg.gameObject.SetActive(false);
    this.bgVoid.gameObject.SetActive(false);
    this.bgEndEarly.gameObject.SetActive(false);
    this.bgFrozenSewers.gameObject.SetActive(false);
    this.bgBlackForge.gameObject.SetActive(false);
    this.bgWolfWars.gameObject.SetActive(false);
    this.bgUlminin.gameObject.SetActive(false);
    this.bgPyramid.gameObject.SetActive(false);
    if (AtOManager.Instance.IsAdventureCompleted())
      this.DoFinishGame();
    else if (currentMapNode != "tutorial_0" && currentMapNode != "sen_0" && currentMapNode != "secta_0" && currentMapNode != "velka_0" && currentMapNode != "aqua_0" && currentMapNode != "spider_0" && currentMapNode != "voidlow_0" && currentMapNode != "faen_0" && currentMapNode != "forge_0" && currentMapNode != "sewers_0" && currentMapNode != "sewers_1" && currentMapNode != "wolf_0" && currentMapNode != "ulmin_0" && currentMapNode != "pyr_0")
      GameManager.Instance.ChangeScene("Map");
    else
      this.DoIntro(currentMapNode);
  }

  private void DoFinishGame()
  {
    this.bgEndEarly.gameObject.SetActive(true);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+1>");
    stringBuilder.Append(Texts.Instance.GetText("congratulations"));
    stringBuilder.Append("</size><br><color=#FFF>");
    stringBuilder.Append("Across the Obelisk");
    this.title.text = stringBuilder.ToString();
    this.body.text = Texts.Instance.GetText("actEndGame");
    this.buttonContinue.gameObject.SetActive(true);
    GameManager.Instance.SceneLoaded();
  }

  private void DoIntro(string currentMapNode)
  {
    if (currentMapNode == "sen_0" || currentMapNode == "tutorial_0")
    {
      AtOManager.Instance.CinematicId = "intro";
      AtOManager.Instance.LaunchCinematic();
    }
    else
    {
      int townTier = AtOManager.Instance.GetTownTier();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<size=+2>");
      string _id = "";
      if (currentMapNode == "sen_0" || currentMapNode == "tutorial_0")
      {
        stringBuilder.Append(string.Format(Texts.Instance.GetText("actNumber"), (object) 1));
        this.bgSenenthia.gameObject.SetActive(true);
        _id = "Senenthia";
      }
      else
      {
        switch (currentMapNode)
        {
          case "secta_0":
            stringBuilder.Append(Texts.Instance.GetText("theHatch"));
            this.bgHatch.gameObject.SetActive(true);
            _id = "Senenthia";
            break;
          case "spider_0":
            stringBuilder.Append(Texts.Instance.GetText("spiderLair"));
            this.bgSpiderLair.gameObject.SetActive(true);
            _id = "Aquarfall";
            break;
          case "forge_0":
            stringBuilder.Append(Texts.Instance.GetText("blackForge"));
            this.bgBlackForge.gameObject.SetActive(true);
            _id = "Velkarath";
            break;
          default:
            if (currentMapNode == "sewers_0" || currentMapNode == "sewers_1")
            {
              stringBuilder.Append(Texts.Instance.GetText("frozenSewers"));
              this.bgFrozenSewers.gameObject.SetActive(true);
              _id = "Faeborg";
              break;
            }
            switch (currentMapNode)
            {
              case "wolf_0":
                stringBuilder.Append(Texts.Instance.GetText("wolfWars"));
                this.bgWolfWars.gameObject.SetActive(true);
                _id = "Senenthia";
                break;
              case "pyr_0":
                stringBuilder.Append(Texts.Instance.GetText("pyramid"));
                this.bgPyramid.gameObject.SetActive(true);
                _id = "Ulminin";
                break;
              default:
                stringBuilder.Append(string.Format(Texts.Instance.GetText("actNumber"), (object) (townTier + 2)));
                switch (currentMapNode)
                {
                  case "velka_0":
                    this.bgVelkarath.gameObject.SetActive(true);
                    _id = "Velkarath";
                    break;
                  case "aqua_0":
                    this.bgAquarfall.gameObject.SetActive(true);
                    _id = "Aquarfall";
                    break;
                  case "voidlow_0":
                    this.bgVoid.gameObject.SetActive(true);
                    _id = "TheVoid";
                    break;
                  case "faen_0":
                    this.bgFaeborg.gameObject.SetActive(true);
                    _id = "Faeborg";
                    break;
                  case "ulmin_0":
                    this.bgUlminin.gameObject.SetActive(true);
                    _id = "Ulminin";
                    break;
                }
                break;
            }
            break;
        }
      }
      stringBuilder.Append("</size><br><color=#FFF>");
      stringBuilder.Append(Texts.Instance.GetText(_id));
      this.title.text = stringBuilder.ToString();
      if (currentMapNode == "sen_0" || currentMapNode == "tutorial_0" || currentMapNode == "velka_0" || currentMapNode == "aqua_0" || currentMapNode == "voidlow_0" || currentMapNode == "faen_0" || currentMapNode == "ulmin_0")
      {
        this.body.text = currentMapNode == "sen_0" || currentMapNode == "tutorial_0" ? Texts.Instance.GetText("act0Intro") : Texts.Instance.GetText("act" + (townTier + 1).ToString() + "Intro");
        this.buttonContinue.gameObject.SetActive(true);
      }
      else if (currentMapNode == "wolf_0")
      {
        this.body.text = Texts.Instance.GetText("wolfWarsIntro");
        this.buttonContinue.gameObject.SetActive(true);
      }
      else
      {
        this.body.text = "";
        this.buttonContinue.gameObject.SetActive(true);
        this.coFade = this.StartCoroutine(this.FadeOut());
      }
      this.body.GetComponent<TextFade>().enabled = true;
      GameManager.Instance.SceneLoaded();
      this.ControllerMovement(true);
    }
  }

  private IEnumerator FadeOut()
  {
    yield return (object) Globals.Instance.WaitForSeconds(4f);
    this.SkipIntro();
  }

  public void SkipIntro()
  {
    if (this.coFade != null)
      this.StopCoroutine(this.coFade);
    if (AtOManager.Instance.IsAdventureCompleted())
      SceneStatic.LoadByName("FinishRun");
    else
      GameManager.Instance.ChangeScene("Map");
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    if (!(goingUp | goingLeft | goingRight | goingDown))
      return;
    Mouse.current.WarpCursorPosition((Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.buttonContinue.position));
  }
}
