using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.ID;
using TShockAPI;

namespace FACommands;

public class Config
{
    [JsonIgnore]
    private static string Path => System.IO.Path.Combine(TShock.SavePath, "FACommands.json");
    public static Config Create()
    {
        if (File.Exists(Path))
            return Read();
        TShock.Log.Info("Creating new Config file");
        try
        {
            var conf = new Config();
            conf.Validate();
            Directory.CreateDirectory(TShock.SavePath);
            Write(conf);
            return conf;
        }
        catch
        {
            TShock.Log.ConsoleError("[FACommands] Failed to create new config file!");
            throw;
        }
    }

    public static Config Read()
    {
        TShock.Log.Info("Reading config file");
        var config = Parse(File.ReadAllText(Path), out var migrated);
        if (migrated)
        {
            TShock.Log.Info("Converting old config file");
            // Keep the original legacy settings before converting them.
            if (!File.Exists(Path + ".legacy.bak"))
                File.Copy(Path, Path + ".legacy.bak");
            Write(config);
        }
        return config;
    }

    internal static Config Parse(string rawJson, out bool migrated)
    {
        var json = JObject.Parse(rawJson);
        var legacyNames = new Dictionary<string, string>
        {
            ["ranklist"] = "Rank List", ["moreCD"] = "More Cooldown",
            ["slayCD"] = "Slay Cooldown", ["fartCD"] = "Fart Cooldown",
            ["tickleCD"] = "Tickle Cooldown", ["pokeCD"] = "Poke Cooldown",
            ["spokeCD"] = "Spoke Cooldown", ["hugCD"] = "Hug Cooldown",
            ["lickCD"] = "Lick Cooldown", ["facepalmCD"] = "Facepalm Cooldown",
            ["kissCD"] = "Kiss Cooldown", ["babyCD"] = "Baby Cooldown",
            ["stabCD"] = "Stab Cooldown", ["loveCD"] = "Love Cooldown",
            ["faceplantCD"] = "Faceplant Cooldown", ["slapallCD"] = "Slap All Cooldown",
            ["giftCD"] = "Gift Cooldown", ["disturbCD"] = "Disturb Cooldown",
            ["bankCD"] = "Bank Cooldown", ["maxRollDice"] = "Max Roll Dice",
            ["minRangeRollDice"] = "Min Roll Dice"
        };
        migrated = false;
        foreach (var (legacy, current) in legacyNames)
        {
            if (!json.TryGetValue(legacy, StringComparison.OrdinalIgnoreCase, out var value)) continue;
            // Modern values take priority in a mixed-format file.
            if (json.GetValue(current, StringComparison.OrdinalIgnoreCase) == null)
                json[current] = value.DeepClone();
            migrated = true;
        }
        var config = json.ToObject<Config>() ?? throw new JsonSerializationException("Failed to read configuration");
        config.Validate();
        return config;
    }

    internal void Validate()
    {
        if (RankList == null || RankList.Any(rank => rank == null))
            throw new JsonSerializationException("Rank List must be an array of strings.");
        if (NpcList == null || NpcList.Any(id => id <= 0 || id >= NPCID.Count))
            throw new JsonSerializationException($"NPC Spawn List must contain IDs from 1 to {NPCID.Count - 1}.");
        if (OwnerBroadcastPrefix == null || OwnerBroadcastColor == null)
            throw new JsonSerializationException("Owner broadcast prefix and color must not be null.");
        if (MinRollDice > MaxRollDice)
            throw new JsonSerializationException("Min Roll Dice must not exceed Max Roll Dice.");
        foreach (var property in typeof(Config).GetProperties().Where(p => p.Name.EndsWith("Cooldown", StringComparison.Ordinal)))
            if ((int)property.GetValue(this)! < 0)
                throw new JsonSerializationException($"{property.Name} must not be negative.");
    }

    private static void Write(Config config)
    {
        var temporary = Path + ".tmp";
        File.WriteAllText(temporary, JsonConvert.SerializeObject(config, Formatting.Indented));
        File.Move(temporary, Path, overwrite: true);
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
        NpcList = new[]{ 19, 54, 209, 38, 20, 207, 107, 22, 124, 17, 18, 227, 208, 229, 178, 353, 368, 160, 228, 108, 369, 441, 550, 142, 588, 633, 663 };
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
        public Color ConvertToColor() => new (R, G, B, 255);
    }

    public class OldConfig
    {
        [JsonProperty("ranklist")]
        public string[] RankList { get; set; } = Array.Empty<string>();
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
