using System;
using TShockAPI;
using TerrariaApi.Server;
using Terraria;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Collections.Generic;
using Terraria.DataStructures;
using TShockAPI.Hooks;

namespace FACommands
{
    [ApiVersion(2, 1)]
    // ReSharper disable once InconsistentNaming
    public class FACommands : TerrariaPlugin
    {
        private readonly Dictionary<string, Player> _playerList = new Dictionary<string, Player>();
        private Config _config;
        private Random _random;

        public override string Name => "FACommands";
        public override Version Version => new Version(1, 7, 1);
        public override string Author => "MineBartekSA & Hiarni & Zaicon";
        public override string Description => "Fun and Admin Commands";

        public FACommands(Main game) : base(game) { }

        public override void Initialize()
        {
            _config = Config.Create();
            _random = new Random(DateTime.Now.Millisecond);
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            GeneralHooks.ReloadEvent += OnReload;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                GeneralHooks.ReloadEvent -= OnReload;
            }
            base.Dispose(disposing);
        }

        private void OnJoin(JoinEventArgs args)
        {
            if (!_playerList.ContainsKey(TShock.Players[args.Who].UUID))
                _playerList.Add(TShock.Players[args.Who].UUID, new Player());
        }

        private void OnLeave(LeaveEventArgs args)
        {
            if (args.Who == TShock.Players.Count(p => p != null)) return;
            if (!_playerList.ContainsKey(TShock.Players[args.Who].UUID))
                _playerList.Remove(TShock.Players[args.Who].UUID);
        }

