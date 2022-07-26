﻿// Copyright (c) Jordan Maxwell. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.HairDye.Server.Items
{
    using System.Collections.Generic;

    public static class Constants
    {
        public static Dictionary<string, string> ColorToHex = new Dictionary<string, string>
        {
            { "black", "000000" },
            { "blue", "2E5984" },
            { "brown", "964B00" },
            { "dark red", "8b0000" },
            { "green", "00FF00" },
            { "grey", "808080" },
            { "orange", "FFA500" },
            { "pink", "FFC0CB" },
            { "purple", "855cb3" },
            { "red", "ff0000" },
            { "white", "ffffff" },
            { "yellow", "FFFF00" }
        };
    }
}
