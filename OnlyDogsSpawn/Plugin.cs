using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace OnlyDogsSpawn
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class OnlyDogsSpawn : BaseUnityPlugin
    {
        public static bool loaded;
        private const string modGUID = "Alphonso.OnlyDogsSpawn";
        private const string modName = "OnlyDogsSpawn";
        private const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static OnlyDogsSpawn Instance;
        public static ManualLogSource mls;
        private void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("OnlyDogsSpawn");
            // Plugin startup logic
            mls.LogInfo("Loaded OnlyDogsSpawn and applying patches.");
            harmony.PatchAll(typeof(OnlyDogsSpawn));
            mls = Logger;
        }

        [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.LoadNewLevel))]
        [HarmonyPrefix]
        static bool Only_Dogs_Spawn(ref SelectableLevel newLevel)
        {

            foreach (var item in newLevel.OutsideEnemies)
            {
                item.rarity = 0;
                if (item.enemyType.enemyPrefab.GetComponent<MouthDogAI>() != null)
                {
                    item.rarity = 999;
                }
            }
            return true;
        }

    }
}
