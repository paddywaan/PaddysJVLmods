using Jotunn.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BatchCraft
{
    class Hooks
    {
        private static GameObject craftAllButton;
        public Hooks()
        {

        }

        public static bool Init()
        {
            //On.FejdStartup.OnCharacterStart += FejdStartup_OnCharacterStart;
            //On.FejdStartup.Awake += FejdStartup_Awake;
            On.InventoryGui.SetRecipe += InventoryGui_SetRecipe;
            On.InventoryGui.UpdateRecipe += InventoryGui_UpdateRecipe;
            On.InventoryGui.OnCraftPressed += InventoryGui_OnCraftPressed;
            return true;
        }

        private static void FejdStartup_OnCharacterStart(On.FejdStartup.orig_OnCharacterStart orig, FejdStartup self)
        {
            orig(self);
            Main.logger.LogWarning($"Init start.");
            craftAllButton = GUIManager.Instance.CreateButton($"Craft All", self.m_selectCharacterPanel.transform, new Vector2(0.51f, 0f),
                Vector2.one, Vector2.zero, 100f, 25f);
            var craftAllRect = craftAllButton.GetComponent<RectTransform>();
            craftAllRect.offsetMax = new Vector2(0f, -5f);
            craftAllRect.offsetMin = new Vector2(0, 5f);
            Main.logger.LogWarning($"CreateButton finished.");

            Main.logger.LogWarning($"Setup OnClick");
            Button button = craftAllButton.GetComponent<Button>();
            var proc = craftAllButton.AddComponent<BatchCraftProcessor>();
            /*if (!IsUpgradable())*/ craftAllButton.SetActive(true);
            button.onClick.AddListener(OnCraftAllClick);
            /*if (!IsUpgradable())*/ craftAllButton.SetActive(true);
            Main.logger.LogWarning($"Init start.");
        }

        private static void Menu_Start(On.Menu.orig_Start orig, Menu self)
        {
            
        }

        private static bool Menu_IsVisible(On.Menu.orig_IsVisible orig)
        {
            var o = orig();
            
            return orig();

        }

        private static void InventoryGui_OnCraftPressed(On.InventoryGui.orig_OnCraftPressed orig, InventoryGui self)
        {
            orig(self);
            craftAllButton.SetActive(false);
        }

        public static void setupCraftAllButton(InventoryGui invent)
        {
            
            var panel = invent.m_craftButton.transform.parent.gameObject;
            craftAllButton = GUIManager.Instance.CreateButton($"Craft All", invent.m_craftButton.transform.parent, new Vector2(0.51f,0f), 
                Vector2.one, Vector2.zero, 100f, 25f);
            var craftAllRect = craftAllButton.GetComponent<RectTransform>();
            craftAllRect.offsetMax = new Vector2(0f, -5f);
            craftAllRect.offsetMin = new Vector2(0, 5f);
            
            
            
            Button button = craftAllButton.GetComponent<Button>();
            var proc = craftAllButton.AddComponent<BatchCraftProcessor>();
            button.onClick.AddListener(OnCraftAllClick);
            if (!IsUpgradable()) craftAllButton.SetActive(true);



        }

        private static void OnCraftAllClick()
        {
            Main.logger.LogDebug($"OnCraftAllClick");
            if (InventoryGui.instance == null || !(InventoryGui.instance.m_selectedRecipe.Key))
            {
                Main.logger.LogDebug($"Invent instance/recipe was null.");
                return;
            }
            
            int num = (InventoryGui.instance.m_craftUpgradeItem != null) ? (InventoryGui.instance.m_craftUpgradeItem.m_quality + 1) : 1;
            var hasReqs = Player.m_localPlayer.HaveRequirements(InventoryGui.instance.m_selectedRecipe.Key, false, num);
            if (hasReqs && !Player.m_localPlayer.NoCostCheat())
            {
                Main.logger.LogDebug($"Player has requirements");
                var proc = craftAllButton.GetComponent<BatchCraftProcessor>();
                if (proc == null) Main.logger.LogDebug($"proc was null");
                else
                {

                    Main.logger.LogDebug($"Starting Coroutine:");

                    proc.StartCoroutine("ProcessBatch");
                }
            }

        }

        

        private static void InventoryGui_UpdateRecipe(On.InventoryGui.orig_UpdateRecipe orig, InventoryGui self, Player player, float dt)
        {
            orig(self, player, dt);
            if(InventoryGui.instance.m_selectedRecipe.Key)
            {
                var craftRect = self.m_craftButton.GetComponent<RectTransform>();

                if (!craftAllButton)
                {
                    setupCraftAllButton(self);
                }
                else
                {
                    if(self.m_craftTimer > 0)
                    {
                        craftAllButton.SetActive(false);
                    }
                    else if (self.m_craftTimer == -1f)
                    {
                        if (IsUpgradable())
                        {
                            craftAllButton.SetActive(false);
                            craftRect.anchorMax = new Vector2(1f, 1f);

                        }
                        else
                        {
                            ItemDrop.ItemData value = InventoryGui.instance.m_selectedRecipe.Value;
                            int num = (value != null) ? (value.m_quality + 1) : 1;
                            if (!(num > InventoryGui.instance.m_selectedRecipe.Key.m_item.m_itemData.m_shared.m_maxQuality))
                            {
                                craftAllButton.SetActive(true);
                                craftRect.anchorMax = new Vector2(0.49f, 1f);
                            }
                        }
                    }
                }
            }
        }

        private static void InventoryGui_SetRecipe(On.InventoryGui.orig_SetRecipe orig, InventoryGui self, int index, bool center)
        {
            orig(self, index, center);
            if (self.m_selectedRecipe.Key)
            {
                ItemDrop.ItemData value = self.m_selectedRecipe.Value;
                int num = (value != null) ? (value.m_quality + 1) : 1;
                Main.logger.LogDebug($"{self.m_selectedRecipe.Key.name} is {(IsUpgradable() ? "" : "not ")}upgradable. Num:{num} vs max: {self.m_selectedRecipe.Key.m_item.m_itemData.m_shared.m_maxQuality}");
            }
        }

        private static bool IsUpgradable()
        {
            if (InventoryGui.instance.m_selectedRecipe.Key)
            {
                ItemDrop.ItemData value = InventoryGui.instance.m_selectedRecipe.Value;
                int num = (value != null) ? (value.m_quality + 1) : 1;
                bool flag = (num > 1 && num <= InventoryGui.instance.m_selectedRecipe.Key.m_item.m_itemData.m_shared.m_maxQuality);
                //flag = !flag;
                return flag;
            }
            return false;
            
        }
    }
}
