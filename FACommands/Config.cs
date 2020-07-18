using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using TShockAPI;

namespace FACommands
{
    public class Config
    {
        [JsonIgnore]
        private static readonly string Path = System.IO.Path.Combine(TShock.SavePath, "FACommands.json");
        public static Config Create()
        {
            if (File.Exists(Path))
                return Read();
            TShock.Log.Info("Creating new Config file");
            try
            {
                var conf = new Config();
                File.WriteAllText(Path, JsonConvert.SerializeObject(conf, Formatting.Indented));
                return conf;
            }
            catch
            {
                TShock.Log.ConsoleError("[FACommands] Failed to create new config file!");
                throw;
            }
        }

        public static Config Read(bool returnNull = false)
        {
            TShock.Log.Info("Reading config file");
            try
            {
                var sr = new StreamReader(File.Open(Path, FileMode.Open));
                var rawJson = sr.ReadToEnd();
                sr.Close();
                if (rawJson.Contains("Cooldown"))
                {
                    var conf = JsonConvert.DeserializeObject<Config>(rawJson);
                    File.WriteAllText(Path, JsonConvert.SerializeObject(conf, Formatting.Indented));
                    return conf;
                }
                TShock.Log.Info("Converting old config file");
                var old = JsonConvert.DeserializeObject<OldConfig>(rawJson);
                var newConfig = new Config
                {
                    RankList = old.RankList,
                    MoreCooldown = old.MoreCd,
                    SlayCooldown = old.SlayCd,
                    FartCooldown = old.FartCd,
                    TickleCooldown = old.TickleCd,
                    PokeCooldown = old.PokeCd,
                    SpokeCooldown = old.SpokeCd,
                    HugCooldown = old.HugCd,
                    LickCooldown = old.LickCd,
                    FacepalmCooldown = old.FacepalmCd,
                    KissCooldown = old.KissCd,
                    BabyCooldown = old.BabyCd,
                    StabCooldown = old.StabCd,
                    LoveCooldown = old.LoveCd,
                    FaceplantCooldown = old.FaceplantCd,
                    SlapAllCooldown = old.SlapAllCd,
                    GiftCooldown = old.GiftCd,
                    DisturbCooldown = old.DisturbCd,
                    BankCooldown = old.BankCd,
                    MaxRollDice = old.MaxRollDice,
                    MinRollDice = old.MinRangeRollDice,
                    NpcList = new[] { 19, 54, 209, 38, 20, 207, 107, 22, 124, 17, 18, 227, 208, 229, 178, 353, 368, 160, 228, 108, 369, 441, 550, 142, 588, 633 },
                    OwnerBroadcastPrefix = "(Owner Broadcast)",
                    OwnerBroadcastColor = new ConfigColor { R = Color.Cyan.R, G = Color.Cyan.G, B = Color.Cyan.B }
            };
                File.WriteAllText(Path, JsonConvert.SerializeObject(newConfig, Formatting.Indented));
                return newConfig;
            }
            catch (JsonReaderException e)
            {
                TShock.Log.Error(e.ToString());
                var additional = returnNull ? "" : " Using Defaults!";
                TShock.Log.ConsoleError($"[FACommands] Failed to load config!{additional}");
                return returnNull ? null : new Config();
            }
        }

        public Config()
        {
            RankList = new [] { "Traveler, Citizen, Soldier, Fighter, Warrior, Champion, Gladiator, Commander, Warmaster, Hero, Executor, Lord, Legend, Demigod, Immortal, Unattainable, Keeper of Gods" };
            MoreCooldown = 120;
            SlayCooldown = 60;
            FartCooldown = 60;
            TickleCooldown = 60;
            PokeCooldown = 60;
            SpokeCooldown = 120;
            HugCooldown = 30;
            LickCooldown = 30;
            FacepalmCooldown = 30;
            KissCooldown = 30;
            BabyCooldown = 300;
            StabCooldown = 120;
            LoveCooldown = 30;
            FaceplantCooldown = 120;
            SlapAllCooldown = 120;
            GiftCooldown = 300;
            DisturbCooldown = 120;
            BankCooldown = 10;
            MaxRollDice = 100;
            MinRollDice = 1;
            NpcList = new[]{ 19, 54, 209, 38, 20, 207, 107, 22, 124, 17, 18, 227, 208, 229, 178, 353, 368, 160, 228, 108, 369, 441, 550, 142, 588, 633 };
            OwnerBroadcastPrefix = "(Owner Broadcast)";
            OwnerBroadcastColor = new ConfigColor{ R = Color.Cyan.R, G = Color.Cyan.G, B = Color.Cyan.B };
        }

