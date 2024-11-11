// Decompiled with JetBrains decompiler
// Type: NodeEditorName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class NodeEditorName : MonoBehaviour
{
  public TMP_Text nodeIdText;

  private void Awake()
  {
  }

  private void Start()
  {
    this.nodeIdText.text = "";
    this.nodeIdText.gameObject.SetActive(false);
  }

  private void Update()
  {
  }

  private void DoName()
  {
    NodeData nodeData = this.GetComponent<Node>().nodeData;
    if ((Object) nodeData != (Object) null)
      this.nodeIdText.text = nodeData.NodeId;
    this.nodeIdText.gameObject.SetActive(true);
  }
}
