using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using EFT.InventoryLogic;

namespace InventoryPatch
{
    [BepInPlugin("zzzNIKOzzz.InventoryPatch", "InventoryPatch", PluginVersion)]
    public class InventoryPatchPlugin : BasePlugin
    {
        public const string PluginVersion = "1.0.0";
		internal static ManualLogSource Logger;

        public override void Load()
        {
            Logger = Log;

            Logger.LogInfo("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");
            Logger.LogInfo("___________________InventoryPatch от zzzNIKOzzz загружается___________________");


            var harmony = new Harmony("zzznikozzz.inventorypatch");
            int patched = 0;

            PatchMethod(harmony, typeof(InventoryController), "IsAtReachablePlace", ref patched);
            PatchMethod(harmony, typeof(InventoryController), "IsAtBindablePlace", ref patched);

            Logger.LogInfo("InventoryPatch: " + patched + " патча УСПЕШНО применены. И это прекрасно!  Да?");
            Logger.LogInfo("zzzNIKOzzz: Рюкзаки, подсумки, похуй! Сегодня я решаю, откуда буду лечиться!!!");
            Logger.LogInfo("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");
        }

        private static void PatchMethod(Harmony harmony, System.Type type, string name, ref int count)
        {
            var m = AccessTools.Method(type, name);
            if (m != null)
            {
                harmony.Patch(m, prefix: new HarmonyMethod(
                    typeof(InventoryPatchPlugin), nameof(AlwaysTrue)));
                count++;
                Logger.LogInfo("Patched: " + name);
            }
        }

        private static bool AlwaysTrue(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}
