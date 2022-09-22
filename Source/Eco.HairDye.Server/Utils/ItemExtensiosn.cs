// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.HairDye.Server.Utils
{
    using Eco.Gameplay.Items;

    /// <summary>Static extension methods for the Eco <see cref="Item"/> object.</summary>
    public static class ItemExtensions
    {
        public static bool ItemsEqual(this Item x, Item y) => x.DisplayName.NotTranslated.Equals(y.DisplayName.NotTranslated);
    }
}
