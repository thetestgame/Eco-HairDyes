// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

#nullable enable
namespace Eco.HairDye.Server.Utils
{
    using Eco.Gameplay.Items;
    using System.Linq;

    /// <summary>
    /// Static extension methods for the Eco <see cref="Inventory"/> object.
    /// </summary>
    public static class InventoryExtensions
    {
        public static Item? FindInInventory(this Inventory inventory, Item searchItem)
        {
            var query = inventory.AllInventories
                .AllStacks()
                .Where(stack => stack.Item.ItemsEqual(searchItem));
            return query.Any() ? query.First().Item : null;
        }
    }
}
