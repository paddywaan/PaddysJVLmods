using Mono.Cecil.Cil;
using MonoMod.Cil;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemDropDecayConfig
{
    public static class Hooks
    {
        internal static void Init()
        {
            //IL.*
            //On.*
            IL.ItemDrop.TimedDestruction += ItemDropOnTimedDestruction;
        }

        private static void ItemDropOnTimedDestruction(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
                zz => zz.MatchCallOrCallvirt<ItemDrop>("GetTimeSinceSpawned"),
                zz => zz.MatchLdcR8(3600f),
                zz => zz.MatchBgeUn(out _)
            ))
            {
                c.Next.Next.Operand = ItemDropDecayConfig.DecayTimer.Value;
            }
            else Jotunn.Logger.LogWarning($"Failed to patch ItemDropOnTimedDestruction.");
        }
    }
}