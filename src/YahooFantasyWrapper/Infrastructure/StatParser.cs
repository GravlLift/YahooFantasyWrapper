﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YahooFantasyWrapper.Infrastructure
{
    public static class StatParser
    {
        public static float? Parse(string value)
        {
            if (value == "-") return null;
            if (value.Contains(":"))
            {
                //minutes:seconds will return as minutes
                value = value.Replace(",", "");
                var split = value.Split(':');
                return float.Parse(split[0]) + (float.Parse(split[1]) / 60);
            }

            return float.Parse(value);
        }
    }
}
