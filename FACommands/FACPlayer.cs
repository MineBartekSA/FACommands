// Decompiled with JetBrains decompiler
// Type: FACommands.FACPlayer
// Assembly: FACommands, Version=1.2.7.0, Culture=neutral, PublicKeyToken=null
// MVID: CFC654C6-48C4-48BE-A6A4-7655CB7E5574
// Assembly location: C:\Users\SKOCZEN\Documents\TShock\FACommands.dll

using TShockAPI;

namespace FACommands
{
  public class FACPlayer
  {
    public int Index { get; set; }

    public TSPlayer TSPlayer
    {
      get
      {
        return (TSPlayer) TShock.Players[this.Index];
      }
    }

    public string ranklist { get; set; }

    public int moreCD { get; set; }

    public int slayCD { get; set; }

    public int fartCD { get; set; }

    public int tickleCD { get; set; }

    public int pokeCD { get; set; }

    public int spokeCD { get; set; }

    public int hugCD { get; set; }

    public int lickCD { get; set; }

    public int facepalmCD { get; set; }

    public int kissCD { get; set; }

    public int babyCD { get; set; }

    public int stabCD { get; set; }

    public int loveCD { get; set; }

    public int faceplantCD { get; set; }

    public int slapallCD { get; set; }

    public int giftCD { get; set; }

    public int disturbCD { get; set; }

    public int bankCD { get; set; }

    public FACPlayer(int index)
    {
      this.Index = index;
    }
  }
}