        private void OnReload(ReloadEventArgs args)
        {
            try
            {
                var newConfig = Config.Read(true);
                if (newConfig != null)
                    _config = newConfig;
            }
            catch
            {
                args.Player.SendErrorMessage("[FACommands] Failed to reload config");
            }
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("facommands.reload", Reload, "facreload")
            {
                HelpText = "Reloads FACommands cooldown config file."
            });
            Commands.ChatCommands.Add(new Command("facommands.bank", FacBb, "bb")
            {
                HelpText = "Shows you your current bank balance."
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FacHistory, "h")
            {
                HelpText = "Short command for /history"
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FacClear, "ca")
            {
                HelpText = "Short command for clearing up items and projectiles."
            });
            Commands.ChatCommands.Add(new Command("facommands.ranklist", FacRanklist, "ranklist")
            {
                HelpText = "Shows you all available ranks listed from lowest to highest."
            });
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FacP1, "p1")
            {
                HelpText = "Short command for WorldEdit //point1"
            });
            Commands.ChatCommands.Add(new Command("worldedit.selection.point", FacP2, "p2")
            {
                HelpText = "Short command for WorldEdit //point2"
            });
            Commands.ChatCommands.Add(new Command("facommands.more", FacMore, "more")
            {
                HelpText = "Fill up all your items to max stack.."
            });
            Commands.ChatCommands.Add(new Command("facommands.npc", FacNpc, "npcr")
            {
                HelpText = "Respawns all Town NPC's at your location."
            });
            Commands.ChatCommands.Add(new Command("facommands.obc", FacOwnerBroadcast, "obc")
            {
                HelpText = "Owner Broadcast."
            });
            Commands.ChatCommands.Add(new Command("facommands.slay", FacSlay, "slay")
            {
                HelpText = "Slay them DOWN! ALL!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacFart, "fart")
            {
                HelpText = "Woah! That fart will blow you away!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacTickle, "tickle")
            {
                HelpText = "Tickle them down!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacPoke, "poke")
            {
                HelpText = "Give someone a lovely poke."
            });
            Commands.ChatCommands.Add(new Command("facommands.spoke", FacSPoke, "spoke")
            {
                HelpText = "You shouldn't do that..."
            });
            Commands.ChatCommands.Add(new Command("facommands.stab", FacStab, "stab")
            {
                HelpText = "Well, you should better run now!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacHug, "hug")
            {
                HelpText = "Awwwhhh how lovely!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacLick, "lick")
            {
                HelpText = "Ugh... are you serious?!"
            });
            Commands.ChatCommands.Add(new Command("facommands.disturb", FacDisturb, "disturb")
            {
                HelpText = "They will catch you with they dusty sticks!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacPalm, "facepalm")
            {
                HelpText = "Perform a facepalm."
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacPlant, "faceplant")
            {
                HelpText = "Are you crazy?!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacLove, "love")
            {
                HelpText = "hum... this must be true love..."
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacBaby, "baby")
            {
                HelpText = "uhh wat?! woah wait! Dude! You will pay forever!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacKiss, "kiss")
            {
                HelpText = "RAAWWWRRRR! What next?!"
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacSlap, "slapall")
            {
                HelpText = "Dusty sticks incoming!"
            });
            Commands.ChatCommands.Add(new Command("facommands.gift", FacGift, "gift")
            {
                HelpText = "If they were good!"
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FacUI, "uinfo")
            {
                HelpText = "Lists detailed information about players."
            });
            Commands.ChatCommands.Add(new Command("facommands.staff", FacBI, "binfo")
            {
                HelpText = "Lists detailed information about banned players."
            });
            Commands.ChatCommands.Add(new Command("facommands.fun", FacDice, "diceroll", "dr")
            {
                HelpText = "Roll a Dice! As a parameter you can set rage!"
            });
        }

        private void FacDice(CommandArgs args)
        {
            int min = _config.MinRollDice, max = _config.MaxRollDice;
            if (args.Parameters.Count != 0)
            {
                if (args.Parameters.Count >= 1)
                    if (!int.TryParse(args.Parameters[0], out max))
                    {
                        args.Player.SendErrorMessage($"Invalid maximum value! Using {_config.MaxRollDice}");
                        max = _config.MaxRollDice;
                    }
                if (args.Parameters.Count >= 2)
                    if (!int.TryParse(args.Parameters[1], out min))
                    {
                        args.Player.SendErrorMessage($"Invalid minimal value! Using {_config.MinRollDice}");
                        min = _config.MinRollDice;
                    }
            }
            if (max < min)
            {
                args.Player.SendErrorMessage("The maximum value must be grater than the minimal!");
                return;
            }

            var roll = _random.Next(min, max + 1);
            args.Player.SendInfoMessage($"You have rolled: {roll}");
            TSPlayer.All.SendData(PacketTypes.CreateCombatText, "", (int)new Color(112, 218, 255).PackedValue, args.TPlayer.position.X, args.TPlayer.position.Y + 32, roll);
        }
        private void FacBb(CommandArgs args)
        {
            var play = _playerList[args.Player.UUID];
            if (play.CheckCooldown("bb", _config.BankCooldown, out var left) && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage($"This command is on cooldown for {left} seconds.");
                return;
            }
            Commands.HandleCommand(args.Player, "/bank bal");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                play.SetCooldown("bb", _config.BankCooldown);
        }

        private void FacHistory(CommandArgs args) => Commands.HandleCommand(args.Player, "/history");

        private void FacP1(CommandArgs args) => Commands.HandleCommand(args.Player, "//point1");

        private void FacP2(CommandArgs args) => Commands.HandleCommand(args.Player, "//point2");

        private void FacClear(CommandArgs args)
        {
            Commands.HandleCommand(args.Player, "/clear item 100000");
            Commands.HandleCommand(args.Player, "/clear projectile 100000");
        }

        private void FacRanklist(CommandArgs args) => args.Player.SendInfoMessage($"Available ranks listed from lowest to highest: {string.Join(" ", _config.RankList)}");

        private void FacMore(CommandArgs args)
        {
            var play = _playerList[args.Player.UUID];
            if (play.CheckCooldown("more", _config.MoreCooldown, out var left) && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage($"This command is on cooldown for {left} seconds.");
                return;
            }
            if (args.Parameters.Count > 0 && args.Parameters[0].ToLower() == "all")
            {
                var flag = true;
                foreach (var item in args.TPlayer.inventory)
                {
                    if (item == null || item.stack == 0) continue;
                    var leftToMaxStack = (item.maxStack - item.stack);
                    if (item.stack <= 0 || leftToMaxStack <= 0 || item.Name.ToLower().Contains("coin")) continue;
                    flag = false;
                    args.Player.GiveItem(item.type, leftToMaxStack, item.prefix);
                }
                if (!flag)
                    args.Player.SendSuccessMessage("Filled up all your items.");
                else
                    args.Player.SendErrorMessage("Your inventory is already filled up.");
            }
            else
            {
                var item = args.Player.TPlayer.inventory[args.TPlayer.selectedItem];
                var leftToMaxStack = (item.maxStack - item.stack);
                if (item.stack > 0 && leftToMaxStack > 0)
                    args.Player.GiveItem(item.type, leftToMaxStack, item.prefix);
                if (leftToMaxStack == 0)
                    args.Player.SendErrorMessage($"Your {item.Name} is already full.");
                else
                    args.Player.SendSuccessMessage($"Filled up your {item.Name}.");
            }
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                play.SetCooldown("more", _config.MoreCooldown);
        }

        private void FacNpc(CommandArgs args)
        {
            var killedAmount = 0;
            for (var i = 0; i < Main.npc.Length; ++i)
            {
                if (!Main.npc[i].townNPC) continue;
                TSPlayer.Server.StrikeNPC(i, 99999, 0.0f, 0);
                ++killedAmount;
            }
            TSPlayer.All.SendInfoMessage($"{args.Player.Name} killed {killedAmount} friendly NPCs and spawned all town NPCs.");

            foreach (var id in _config.NpcList)
            {
                var npc = TShock.Utils.GetNPCById(id);
                TSPlayer.Server.SpawnNPC(npc.type, npc.FullName, 1, args.Player.TileX, args.Player.TileY, 20, 20);
            }
        }

        private void FacOwnerBroadcast(CommandArgs args) => TShock.Utils.Broadcast($"{_config.OwnerBroadcastPrefix} {string.Join(" ", args.Parameters)}", _config.OwnerBroadcastColor.ConvertToColor());

        private void FacSlay(CommandArgs args)
        {
            if (!FirstCheck(args, "slay", new []{"<player>", "<reason>"}, _config.SlayCooldown, c => c < 2))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            NetMessage.SendPlayerDeath(target.Index, PlayerDeathReason.ByCustomReason($"{target.Name} got slayed! {string.Join(" ", args.Parameters.Skip(1))}"), 9999, _random.Next(-1, 1), false);
            args.Player.SendSuccessMessage($"You just slayed {target.Name}");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("slay", _config.SlayCooldown);
        }

        private void FacFart(CommandArgs args)
        {
            if (!FirstCheck(args, "fart", new []{"<player>"}, _config.FartCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            if (target.Group.Name == "admin" | target.Group.Name == "owner" | target.Group.Name == "owner")
                args.Player.SendErrorMessage("You cannot fart on this player!");
            else
            {
                target.SetBuff(163, 600, true);
                target.SetBuff(120, 600, true);
                args.Player.SendInfoMessage("Woah! That fart dude! You shouldn't eat so much shit!");
                TSPlayer.All.SendMessage($"{args.Player.Name} turned around and farted {target.Name} in the face! Woah! That fart made him blind dude! aahh it stinks so much! Run away! o.O", Color.Sienna);
                TShock.Log.Info($"{args.Player.Name} farted {target.Name} in the face!");
                if (!args.Player.Group.HasPermission("facommands.nocd"))
                    _playerList[args.Player.UUID].SetCooldown("fart", _config.FartCooldown);
            }
        }

        private void FacTickle(CommandArgs args)
        {
            if (!FirstCheck(args, "tickle",new []{"<player>"}, _config.TickleCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            target.SetBuff(2, 3600, true);
            args.Player.SendInfoMessage("You shouldn't tickle people to much... >.>");
            TSPlayer.All.SendMessage($"{args.Player.Name} tickled {target.Name}! Isn't that sweet? :P", Color.Thistle);
            TShock.Log.Info($"{args.Player.Name} tickles {target.Name}!");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("tickle", _config.TickleCooldown);
        }

        private void FacPoke(CommandArgs args)
        {
            if (!FirstCheck(args, "poke", new []{"<player>"}, _config.PokeCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            target.DamagePlayer(1);
            args.Player.SendInfoMessage($"You poked {target.Name}. Uh oh... should you run now?");
            TSPlayer.All.SendMessage($"{args.Player.Name} poked {target.Name}. Well, that was lovely! :3", Color.LightSkyBlue);
            TShock.Log.Info($"{args.Player.Name} poked {target.Name}.");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("poke", _config.PokeCooldown);
        }

        private void FacSPoke(CommandArgs args)
        {
            if (!FirstCheck(args, "spoke", new []{"<player>"}, _config.SpokeCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            NetMessage.SendPlayerDeath(target.Index, PlayerDeathReason.ByCustomReason($"{target.Name} was poked to death!"), 9999, _random.Next(-1, 1), false);
            args.Player.SendInfoMessage($"You poked {target.Name}. BOOM! BANG! PAW!");
            TSPlayer.All.SendMessage($"{args.Player.Name} poked {target.Name} in the tummy. KADUSH! Who is the next one?!", Color.MediumTurquoise);
            TShock.Log.Info($"{args.Player.Name} super-poked {target.Name}.");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("spoke", _config.SpokeCooldown);
        }

        private void FacHug(CommandArgs args)
        {
            if (!FirstCheck(args, "hug", new []{"<player>"}, _config.HugCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            args.Player.SendInfoMessage($"You hugged {target.Name}! You need love huh?");
            TSPlayer.All.SendMessage($"{args.Player.Name} hugged {target.Name}! Awwwhhh... how sweet? <3", Color.Chartreuse);
            TShock.Log.Info($"{args.Player.Name} hugged {target.Name}! Awwwhhh... how sweet? <3");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("hug", _config.HugCooldown);
        }

        private void FacLick(CommandArgs args)
        {
            if (!FirstCheck(args, "lick", new []{"<player>"}, _config.LickCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            args.Player.SendInfoMessage($"You licked {target.Name}! Really...?");
            TSPlayer.All.SendMessage($"{args.Player.Name} licked {target.Name}! Ugh... X.X", Color.DarkOrchid);
            TShock.Log.Info($"{args.Player.Name} licked {target.Name}!");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("lick", _config.LickCooldown);
        }

        private void FacPalm(CommandArgs args)
        {
            var play = _playerList[args.Player.UUID];
            if (play.CheckCooldown("facepalm", _config.FacepalmCooldown, out var left) && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage($"This command is on cooldown for {left} seconds.");
                return;
            }
            args.Player.SendInfoMessage("Well... why not?");
            TSPlayer.All.SendMessage($"{args.Player.Name} facepalmed.", Color.Chocolate);
            TShock.Log.Info($"{args.Player.Name} facepalmed.");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                play.SetCooldown("facepalm", _config.FacepalmCooldown);
        }

        private void FacKiss(CommandArgs args)
        {
            if (!FirstCheck(args, "kiss", new []{"<player>"}, _config.KissCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            args.Player.SendInfoMessage($"You kissed {target.Name}! RAAWWRRR! Are you horny?");
            TSPlayer.All.SendMessage($"{args.Player.Name} kisses {target.Name}! Love is everywhere! <3", Color.Coral);
            TShock.Log.Info($"{args.Player.Name} kisses {target.Name}!");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("kiss", _config.KissCooldown);
        }

        private void FacBaby(CommandArgs args)
        {
            if (!FirstCheck(args, "baby", new []{"<player>"}, _config.BabyCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            target.SetBuff(92);
            target.SetBuff(103, 3600, true);
            args.Player.SendInfoMessage("... does it made you happy? I hope it's your true love... otherwise much fun with the alimony! :D");
            TSPlayer.All.SendMessage($"{args.Player.Name} tried to make a baby (grinch) with {target.Name}! awwhhh look! It's soooo cute! <3", Color.Firebrick);
            TShock.Log.Info($"{args.Player.Name} tried to make a baby (grinch) with {target.Name}!");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("baby", _config.BabyCooldown);
        }

        private void FacStab(CommandArgs args)
        {
            if (!FirstCheck(args, "stab", new []{"<player>"}, _config.StabCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            target.DamagePlayer(9001);
            args.Player.SendInfoMessage($"You stabbed {target.Name} for OVER 9000 damage! That was close!");
            TSPlayer.All.SendMessage(
                _random.Next(2) == 0
                    ? $"{args.Player.Name} stabbed {target.Name} mercilessly! GO CATCH HIM! O.O"
                    : $"{target.Name} got stabbed mercilessly! But we couldn't find the culprit! O.O", Color.AliceBlue);
            TShock.Log.Info($"{args.Player.Name} stabbed {target.Name}.");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("stab", _config.StabCooldown);
        }

        private void FacLove(CommandArgs args)
        {
            if (!FirstCheck(args, "love", new []{"<player>"}, _config.LoveCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            args.Player.SendInfoMessage($"You love {target.Name}! hum... next step? ;)");
            TSPlayer.All.SendMessage($"{args.Player.Name} loves {target.Name}! Oehlalah... ready for the next step? <3", Color.Pink);
            TShock.Log.Info($"{args.Player.Name} loves {target.Name}!");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("love", _config.LoveCooldown);
        }

        private void FacPlant(CommandArgs args)
        {
            var play = _playerList[args.Player.UUID];
            if (play.CheckCooldown("faceplant", _config.FaceplantCooldown, out var left) && !args.Player.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage($"This command is on cooldown for {left} seconds.");
                return;
            }
            args.Player.SendInfoMessage("You planted your face on the ground. Serious man...?");
            NetMessage.SendPlayerDeath(args.Player.Index, PlayerDeathReason.LegacyEmpty(), 9999, _random.Next(-1, 1), false);
            var seriouslyRealGender = args.Player.TPlayer.Male ? "his" : "her";
            TSPlayer.All.SendMessage($"{args.Player.Name} planted {seriouslyRealGender} face on the ground. Are you crazy?!", Color.BlanchedAlmond);
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                play.SetCooldown("faceplant", _config.FaceplantCooldown);
        }

        private void FacSlap(CommandArgs args)
        {
            var play = _playerList[args.Player.UUID];
            if (play.CheckCooldown("slapall", _config.SlapAllCooldown, out var left) && !args.Player.Group.HasPermission("facommands.nocd"))
            {
                args.Player.SendErrorMessage($"This command is on cooldown for {left} seconds.");
                return;
            }
            args.Player.SendInfoMessage("You slapped everyone! That stings!");
            TSPlayer.All.SendMessage($"{args.Player.Name} slapped you (along with everyone else)! THE HELL?! CATCH HIM! :O", Color.BlanchedAlmond);
            TSPlayer.All.DamagePlayer(15);
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                play.SetCooldown("slapall", _config.SlapAllCooldown);
        }

        private void FacGift(CommandArgs args)
        {
            if (!FirstCheck(args, "gift", new []{"<player>"}, _config.GiftCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            var seriouslyRealGender = target.TPlayer.Male ? "boy" : "girl";
            if (_random.Next(2) == 0)
            {
                var itemById = TShock.Utils.GetItemById(1922);
                target.GiveItem(itemById.type, itemById.maxStack, itemById.prefix);
                target.SendInfoMessage($"{args.Player.Name} gave you Coal! You were a naughty {seriouslyRealGender}...");
                args.Player.SendSuccessMessage($"You gave {target.Name} Coal! {target.Name} was a naughty {seriouslyRealGender}...");
            }
            else
            {
                var itemById = TShock.Utils.GetItemById(1869);
                target.GiveItem(itemById.type, 1, itemById.prefix);
                target.SendInfoMessage($"{args.Player.Name} gave you a Present! You were a good {seriouslyRealGender}...");
                args.Player.SendSuccessMessage($"You gave {target.Name} a Present! {target.Name} was a good {seriouslyRealGender}...");
                if (!args.Player.Group.HasPermission("facommands.nocd"))
                    _playerList[args.Player.UUID].SetCooldown("gift", _config.GiftCooldown);
            }
        }

        private void FacDisturb(CommandArgs args)
        {
            if (!FirstCheck(args, "disturb", new []{"<player>"}, _config.DisturbCooldown, c => c != 1))
                return;
            if (!FindPlayer(args.Parameters[0], args.Player, out var target))
                return;
            foreach (var buff in new []{26, 30, 31, 32, 103, 115, 120})
                target.SetBuff(buff, 900, true);
            args.Player.SendInfoMessage($"You disturbed {target.Name}! You feel slightly better now...");
            TSPlayer.All.SendMessage($"{args.Player.Name} disturbed {target.Name}! Is he angry now? huh?", Color.Crimson);
            TShock.Log.Info($"{args.Player.Name} disturbed {target.Name}.");
            if (!args.Player.Group.HasPermission("facommands.nocd"))
                _playerList[args.Player.UUID].SetCooldown("disturb", _config.DisturbCooldown);
        }

        private void FacUI(CommandArgs args)
        {
            if (args.Parameters.Count == 1 && args.Parameters[0].Length != 0)
            {
                var user = args.Parameters[0];
                var userByName = TShock.UserAccounts.GetUserAccountByName(user);
                if (userByName != null)
                {
                    args.Player.SendMessage($"User {user} exists.", Color.DeepPink);
                    try
                    {
                        var dateTime1 = DateTime.Parse(userByName.Registered);
                        var dateTime2 = DateTime.Parse(userByName.LastAccessed);
                        var stringList = JsonConvert.DeserializeObject<List<string>>(userByName.KnownIps);
                        var str2 = stringList[stringList.Count - 1];
                        args.Player.SendMessage($"{user}'s group is {userByName.Group}.", Color.DeepPink);
                        args.Player.SendMessage($"{user}'s last known IP is {str2}.", Color.DeepPink);
                        args.Player.SendMessage($"{user}'s register date is {dateTime1.ToShortDateString()}.", Color.DeepPink);
                        args.Player.SendMessage($"{user} was last seen {dateTime2.ToShortDateString()}.", Color.DeepPink);
                    }
                    catch
                    {
                        var dateTime = DateTime.Parse(userByName.Registered);
                        args.Player.SendMessage($"{user}'s group is {userByName.Group}.", Color.DeepPink);
                        args.Player.SendMessage($"{user}'s register date is {dateTime.ToShortDateString()}.", Color.DeepPink);
                    }
                    return;
                }
                args.Player.SendMessage($"User {user} does not exist.", Color.DeepPink);
                return;
            }
            args.Player.SendErrorMessage("Syntax: /uinfo <player>");
        }

        private void FacBI(CommandArgs args)
        {
            if (args.Parameters.Count == 0)
            {
                args.Player.SendErrorMessage("Invalid syntax: /baninfo <player>");
                return;
            }
            var banByName = TShock.Bans.GetBanByName(args.Parameters[0]);
            if (banByName == null)
            {
                args.Player.SendErrorMessage("No bans by this name were found.");
                return;
            }
            args.Player.SendInfoMessage($"Account name: {banByName.Name} ({banByName.IP})");
            args.Player.SendInfoMessage($"Date banned: {banByName.Date}");
            if (banByName.Expiration != "")
                args.Player.SendInfoMessage($"Expiration date: {banByName.Expiration}");
            args.Player.SendInfoMessage($"Banning user: {banByName.BanningUser}");
            args.Player.SendInfoMessage($"Reason: {banByName.Reason}");
        }

        private bool FindPlayer(string param, TSPlayer issuer, out TSPlayer player)
        {
            player = null;
            var players = TShock.Players.Where(p => p?.Name?.Contains(param) ?? false).ToArray();
            if (players.Length != 1)
            {
                if (players.Length > 1)
                    issuer.SendMultipleMatchError(players.Select(p => p.Name));
                else
                    issuer.SendErrorMessage("No players matched!");
                return false;
            }
            player = players[0];
            return true;
        }

        private bool FirstCheck(CommandArgs args, string commandName, string[] arguments, int cooldown, Func<int, bool> expr)
        {
            var play = _playerList[args.Player.UUID];
            var returnString = "";
            if (!play.CheckCooldown(commandName, cooldown, out var left) && !args.Player.Group.HasPermission("facommands.nocd"))
                returnString = $"This command is on cooldown for {left} seconds.";
            else if (expr(args.Parameters.Count))
                returnString = $"Invalid syntax! Proper syntax: /{commandName} {string.Join(" ", arguments)}";
            else if (args.Parameters[0].Length == 0)
                returnString = $"Invalid {arguments[0].Replace("<", "").Replace(">", "")}!";
            if (returnString == "") return true;
            args.Player.SendErrorMessage(returnString);
            return false;
        }

        private void Reload(CommandArgs args)
        {
            try
            {
                var newConfig = Config.Read(true);
                if (newConfig != null)
                {
                    _config = newConfig;
                    args.Player.SendSuccessMessage("Successfully reloaded config!");
                    return;
                }
            }
            catch
            {
                args.Player.SendErrorMessage("Could not read config!");
            }
            args.Player.SendErrorMessage("Failed to reload config!");
        }

        private class Player
        {
            public Player() => Cooldowns = new Dictionary<string, Cooldown>();
            //public List<string> RankList { get; set; } // Note: It was in the legacy code. I don't know what it was for
            private Dictionary<string, Cooldown> Cooldowns { get; }

            public bool CheckCooldown(string command, int cooldown, out int left)
            {
                left = 0;
                if (!Cooldowns.ContainsKey(command)) return true;
                if (Cooldowns[command].LastCooldown != cooldown) return true;
                if (Cooldowns[command].LastUsed.Add(TimeSpan.FromSeconds(cooldown)) <= DateTime.Now) return true;
                left = DateTime.Now.Subtract(Cooldowns[command].LastUsed.Add(TimeSpan.FromSeconds(cooldown))).Seconds * -1;
                return false;
            }

            public void SetCooldown(string command, int cooldown) => Cooldowns[command] = new Cooldown {LastUsed = DateTime.Now, LastCooldown = cooldown};

            private struct Cooldown
            {
                public DateTime LastUsed;
                public int LastCooldown;
            }
        }
    }
}
