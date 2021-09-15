using Mono.Cecil.Cil;
using MonoMod.Cil;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mono.Cecil;
using MonoMod;

namespace CombatTweaks
{
    public static class Hooks
    {
        internal static void Init()
        {
            //IL.*
            //On.*
            IL.Humanoid.UpdateBlock += HumanoidOnUpdateBlock;
        }

        private static void HumanoidOnUpdateBlock(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            ILLabel isBlocking = null;
            if (c.TryGotoNext(MoveType.After,
                zz => zz.MatchLdarg(0),
                zz => zz.MatchLdfld<Humanoid>("m_internalBlockingState"),
                zz => zz.MatchBrtrue(out isBlocking)
                ))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<Humanoid, bool>>((Humanoid self) =>
                {
                    if (self.HaveStamina(5f))
                    {
                        self.UseStamina(5f);
                        return true;
                    }

                    return false;
                });
                c.Emit(OpCodes.Brfalse, isBlocking);
            }
            else Jotunn.Logger.LogWarning($"Failed to patch HumanoidOnUpdateBlock");
        }
    }
}