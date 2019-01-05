﻿using System;

namespace Ozh.Tools.Functional {
    using static F;

    public static class Date {
        public static Option<DateTime> Parse(string s) {
            DateTime d;
            return DateTime.TryParse(s, out d) ? Some(d) : None;
        }
    }
}
