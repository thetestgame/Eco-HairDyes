﻿// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.HairDye.Server.Items.Dyes
{
    using Eco.Core.Items;
    using Eco.EM.Artistry;
    using Eco.EM.Framework.Resolvers;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using System;

    [Serialized]
    [Currency]
    [MaxStackSize(100)]
    [LocDisplayName("Dark Red Hair Dye")]
    [Ecopedia("Items", "Hair Dyes", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]

    public partial class DarkRedHairDyeItem : RepairableItem
    {
        public override LocString DisplayDescription => Localizer.DoStr("Dark Red hair dye used for dying hair with a Hair Coloring Brush.");
        public new float DurabilityMax => 100;
        public override IDynamicValue SkilledRepairCost => new ConstantValue(0);
    }

    
    [RequiresSkill(typeof(PaintingSkill), 4)]
    
    public partial class DarkRedHairDyeRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(DarkRedHairDyeRecipe).Name,
            Assembly = typeof(DarkRedHairDyeRecipe).AssemblyQualifiedName,
            HiddenName = "Hair Dye - Dark Red",
            LocalizableName = Localizer.DoStr("Hair Dye - Dark Red"),
            IngredientList = new()
            {
                new EMIngredient("RedDyeItem", false, 1, true),
                new EMIngredient("BlackDyeItem", false, 1, true),
                new EMIngredient("PaintBaseItem", false, 2, true),
            },
            ProductList = new()
            {
                new EMCraftable("DarkRedHairDyeItem", 1),
            },
            BaseExperienceOnCraft = 1,
            BaseLabor = 250,
            LaborIsStatic = false,
            BaseCraftTime = 2,
            CraftTimeIsStatic = false,
            CraftingStation = "DyeTableItem",
            RequiredSkillType = typeof(PaintingSkill),
            RequiredSkillLevel = 4,
            IngredientImprovementTalents = typeof(PaintingLavishResourcesTalent),
            SpeedImprovementTalents = new Type[] { typeof(PaintingFocusedSpeedTalent), typeof(PaintingParallelSpeedTalent) },
        };

        static DarkRedHairDyeRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public DarkRedHairDyeRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }
}
