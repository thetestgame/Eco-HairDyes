﻿// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.HairDye.Server.Items.Tools
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using System.ComponentModel;

    [Serialized]
    [LocDisplayName("Brown Hair Color Brush")]
    [MaxStackSize(1)]
    [Category("Tool")]
    public partial class BrownHairColorBrushItem : HairColorBrushToolItem
    {
        public override string Color => "Brown";
    }
}