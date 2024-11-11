// Decompiled with JetBrains decompiler
// Type: Photon.Chat.Demo.AppSettingsExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Realtime;

namespace Photon.Chat.Demo
{
  public static class AppSettingsExtensions
  {
    public static ChatAppSettings GetChatSettings(this AppSettings appSettings) => new ChatAppSettings()
    {
      AppIdChat = appSettings.AppIdChat,
      AppVersion = appSettings.AppVersion,
      FixedRegion = appSettings.IsBestRegion ? (string) null : appSettings.FixedRegion,
      NetworkLogging = appSettings.NetworkLogging,
      Protocol = appSettings.Protocol,
      EnableProtocolFallback = appSettings.EnableProtocolFallback,
      Server = appSettings.IsDefaultNameServer ? (string) null : appSettings.Server,
      Port = (ushort) appSettings.Port
    };
  }
}
