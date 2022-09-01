// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.HairDye.Server.Utils
{
    using Eco.Gameplay.Players;
    using Eco.Shared.Utils;

    /// <summary>
    /// Static extension methods for Eco's <see cref="Avatar"/> object.
    /// </summary>
    internal static class AvatarExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="avatar"></param>
        /// <param name="player"></param>
        /// <param name="hairColor"></param>
        public static void SaveHairColor(this Avatar avatar, Player player, Color hairColor) =>
            avatar.SaveChanges(
                player: player, 
                skinColor: avatar.SkinColor, 
                skinShadePercent: avatar.SkinShadePercent,
                skinColorPercent: avatar.SkinColorPercent,
                face: avatar.Face,
                sex: avatar.Sex,
                stance: avatar.Stance,
                hairColor: hairColor);
    }
}
