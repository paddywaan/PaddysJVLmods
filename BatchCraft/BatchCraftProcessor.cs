using System.Collections;
using UnityEngine;

namespace BatchCraft
{
    public class BatchCraftProcessor : MonoBehaviour
    {
        void Start()
        {
            Main.logger.LogDebug($"Component added.");
        }
        IEnumerator ProcessBatch()
        {
            Main.logger.LogDebug($"Routine started.");
            //if(InventoryGui.instance.m_craftRecipe == null) Main.logger.LogWarning($"Craft recipe was null!");
            //InventoryGui.instance.DoCrafting(Player.m_localPlayer);
            //yield return null;
            if (InventoryGui.instance && InventoryGui.instance.m_selectedRecipe.Key)
            {
                int num = (InventoryGui.instance.m_craftUpgradeItem != null) ? (InventoryGui.instance.m_craftUpgradeItem.m_quality + 1) : 1;
                InventoryGui.instance.m_craftRecipe = InventoryGui.instance.m_selectedRecipe.Key;
                InventoryGui.instance.m_craftUpgradeItem = InventoryGui.instance.m_selectedRecipe.Value;
                InventoryGui.instance.m_craftVariant = InventoryGui.instance.m_selectedVariant;
                
                if (InventoryGui.instance.m_craftRecipe.m_craftingStation)
                {
                    CraftingStation currentCraftingStation = Player.m_localPlayer.GetCurrentCraftingStation();
                    if (currentCraftingStation)
                    {
                        while (Player.m_localPlayer.HaveRequirements(InventoryGui.instance.m_selectedRecipe.Key, false, num))
                        {
                            //if (InventoryGui.instance.m_craftTimer >= InventoryGui.instance.m_craftDuration)
                            //{
                             //   InventoryGui.instance.m_craftTimer = 0f;
                                currentCraftingStation.m_craftItemEffects.Create(Player.m_localPlayer.transform.position, Quaternion.identity, null, 1f, -1);
                                InventoryGui.instance.DoCrafting(Player.m_localPlayer);
                                Main.logger.LogDebug($"Crafted 1 in batch");
                                
                            //}
                            yield return new WaitForSeconds(InventoryGui.instance.m_craftDuration + 0.1f);
                        }
                        
                    }
                }
                else
                {
                    InventoryGui.instance.m_craftItemEffects.Create(Player.m_localPlayer.transform.position, Quaternion.identity, null, 1f, -1);
                }
                
            }
        }
        void Stop()
        {
            Main.logger.LogDebug($"Routine Stopping.");
        }
    }
}