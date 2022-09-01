// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.HairDye.Server.Items.Tools
{
    using Eco.Core.Items;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;
    using Eco.EM.Framework.Utils;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.UI;
    using Eco.HairDye.Server.Utils;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    [Serialized]
    [Category("Hidden")]
    [Tag("Tool", 1)]
    public abstract class HairColorBrushToolItem : ToolItem
    {
        private static readonly IDynamicValue caloriesBurn = new MultiDynamicValue(MultiDynamicOps.Multiply, new TalentModifiedValue(typeof(HairColorBrushToolItem), typeof(ToolEfficiencyTalent)), CreateCalorieValue(8, typeof(SelfImprovementSkill), typeof(HairColorBrushToolItem)));
        public override IDynamicValue CaloriesBurn => caloriesBurn;
        public override float DurabilityRate => DurabilityMax / 2500f;
        public override Item RepairItem => Get<FurPeltItem>();
        public override int FullRepairAmount => 1;
        public override LocString LeftActionDescription => Localizer.DoStr("Dye");
        public virtual string Color { get; set; }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (Durability == 0)
                return InteractResult.Failure(Localizer.DoStr("The Hair Color Brush is broken and needs repairs, it can not be used until it has been repaired"));

            var inventory = context.Player.User.Inventory.ToolbarBackpack;
            var requiredHairDye = ItemUtil.TryGet($"{Color}HairDyeItem");
            if (requiredHairDye == null)
                return InteractResult.Failure(Localizer.DoStr($"You require {Color} dye in your toolbar but that color does not seem to exist..."));

            Item hairDye = null;
            try
            {
                var inventoryQuery = inventory.AllParentsAndSelf.SelectMany(i => i.Stacks.Where(itm => itm.Item.DisplayName.ToLower() == requiredHairDye.DisplayName.ToLower()));
                hairDye = inventoryQuery.First().Item;
            } catch (Exception) { }

            if (hairDye == null)
                return InteractResult.Failure(Localizer.DoStr($"You require {Color} dye in your toolbar to be able to dye your hair..."));

            // After everything is good, Finish the action
            var avatar = context.Player.User.Avatar;
            var chosenColor = new Color(Constants.ColorToHex[this.Color.ToLower()]);
            avatar.SaveHairColor(context.Player, chosenColor);

            BurnCaloriesNow(context.Player);
            UseDurability(DurabilityRate, context.Player);

            // Remove durability from the Hair Dye
            var hairDyeItem = hairDye as RepairableItem;
            hairDyeItem.Durability -= 1;
            if (hairDyeItem.Durability == 0)
                inventory.RemoveItem(hairDye.GetType());
            return InteractResult.SuccessLoc($"You've succesfully dyed your hair {Color}.");
        }

        public override string OnUsed(Player player, ItemStack itemStack)
        {
            player.PopupTypePicker(Localizer.DoStr("Pick A Color!"), typeof(HairColorBrushToolItem), material => SetMaterial(material, player, this));
            return String.Empty;
        }

        private void SetMaterial(List<Type> result, Player player, Item heldHairColorBrush)
        {
            var tool = Get(result[0]) as RepairableItem;

            player.User.Inventory.ToolbarBackpack.AddItem(tool);
            tool.Durability = Durability;
            player.User.Inventory.ToolbarBackpack.TryRemoveItem(heldHairColorBrush.GetType());

            player.MsgLocStr($"{Get(result[0]).DisplayName} Selected");
        }
    }

    [Serialized]
    [LocDisplayName("Hair Color Brush")]
    [Category("Tool")]
    [MaxStackSize(1)]
    [Ecopedia("Items", "Tools", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]

    public partial class HairColorBrushItem : HairColorBrushToolItem
    {
        public override string Color => "";
        public override InteractResult OnActLeft(InteractionContext context) => InteractResult.NoOp;
    }

    [RequiresSkill(typeof(PaintingSkill), 1)]
    
    public partial class AHairColorBrushRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(AHairColorBrushRecipe).Name,
            Assembly = typeof(AHairColorBrushRecipe).AssemblyQualifiedName,
            HiddenName = "Hair Color Brush",
            LocalizableName = Localizer.DoStr("Hair Color Brush"),
            IngredientList = new()
            {
                new EMIngredient("Wood", true, 2),
                new EMIngredient("FurPeltItem", false, 2),
            },
            ProductList = new()
            {
                new EMCraftable("HairColorBrushItem", 1),
            },
            BaseExperienceOnCraft = 1,
            BaseLabor = 25,
            LaborIsStatic = false,
            BaseCraftTime = .5f,
            CraftTimeIsStatic = false,
            CraftingStation = "ArtStationItem",
            RequiredSkillType = typeof(PaintingSkill),
            RequiredSkillLevel = 1,
            IngredientImprovementTalents = typeof(PaintingLavishResourcesTalent),
            SpeedImprovementTalents = new Type[] { typeof(PaintingParallelSpeedTalent), typeof(PaintingFocusedSpeedTalent) },
        };

        static AHairColorBrushRecipe() => EMRecipeResolver.AddDefaults(Defaults);

        public AHairColorBrushRecipe()
        {
            Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }
}
