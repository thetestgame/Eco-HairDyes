namespace Eco.HairDye.Server.Items.Tools
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using System.ComponentModel;

    [Serialized]
    [LocDisplayName("White Hair Color Brush")]
    [MaxStackSize(1)]
    [Category("Tool")]
    public partial class WhiteHairColorBrushItem : HairColorBrushToolItem
    {
        public override string Color => "White";
    }
}