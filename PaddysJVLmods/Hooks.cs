using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Steamworks;

namespace PaddysJVLmods
{
    public static class Hooks
    {
        internal static void Init()
        {
            if (Conf.WorkBenchRadiusEnabled.Value)
            {
                IL.CraftingStation.HaveBuildStationInRange += CraftingStation_HaveBuildStationInRange;
                On.CircleProjector.CreateSegments += CircleProjector_CreateSegments;
            }
            On.Vagon.UpdateMass += Vagon_UpdateMass;
            On.InventoryGui.Update += InventoryGui_Update;
            On.Minimap.Start += Minimap_Start;
            if (Conf.AutoShout.Value) On.Chat.InputText += Chat_InputText;
            if (Conf.TrashFilter.Value) IL.Player.AutoPickup += Player_AutoPickup;
            On.ZSteamSocket.OnNewConnection += ZSteamSocket_OnNewConnection;
#if DEBUG
            On.Player.GetRunSpeedFactor += Player_GetRunSpeedFactor;

            On.OfferingBowl.Awake += OfferingBowl_Awake;
#endif
        }

        private static void OfferingBowl_Awake(On.OfferingBowl.orig_Awake orig, OfferingBowl self)
        {
            //Jotunn.Logger.LogWarning($"Prefix OfferingBowl_Awake ran on {self.m_name}");
            orig(self);
            //Jotunn.Logger.LogWarning($"Postfix OfferingBowl_Awake ran on {self.m_name}");
        }

        private static float Player_GetRunSpeedFactor(On.Player.orig_GetRunSpeedFactor orig, Player self)
        {
            if (self.GetCurrentWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                return orig(self) * 1.5f;
            }
            return orig(self);
        }

        //private static float Humanoid_GetAttackDrawPercentage(On.Humanoid.orig_GetAttackDrawPercentage orig, Humanoid self)
        //{
        //    //var oself = self.m_attackDrawTime;
        //    var num = orig(self);
        //    Jotunn.Logger.LogDebug($"{num}");
        //    return num;
        //}

        private static void ZSteamSocket_OnNewConnection(On.ZSteamSocket.orig_OnNewConnection orig, ZSteamSocket self, HSteamNetConnection con)
        {
            orig(self, con);
            Jotunn.Logger.LogDebug($"New player connected with Steam64ID: {self.GetPeerID()}");
        }

        private static void Player_AutoPickup(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            int num;
            ILLabel cElse = null;
            if (c.TryGotoNext(MoveType.After,
                    zz => zz.MatchLdloc(out num),
                    zz => zz.MatchLdarg(0),
                    zz => zz.MatchLdfld<Player>("m_autoPickupRange"),
                    zz => zz.MatchBgt(out cElse)))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldloc_S, (byte)4);
                c.EmitDelegate<Func<Player, ItemDrop, bool>>((player, item) => PlayerPickup(player, item));
                c.Emit(OpCodes.Brtrue, cElse.Target);
            }
            else Jotunn.Logger.LogWarning($"Failed to match in Player_AutoPickup");
        }

        private static bool PlayerPickup(Player player, ItemDrop item)
        {
            if (Conf.FilterList.Any(zz => item.name.Equals(zz))) return true;
            Jotunn.Logger.LogDebug($"{player.m_name} {player.GetPlayerName()} picked up {item.name}");
            return false;
        }

        private static void Chat_InputText(On.Chat.orig_InputText orig, Chat self)
        {
            var text = self.m_input.text;
            if (text.StartsWith("\\")) orig(self);
            self.SendText(Talker.Type.Shout, text);
        }

        private static void Minimap_Start(On.Minimap.orig_Start orig, Minimap self)
        {
            orig(self);
            self.m_publicPosition.isOn = Conf.PublicPlayerPosition.Value;
            ZNet.instance.SetPublicReferencePosition(self.m_publicPosition.isOn);
        }

        public static void CircleProjector_CreateSegments(On.CircleProjector.orig_CreateSegments orig, CircleProjector self)
        {
            self.m_radius = Conf.WorkbenchRadius.Value;
            orig(self);
        }

        public static void CraftingStation_HaveBuildStationInRange(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                MoveType.Before,
                zz => zz.MatchLdloc(1),
                zz => zz.MatchLdfld<CraftingStation>("m_rangeBuild"),
                zz => zz.MatchStloc(2)
                ))
            {
                c.Next.OpCode = OpCodes.Nop;
                c.Next.Next.OpCode = OpCodes.Ldc_R4;
                c.Next.Next.Operand = Conf.WorkbenchRadius.Value;
            }
            else Jotunn.Logger.LogWarning($"Failed to match in CraftingStation_HaveBuildStationInRange");
        }

        private static void InventoryGui_Update(On.InventoryGui.orig_Update orig, InventoryGui self)
        {
            orig(self);
            int alphaBegin = (int)KeyCode.Alpha0;
            int keypadBegin = (int)KeyCode.Keypad0;
            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown((KeyCode)alphaBegin + i) || Input.GetKeyDown((KeyCode)keypadBegin + i))
                {
                    self.m_splitSlider.value = i;
                    self.OnSplitSliderChanged(i);
                }
            }
        }

        public static void Vagon_UpdateMass(On.Vagon.orig_UpdateMass orig, Vagon self)
        {
            var x = self.m_itemWeightMassFactor;
            self.m_itemWeightMassFactor = Conf.CartMassMultiplier.Value;
            orig(self);
        }
    }
}
