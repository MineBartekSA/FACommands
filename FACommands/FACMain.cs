using System;
using TShockAPI;
using TerrariaApi.Server;
using Terraria;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using TShockAPI.DB;
using System.Linq;
using System.Collections.Generic;

namespace FACommands
{
    [ApiVersion(2, 1)]
    public class FACommands : TerrariaPlugin
    {
        public DateTime LastCheck;
        private Config config;
        private Dictionary<TSPlayer, FACPlayer> PlayerList = new Dictionary<TSPlayer, FACPlayer>();

        public override string Name
        {
            get { return "FACommands"; }
        }
        public override Version Version
        {
            get { return new Version(1, 5, 0); }
        }
        public override string Author
        {
            get { return "MineBartekSA & Hiarni & Zaicon"; }
        }
        public override string Description
        {
            get { return "FACommands"; }
        }
        public FACommands(Main game) : base(game)
        {
            //Nothing!
        }
        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            ServerApi.Hooks.GameUpdate.Register(this, Cooldowns);
        }
        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                ServerApi.Hooks.GameUpdate.Deregister(this, Cooldowns);
            }
            base.Dispose(Disposing);
        }

        public void OnJoin(JoinEventArgs args)
        {
            if(!PlayerList.ContainsKey(TShock.Players[args.Who]))
                PlayerList.Add(TShock.Players[args.Who], new FACPlayer(TShock.Players[args.Who]));
        }

        public void OnLeave(LeaveEventArgs args)
        {
            try
            {
                if (!PlayerList.ContainsKey(TShock.Players[args.Who]))
                    PlayerList.Remove(TShock.Players[args.Who]);
            }
            catch { }
        }

        private void Cooldowns(EventArgs args)
        {
            if ((DateTime.UtcNow - LastCheck).TotalSeconds < 1.0)
                return;
            LastCheck = DateTime.UtcNow;
            foreach (FACPlayer facPlayer1 in PlayerList.Values)
            {
                if (facPlayer1 != null && facPlayer1.TSPlayer != null)
                {
                    if (facPlayer1.moreCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int moreCd = facPlayer2.moreCD;
                        facPlayer2.moreCD = moreCd - 1;
                    }
                    if (facPlayer1.slayCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int slayCd = facPlayer2.slayCD;
                        facPlayer2.slayCD = slayCd - 1;
                    }
                    if (facPlayer1.fartCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int fartCd = facPlayer2.fartCD;
                        facPlayer2.fartCD = fartCd - 1;
                    }
                    if (facPlayer1.tickleCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int tickleCd = facPlayer2.tickleCD;
                        facPlayer2.tickleCD = tickleCd - 1;
                    }
                    if (facPlayer1.pokeCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int pokeCd = facPlayer2.pokeCD;
                        facPlayer2.pokeCD = pokeCd - 1;
                    }
                    if (facPlayer1.spokeCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int spokeCd = facPlayer2.spokeCD;
                        facPlayer2.spokeCD = spokeCd - 1;
                    }
                    if (facPlayer1.hugCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int hugCd = facPlayer2.hugCD;
                        facPlayer2.hugCD = hugCd - 1;
                    }
                    if (facPlayer1.lickCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int lickCd = facPlayer2.lickCD;
                        facPlayer2.lickCD = lickCd - 1;
                    }
                    if (facPlayer1.facepalmCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int facepalmCd = facPlayer2.facepalmCD;
                        facPlayer2.facepalmCD = facepalmCd - 1;
                    }
                    if (facPlayer1.kissCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int kissCd = facPlayer2.kissCD;
                        facPlayer2.kissCD = kissCd - 1;
                    }
                    if (facPlayer1.babyCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int babyCd = facPlayer2.babyCD;
                        facPlayer2.babyCD = babyCd - 1;
                    }
                    if (facPlayer1.stabCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int stabCd = facPlayer2.stabCD;
                        facPlayer2.stabCD = stabCd - 1;
                    }
                    if (facPlayer1.loveCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int loveCd = facPlayer2.loveCD;
                        facPlayer2.loveCD = loveCd - 1;
                    }
                    if (facPlayer1.faceplantCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int faceplantCd = facPlayer2.faceplantCD;
                        facPlayer2.faceplantCD = faceplantCd - 1;
                    }
                    if (facPlayer1.slapallCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int slapallCd = facPlayer2.slapallCD;
                        facPlayer2.slapallCD = slapallCd - 1;
                    }
                    if (facPlayer1.giftCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int giftCd = facPlayer2.giftCD;
                        facPlayer2.giftCD = giftCd - 1;
                    }
                    if (facPlayer1.disturbCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int disturbCd = facPlayer2.disturbCD;
                        facPlayer2.disturbCD = disturbCd - 1;
                    }
                    if (facPlayer1.bankCD > 0)
                    {
                        FACPlayer facPlayer2 = facPlayer1;
                        int bankCd = facPlayer2.bankCD;
                        facPlayer2.bankCD = bankCd - 1;
                    }
                }
            }
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("facommands.reload", Reload_Config, "facreload")
            {
                HelpText = "Reloads FACommands cooldown config file."
            });
            Commands.ChatCommands.Add(new Command("facommands.bank", FACBB, "bb")
            {
                HelpText = "Shows you your current bank balance."
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACHistory, "h")
            {
                HelpText = "Short command for /history"
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACClear, "ca")
            {
                HelpText = "Short command for clearing up items and projectiles."
            });
            Commands.ChatCommands.Add(new Command("facommands.ranklist", FACRanklist, "ranklist")
            {
                HelpText = "Shows you all available ranks listed from lowest to highest."
            });
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FACP1, "p1")
            {
                HelpText = "Short command for WorldEdit //point1"
            });
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FACP2, "p2")
            {
                HelpText = "Short command for WorldEdit //point2"
            });
            Commands.ChatCommands.Add(new Command("facommands.more", FACMore, "more")
            {
                HelpText = "Fill up all your items to max stack.."
            });
            Commands.ChatCommands.Add(new Command("facommands.npc", FACNPC, "ncpr")
            {
                HelpText = "Respawns all Town NPC's at your location."
            });
            Commands.ChatCommands.Add(new Command("facommands.obc", FACOBC, "obc")
            {
                HelpText = "Owner Broadcast."
            });
            Commands.ChatCommands.Add(new Command("facommands.slay", FACSlay, "slay")
            {
                HelpText = "Slay them DOWN! ALL!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACFart, "fart")
            {
                HelpText = "Woah! That fart will blow you away!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACTickle, "tickle")
            {
                HelpText = "Tickle them down!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPoke, "poke")
            {
                HelpText = "Give someone a lovely poke."
            });
            Commands.ChatCommands.Add(new Command("facommands.spoke", FACSPoke, "spoke")
            {
                HelpText = "You shouldn't do that..."
            });
            Commands.ChatCommands.Add(new Command("facommands.stab", FACStab, "stab")
            {
                HelpText = "Well, you should better run now!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACHug, "hug")
            {
                HelpText = "Awwwhhh how lovely!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACLick, "lick")
            {
                HelpText = "Ugh... are you serious?!"
            });
            Commands.ChatCommands.Add(new Command("facommands.disturb", FACDisturb, "disturb")
            {
                HelpText = "They will catch you with they dusty sticks!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPalm, "facepalm")
            {
                HelpText = "Perform a facepalm."
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACPlant, "faceplant")
            {
                HelpText = "Are you crazy?!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACLove, "love")
            {
                HelpText = "hum... this must be true love..."
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACBaby, "baby")
            {
                HelpText = "uhh wat?! woah wait! Dude! You will pay forever!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACKiss, "kiss")
            {
                HelpText = "RAAWWWRRRR! What next?!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACSlap, "slapall")
            {
                HelpText = "Dusty sticks incomming!"
            });
            Commands.ChatCommands.Add(new Command("facommands.gift", FACGift, "gift")
            {
                HelpText = "If they were good!"
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACUI, "uinfo")
            {
                HelpText = "Lists detailed informations about players."
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FACBI, "binfo")
            {
                HelpText = "Lists detailed informations about banned players."
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FACDice, "diceroll", "dr")
            {
                HelpText = "Roll a Dice! As a parameter you can set rage!"
            });
            ReadConfig();
        }

        private void FACDice(CommandArgs args)
        {
            if(args.Parameters.Count != 0)
            {
                if(args.Parameters.Count == 2 || args.Parameters.Count > 2)
                {
                    int min, max;
                    try
                    {
                        min = Int32.Parse(args.Parameters[0]);
                        max = Int32.Parse(args.Parameters[1]);
                    }
                    catch(Exception exe)
                    {
                        TShock.Log.Error("Error while parsing!");
                        TShock.Log.Error(exe.ToString());
                        args.Player.SendErrorMessage("Only numbers! Nothing else!");
                        return;
                    }
                    if((max - min) < config.minRangeRollDice)
                    {
                        args.Player.SendErrorMessage("Range must me at least " + config.minRangeRollDice + " numbers!");
                        return;
                    }
                    TSPlayer.All.SendData(PacketTypes.CreateCombatText, "", (int)new Color(112, 218, 255).PackedValue, args.TPlayer.position.X, args.TPlayer.position.Y + 32, new Random().Next(min, max));
                    return;
                }
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /diceroll [min] [max]");
                return;
            }
            if(config.maxRollDice < 0)
            {
                args.Player.SendErrorMessage("Max value can't be smaller than 0!");
                args.Player.SendErrorMessage("If you want to get negativ valeue use: /dr [min] [max]");
                args.Player.SendErrorMessage("Setting max value to default");
                config.maxRollDice = 100;
            }
            TSPlayer.All.SendData(PacketTypes.CreateCombatText, "", (int)new Color(112, 218, 255).PackedValue, args.TPlayer.position.X, args.TPlayer.position.Y + 32, new Random().Next(0, config.maxRollDice));
        }
        private void FACBB(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.bankCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.bankCD);
            }
            else
            {
                Commands.HandleCommand(args.Player, "/bank bal");
                if (args.Player.Group.HasPermission("facommands.nocd"))
                    return;
                facPlayer.bankCD = config.bankCD;
            }
        }

        private void FACHistory(CommandArgs args)
        {
            Commands.HandleCommand(args.Player, "/history");
        }

        private void FACP1(CommandArgs args)
        {
            Commands.HandleCommand(args.Player, "//point1");
        }

        private void FACP2(CommandArgs args)
        {
            Commands.HandleCommand(args.Player, "//point2");
        }

        private void FACClear(CommandArgs args)
        {
            Commands.HandleCommand(args.Player, "/clear item 100000");
            Commands.HandleCommand(args.Player, "/clear projectile 100000");
        }

        private void FACRanklist(CommandArgs args)
        {
            args.Player.SendInfoMessage(string.Format("Available ranks listed from lowest to highest: {0}", config.ranklist));
        }

        private void FACMore(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.moreCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.moreCD);
            }
            else
            {
                if (args.Parameters.Count > 0 && args.Parameters[0].ToLower() == "all")
                {
                    bool flag = true;
                    int num1 = 0;
                    foreach (Item obj in args.TPlayer.inventory)
                    {
                        if (obj != null && obj.stack != 0)
                        {
                            int num2 = (obj.maxStack - obj.stack);
                            if (obj.stack > 0 && num2 > 0 && !obj.Name.ToLower().Contains("coin"))
                            {
                                flag = false;
                                args.Player.GiveItem(obj.type, obj.Name, obj.width, obj.height, num2, 0);
                            }
                            ++num1;
                        }
                    }
                    if (!flag)
                        args.Player.SendSuccessMessage("Filled up all your items.");
                    else
                        args.Player.SendErrorMessage("Your inventory is already filled up.");
                }
                else
                {
                    Item obj = args.Player.TPlayer.inventory[args.TPlayer.selectedItem];
                    int num = (obj.maxStack - obj.stack);
                    if (obj.stack > 0 && num > 0)
                        args.Player.GiveItem(obj.type, obj.Name, obj.width, obj.height, num, 0);
                    if (num == 0)
                        args.Player.SendErrorMessage("Your {0} is already full.", obj.Name);
                    else
                        args.Player.SendSuccessMessage("Filled up your {0}.", obj.Name);
                }
                if (!args.Player.Group.HasPermission("facommands.nocd"))
                    facPlayer.moreCD = config.moreCD;
            }
        }

        private void FACNPC(CommandArgs args)
        {
            int num = 0;
            for (int index = 0; index < Main.npc.Length; ++index)
            {
                if (Main.npc[index].townNPC)
                {
                    TSPlayer.Server.StrikeNPC(index, 99999, 0.0f, 0);
                    ++num;
                }
            }
            TSPlayer.All.SendInfoMessage(string.Format("{0} killed {1} friendly NPCs and spawned all town NPCs.", args.Player.Name, num));
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(19).type, TShock.Utils.GetNPCById(19).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(54).type, TShock.Utils.GetNPCById(54).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(209).type, TShock.Utils.GetNPCById(209).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(38).type, TShock.Utils.GetNPCById(38).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(20).type, TShock.Utils.GetNPCById(20).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(207).type, TShock.Utils.GetNPCById(207).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(107).type, TShock.Utils.GetNPCById(107).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(22).type, TShock.Utils.GetNPCById(22).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(124).type, TShock.Utils.GetNPCById(124).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(17).type, TShock.Utils.GetNPCById(17).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(18).type, TShock.Utils.GetNPCById(18).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(227).type, TShock.Utils.GetNPCById(227).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(208).type, TShock.Utils.GetNPCById(208).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(229).type, TShock.Utils.GetNPCById(229).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(178).type, TShock.Utils.GetNPCById(178).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(353).type, TShock.Utils.GetNPCById(353).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(368).type, TShock.Utils.GetNPCById(368).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(160).type, TShock.Utils.GetNPCById(160).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(228).type, TShock.Utils.GetNPCById(228).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(108).type, TShock.Utils.GetNPCById(108).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(369).type, TShock.Utils.GetNPCById(369).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(441).type, TShock.Utils.GetNPCById(441).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            TSPlayer.Server.SpawnNPC(TShock.Utils.GetNPCById(550).type, TShock.Utils.GetNPCById(550).FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
        }

        private void FACOBC(CommandArgs args)
        {
            string str = string.Join(" ", args.Parameters);
            TShock.Utils.Broadcast("(Owner Broadcast) " + str, Color.Cyan);
        }

        private void FACSlay(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.slayCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.slayCD);
            else if (args.Parameters.Count < 2)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /slay <player> <reason>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                    args.Player.SendErrorMessage("Invalid player!");
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    NetMessage.SendData(26, -1, -1, new Terraria.Localization.NetworkText(" " + string.Join(" ", args.Parameters.Skip(1)), Terraria.Localization.NetworkText.Mode.Literal), tsPlayer.Index, 0.0f, 15000f, 0.0f, 0, 0, 0);
                    args.Player.SendSuccessMessage("You just slayed {0}.", new object[1]
                    {
                        tsPlayer.Name
                    });
                }
                if (!args.Player.Group.HasPermission("facommands.nocd"))
                    facPlayer.slayCD = this.config.slayCD;
            }
        }

        private void FACFart(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.fartCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.fartCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /fart <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                    args.Player.SendErrorMessage("No players matched!");
                else if (player.Count > 1)
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                else if (player[0].Group.Name == "admin" | player[0].Group.Name == "owner" | player[0].Group.Name == "owner")
                {
                    args.Player.SendErrorMessage("You cannot fart this player!");
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.SetBuff(163, 600, true);
                    tsPlayer.SetBuff(120, 600, true);
                    args.Player.SendInfoMessage("Woah! That fart dude! You shouldn't eat so much shit!", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} turned around and farted {1} in the face! Woah! That fart mades you blind dude! aahh it stinks so much! Run away! o.O", args.Player.Name, tsPlayer.Name), Color.Sienna);
                    TShock.Log.Info("{0} farted {1} in the face!", args.Player.Name, tsPlayer.Name );
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.fartCD = this.config.fartCD;
                }
            }
        }

        private void FACTickle(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.tickleCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", new object[1]
                {
           facPlayer.tickleCD
                });
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /tickle <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, player.Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.SetBuff(2, 3600, true);
                    args.Player.SendInfoMessage("You shouldn't tickle people to much... >.>", tsPlayer.Name);
                    (TSPlayer.All).SendMessage(string.Format("{0} tickles {1}! Isn't that sweet? :P", args.Player.Name, tsPlayer.Name), Color.Thistle);
                    (TShock.Log).Info("{0} tickles {1}!", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.tickleCD = this.config.tickleCD;
                }
            }
        }

        private void FACPoke(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.pokeCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.pokeCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /poke <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                    args.Player.SendErrorMessage("Invalid player!");
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.DamagePlayer(1);
                    args.Player.SendInfoMessage("You poked {0}. Uh oh... should you run now?", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} poked {1}. Well, that was lovely! :3", args.Player.Name, tsPlayer.Name), Color.LightSkyBlue);
                    TShock.Log.Info("{0} poked {1}.", new object[2] { args.Player.Name, tsPlayer.Name });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.pokeCD = this.config.pokeCD;
                }
            }
        }

        private void FACSPoke(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.spokeCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.spokeCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /spoke <player>");
            else if (args.Parameters[0] == "")
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                    args.Player.SendErrorMessage("Invalid player!");
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, player.Select((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.KillPlayer();
                    args.Player.SendInfoMessage("You poked {0}. BOOM! BANG! PAW!", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} poked {1} in the tummy. KADUSH! Who is the next one?!", args.Player.Name, tsPlayer.Name), Color.MediumTurquoise);
                    TShock.Log.Info("{0} super-poked {1}.", new object[2] { args.Player.Name, tsPlayer.Name });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.spokeCD = config.spokeCD;
                }
            }
        }

        private void FACHug(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.hugCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.hugCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /hug <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                {
                    args.Player.SendInfoMessage("You hugged your invisible friend {0}! Common! Get a life bro...", parameter);
                    (TSPlayer.All).SendMessage(string.Format("{0} hugged " + (args.Player.TPlayer.Male ? "his" : "her") + " invisible friend {1}! Common! Really? Get a life bro...", args.Player.Name, parameter), Color.Chartreuse);
                }
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    args.Player.SendInfoMessage("You hugged {0}! You need love huh?", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} hugged {1}! Awwwhhh... how sweet? <3", args.Player.Name, tsPlayer.Name), Color.Chartreuse);
                    TShock.Log.Info("{0} hugged {1}! Awwwhhh... how sweet? <3", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.hugCD = config.hugCD;
                }
            }
        }

        private void FACLick(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.lickCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.lickCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /lick <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                {
                    if (args.Parameters[0].ToLower() != "air")
                    {
                        args.Player.SendInfoMessage("You licked the air! Tasted like air... hum... {0} was not found...", parameter);
                        (TSPlayer.All).SendMessage(string.Format("{0} licked the air! Tasted like air... hum...", args.Player.Name), Color.DarkOrchid);
                    }
                    else
                    {
                        args.Player.SendInfoMessage("You licked the air! Tasted like air... hum...", parameter);
                        (TSPlayer.All).SendMessage(string.Format("{0} licked the air! Tasted like air... hum...", args.Player.Name), Color.DarkOrchid);
                    }
                }
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    args.Player.SendInfoMessage("You licked {0}! Really...?", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} licked {1}! Ugh... X.X", args.Player.Name, tsPlayer.Name), Color.DarkOrchid);
                    TShock.Log.Info("{0} licked {1}!", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.lickCD = config.lickCD;
                }
            }
        }

        private void FACPalm(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.facepalmCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", new object[1]
                {
           facPlayer.facepalmCD
                });
            }
            else
            {
                args.Player.SendInfoMessage("Well... why not?");
                TSPlayer.All.SendMessage(string.Format("{0} facepalmed.", args.Player.Name), Color.Chocolate);
                TShock.Log.Info("{0} facepalmed.", args.Player.Name);
                if (!args.Player.Group.HasPermission("facommands.nocd"))
                    facPlayer.facepalmCD = config.facepalmCD;
            }
        }

        private void FACKiss(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.kissCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.kissCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /kiss <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                {
                    if (args.Parameters[0].ToLower() != "air")
                    {
                        args.Player.SendInfoMessage("You kissed the air! {0} was not found... get a life bro...", parameter);
                        TSPlayer.All.SendMessage(string.Format("{0} kissed the air! The hell...?!", args.Player.Name), Color.Coral);
                    }
                    else
                    {
                        args.Player.SendInfoMessage("You kissed the air! WTF?!", parameter);
                        TSPlayer.All.SendMessage(string.Format("{0} kissed the air! The hell...?!", args.Player.Name), Color.Coral);
                    }
                }
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    args.Player.SendInfoMessage("You kissed {0}! RAAWWRRR! Are you horny?", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} kisses {1}! Love is everywhere! <3", args.Player.Name, tsPlayer.Name), Color.Coral);
                    TShock.Log.Info("{0} kisses {1}!", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.kissCD = config.kissCD;
                }
            }
        }

        private void FACBaby(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.babyCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.babyCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /baby <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count > 1)
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                else if (player[0].Group.Name == "admin" | player[0].Group.Name == "owner" | player[0].Group.Name == "owner")
                {
                    args.Player.SendErrorMessage("You can't make a baby with this player!");
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.SetBuff(92, 3600, false);
                    tsPlayer.SetBuff(103, 3600, true);
                    args.Player.SendInfoMessage("... does it made you happy? I hope it's your true love... otherwise much fun with the alimony! :D", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} tried to make a baby (grinch) with {1}! awwhhh look! It's soooo cute! <3", args.Player.Name, tsPlayer.Name), Color.Firebrick);
                    TShock.Log.Info("{0} tried to make a baby (grinch) with {1}!", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.babyCD = config.babyCD;
                }
            }
        }

        private void FACStab(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            Random r = new Random();
            int rand = r.Next(0, 100);
            if (facPlayer.stabCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.stabCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /stab <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                {
                    args.Player.SendInfoMessage("You stabbed your invisible friend {0}! You should train harder!", parameter);
                    TSPlayer.All.SendMessage(string.Format("{0} stabbed " + (args.Player.TPlayer.Male ? "his" : "her") + " invisible friend {1}! He should train harder...", args.Player.Name, parameter), Color.AliceBlue);
                }
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.DamagePlayer(9001);
                    args.Player.SendInfoMessage("You stabbed {0} for OVER 9000 damage! That was close!", tsPlayer.Name);
                    if (rand > 75)
                    {
                        TSPlayer.All.SendMessage(string.Format("{0} stabbed {1} mercilessly! GO CATCH HIM! O.O", args.Player.Name, tsPlayer.Name), Color.AliceBlue);
                    }
                    else
                    {
                        TSPlayer.All.SendMessage(string.Format("{0} got stabbed mercilessly! But we can't find the stabber! O.O", tsPlayer.Name), Color.AliceBlue);
                    }
                    TShock.Log.Info("{0} stabbed {1}.", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.stabCD = config.stabCD;
                }
            }
        }

        private void FACLove(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.loveCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", new object[1]
                {
           facPlayer.loveCD
                });
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /love <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                {
                    if (args.Parameters[0].ToLower() != "air")
                    {
                        args.Player.SendInfoMessage("You love the air! {0} was not found... GO! Search your true love!", parameter);
                        TSPlayer.All.SendMessage(string.Format("{0} loves the air! You should get some friends... :O", args.Player.Name), Color.Pink);
                    }
                    else
                    {
                        args.Player.SendInfoMessage("You love the air! Really?", parameter);
                        TSPlayer.All.SendMessage(string.Format("{0} loves the air! Well... >.>", args.Player.Name), Color.Pink);
                    }
                }
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    args.Player.SendInfoMessage("You love {0}! hum... next step? ;)", tsPlayer.Name);
                    TSPlayer.All.SendMessage(string.Format("{0} loves {1}! Oehlalah... ready for the next step? <3", args.Player.Name, tsPlayer.Name), Color.Pink);
                    TShock.Log.Info("{0} loves {1}!", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.loveCD = config.loveCD;
                }
            }
        }

        private void FACPlant(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.faceplantCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.faceplantCD);
            }
            else
            {
                if (!args.Player.RealPlayer)
                    args.Player.SendInfoMessage("You planted your face on the ground. Serious man...?");
                else
                    args.Player.KillPlayer();
                TSPlayer.All.SendMessage(string.Format("{0} planted " + (args.Player.TPlayer.Male ? "his" : "her") + " face on the ground. Are you crazy?!", args.Player.Name), Color.BlanchedAlmond);
                if (args.Player.Group.HasPermission("facommands.nocd"))
                    return;
                facPlayer.faceplantCD = config.faceplantCD;
            }
        }

        private void FACSlap(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.slapallCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.slapallCD);
            }
            else
            {
                if (!args.Player.RealPlayer)
                    args.Player.SendInfoMessage("You slapped everyone! That stings!");
                TSPlayer.All.SendMessage(string.Format("{0} slapped you (along with everyone else)! THE HELL! CATCH HIM! :O", args.Player.Name), Color.BlanchedAlmond);
                TSPlayer.All.DamagePlayer(15);
                if (args.Player.Group.HasPermission("facommands.nocd"))
                    return;
                facPlayer.slapallCD = config.slapallCD;
            }
        }

        private void FACGift(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.giftCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.giftCD);
            else if (args.Parameters.Count != 1)
            {
                args.Player.SendErrorMessage("Invalid syntax: /gift <player>");
            }
            else
            {
                List<TSPlayer> player = TShock.Utils.FindPlayer(args.Parameters[0]);
                if (player.Count == 0)
                    args.Player.SendErrorMessage("No players found by that name.");
                else if (player.Count > 1)
                {
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    if (new Random().Next(2) == 0)
                    {
                        Item itemById = TShock.Utils.GetItemById(1922);
                        tsPlayer.GiveItem(itemById.type, (itemById).Name, (itemById).width, (itemById).height, itemById.maxStack, 0);
                        tsPlayer.SendInfoMessage("{0} gave you Coal! You were a naughty {1}...", new object[2]
                        {
               args.Player.Name,
               (tsPlayer.TPlayer.Male ? "boy" : "girl")
                        });
                        args.Player.SendSuccessMessage("You gave {0} Coal! {0} was a naughty {1}...", new object[2]
                        {
               tsPlayer.Name,
               (tsPlayer.TPlayer.Male ? "boy" : "girl")
                        });
                    }
                    else
                    {
                        Item itemById = TShock.Utils.GetItemById(1869);
                        tsPlayer.GiveItem(itemById.type, (itemById).Name, (itemById).width, (itemById).height, itemById.stack, 1);
                        tsPlayer.SendInfoMessage("{0} gave you a Present! You were a good {1}...", new object[2]
                        {
               args.Player.Name,
               (tsPlayer.TPlayer.Male ? "boy" : "girl")
                        });
                        args.Player.SendSuccessMessage("You gave {0} a Present! {0} was a good {1}...", new object[2]
                        {
               tsPlayer.Name,
               (tsPlayer.TPlayer.Male ? "boy" : "girl")
                        });
                        if (!args.Player.Group.HasPermission("facommands.nocd"))
                            facPlayer.giftCD = config.giftCD;
                    }
                }
            }
        }

        private void FACDisturb(CommandArgs args)
        {
            FACPlayer facPlayer = PlayerList[args.Player];
            if (facPlayer.disturbCD != 0 && !args.Player.Group.HasPermission("facommands.nocd"))
                args.Player.SendErrorMessage("This command is on cooldown for {0} seconds.", facPlayer.disturbCD);
            else if (args.Parameters.Count != 1)
                args.Player.SendErrorMessage("Invalid syntax! Proper syntax: /disturb <player>");
            else if (args.Parameters[0].Length == 0)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }
            else
            {
                string parameter = args.Parameters[0];
                List<TSPlayer> player = TShock.Utils.FindPlayer(parameter);
                if (player.Count == 0)
                    args.Player.SendErrorMessage("No players matched!");
                else if (player.Count > 1)
                    TShock.Utils.SendMultipleMatchError(args.Player, ((IEnumerable<TSPlayer>)player).Select<TSPlayer, string>((p => p.Name)));
                else if (player[0].Group.Name == "admin" | player[0].Group.Name == "owner" | player[0].Group.Name == "superadmin")
                {
                    args.Player.SendErrorMessage("You cannot disturb this player!");
                }
                else
                {
                    TSPlayer tsPlayer = player[0];
                    tsPlayer.SetBuff(26, 900, true);
                    tsPlayer.SetBuff(30, 900, true);
                    tsPlayer.SetBuff(31, 900, true);
                    tsPlayer.SetBuff(32, 900, true);
                    tsPlayer.SetBuff(103, 900, true);
                    tsPlayer.SetBuff(115, 900, true);
                    tsPlayer.SetBuff(120, 900, true);
                    args.Player.SendInfoMessage("You disturbed {0}! You feel slightly better now...", tsPlayer.Name);
                    (TSPlayer.All).SendMessage(string.Format("{0} disturbed {1}! Is he angry now? huh?", args.Player.Name, tsPlayer.Name), Color.Crimson);
                    (TShock.Log).Info("{0} disturbed {1}.", new object[2]
                    {
             args.Player.Name,
             tsPlayer.Name
                    });
                    if (!args.Player.Group.HasPermission("facommands.nocd"))
                        facPlayer.disturbCD = config.disturbCD;
                }
            }
        }

        private void FACUI(CommandArgs args)
        {
            if (args.Parameters.Count == 1)
            {
                string str1 = string.Join(" ", args.Parameters);
                if (str1 != null & str1 != "")
                {
                    User userByName = (TShock.Users).GetUserByName(str1);
                    if (userByName != null)
                    {
                        args.Player.SendMessage(string.Format("User {0} exists.", str1), Color.DeepPink);
                        try
                        {
                            DateTime dateTime1 = DateTime.Parse(userByName.Registered);
                            DateTime dateTime2 = DateTime.Parse(userByName.LastAccessed);
                            List<string> stringList = JsonConvert.DeserializeObject<List<string>>(userByName.KnownIps);
                            string str2 = stringList[stringList.Count - 1];
                            args.Player.SendMessage(string.Format("{0}'s group is {1}.", str1, userByName.Group), Color.DeepPink);
                            args.Player.SendMessage(string.Format("{0}'s last known IP is {1}.", str1, str2), Color.DeepPink);
                            args.Player.SendMessage(string.Format("{0}'s register date is {1}.", str1, dateTime1.ToShortDateString()), Color.DeepPink);
                            args.Player.SendMessage(string.Format("{0} was last seen {1}.", str1, dateTime2.ToShortDateString(), dateTime2.ToShortTimeString()), Color.DeepPink);
                        }
                        catch
                        {
                            DateTime dateTime = DateTime.Parse(userByName.Registered);
                            args.Player.SendMessage(string.Format("{0}'s group is {1}.", str1, userByName.Group), Color.DeepPink);
                            args.Player.SendMessage(string.Format("{0}'s register date is {1}.", str1, dateTime.ToShortDateString()), Color.DeepPink);
                        }
                    }
                    else
                        args.Player.SendMessage(string.Format("User {0} does not exist.", str1), Color.DeepPink);
                }
                else
                    args.Player.SendErrorMessage("Syntax: /uinfo <player name>.");
            }
            else
                args.Player.SendErrorMessage("Syntax: /uinfo <player name>");
        }

        private void FACBI(CommandArgs args)
        {
            if (args.Parameters.Count != 1)
            {
                args.Player.SendErrorMessage("Invalid syntax: /baninfo \"Player Name\"");
            }
            else
            {
                string parameter = args.Parameters[0];
                Ban banByName = (TShock.Bans).GetBanByName(parameter, true);
                if (banByName == null)
                {
                    args.Player.SendErrorMessage("No bans by this name were found.");
                }
                else
                {
                    args.Player.SendInfoMessage("Account name: " + banByName.Name + " (" + banByName.IP + ")");
                    args.Player.SendInfoMessage("Date banned: " + banByName.Date);
                    if (banByName.Expiration != "")
                        args.Player.SendInfoMessage("Expiration date: " + banByName.Expiration);
                    args.Player.SendInfoMessage("Banning user: " + banByName.BanningUser);
                    args.Player.SendInfoMessage("Reason: " + banByName.Reason);
                }
            }
        }

        private void CreateConfig()
        {
            string path = Path.Combine(TShock.SavePath, "FACommands.json");
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        config = new FACommands.Config();
                        string str = JsonConvert.SerializeObject(config, (Formatting)1);
                        streamWriter.Write(str);
                    }
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError(ex.Message);
            }
        }

        private bool ReadConfig()
        {
            string path = Path.Combine(TShock.SavePath, "FACommands.json");
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                            config = JsonConvert.DeserializeObject<FACommands.Config>(streamReader.ReadToEnd());
                        fileStream.Close();
                    }
                    return true;
                }
                TShock.Log.ConsoleError("FACommands config not found! Creating new one...");
                CreateConfig();
                return false;
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError(ex.Message);
            }
            return false;
        }

        private void Reload_Config(CommandArgs args)
        {
            if (ReadConfig())
                args.Player.SendMessage("FACommands config reloaded sucessfully.", Color.Yellow);
            else
                args.Player.SendErrorMessage("FACommands config reload failed! Check logs for details!");
        }

        public class Config
        {
            public string[] ranklist = new string[]
            {
        "Traveller, Citizen, Soldier, Fighter, Warrior, Champion, Gladiator, Commander, Warmaster, Hero, Executor, Lord, Legend, Demigod, Immortal, Unattainable, Keeper of Gods"
            };
            public int moreCD = 120;
            public int slayCD = 60;
            public int fartCD = 60;
            public int tickleCD = 60;
            public int pokeCD = 60;
            public int spokeCD = 120;
            public int hugCD = 30;
            public int lickCD = 30;
            public int facepalmCD = 30;
            public int kissCD = 30;
            public int babyCD = 300;
            public int stabCD = 120;
            public int loveCD = 30;
            public int faceplantCD = 120;
            public int slapallCD = 120;
            public int giftCD = 300;
            public int disturbCD = 120;
            public int bankCD = 10;
            public int maxRollDice = 100;
            public int minRangeRollDice = 10;
        }
    }
}