        [JsonProperty("Rank List")]
        public string[] RankList { get; set; }
        [JsonProperty("More Cooldown")]
        public int MoreCooldown { get; set; }
        [JsonProperty("Slay Cooldown")]
        public int SlayCooldown { get; set; }
        [JsonProperty("Fart Cooldown")]
        public int FartCooldown { get; set; }
        [JsonProperty("Tickle Cooldown")]
        public int TickleCooldown { get; set; }
        [JsonProperty("Poke Cooldown")]
        public int PokeCooldown { get; set; }
        [JsonProperty("Spoke Cooldown")]
        public int SpokeCooldown { get; set; }
        [JsonProperty("Hug Cooldown")]
        public int HugCooldown { get; set; }
        [JsonProperty("Lick Cooldown")]
        public int LickCooldown { get; set; }
        [JsonProperty("Facepalm Cooldown")]
        public int FacepalmCooldown { get; set; }
        [JsonProperty("Kiss Cooldown")]
        public int KissCooldown { get; set; }
        [JsonProperty("Baby Cooldown")]
        public int BabyCooldown { get; set; }
        [JsonProperty("Stab Cooldown")]
        public int StabCooldown { get; set; }
        [JsonProperty("Love Cooldown")]
        public int LoveCooldown { get; set; }
        [JsonProperty("Faceplant Cooldown")]
        public int FaceplantCooldown { get; set; }
        [JsonProperty("Slap All Cooldown")]
        public int SlapAllCooldown { get; set; }
        [JsonProperty("Gift Cooldown")]
        public int GiftCooldown { get; set; }
        [JsonProperty("Disturb Cooldown")]
        public int DisturbCooldown { get; set; }
        [JsonProperty("Bank Cooldown")]
        public int BankCooldown { get; set; }
        [JsonProperty("Max Roll Dice")]
        public int MaxRollDice { get; set; }
        [JsonProperty("Min Roll Dice")]
        public int MinRollDice { get; set; }
        [JsonProperty("NPC Spawn List")]
        public int[] NpcList { get; set; }
        [JsonProperty("Owner Broadcast Prefix")]
        public string OwnerBroadcastPrefix { get; set; }
        [JsonProperty("Owner Broadcast Color")]
        public ConfigColor OwnerBroadcastColor { get; set; }

        public class ConfigColor
        {
            public byte R;
            public byte G;
            public byte B;
            public Color ConvertToColor() => new Color(R, G, B, 255);
        }

        public class OldConfig
        {
            [JsonProperty("ranklist")]
            public string[] RankList { get; set; }
            [JsonProperty("moreCD")]
            public int MoreCd { get; set; }
            [JsonProperty("slayCD")]
            public int SlayCd { get; set; }
            [JsonProperty("fartCD")]
            public int FartCd { get; set; }
            [JsonProperty("tickleCD")]
            public int TickleCd { get; set; }
            [JsonProperty("pokeCD")]
            public int PokeCd { get; set; }
            [JsonProperty("spokeCD")]
            public int SpokeCd { get; set; }
            [JsonProperty("hugCD")]
            public int HugCd { get; set; }
            [JsonProperty("lickCD")]
            public int LickCd { get; set; }
            [JsonProperty("facepalmCD")]
            public int FacepalmCd { get; set; }
            [JsonProperty("kissCD")]
            public int KissCd { get; set; }
            [JsonProperty("babyCD")]
            public int BabyCd { get; set; }
            [JsonProperty("stabCD")]
            public int StabCd { get; set; }
            [JsonProperty("loveCD")]
            public int LoveCd { get; set; }
            [JsonProperty("faceplantCD")]
            public int FaceplantCd { get; set; }
            [JsonProperty("slapallCD")]
            public int SlapAllCd { get; set; }
            [JsonProperty("giftCD")]
            public int GiftCd { get; set; }
            [JsonProperty("disturbCD")]
            public int DisturbCd { get; set; }
            [JsonProperty("bankCD")]
            public int BankCd { get; set; }
            [JsonProperty("maxRollDice")]
            public int MaxRollDice { get; set; }
            [JsonProperty("minRangeRollDice")]
            public int MinRangeRollDice { get; set; }
        }
    }
}
