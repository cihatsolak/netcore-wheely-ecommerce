﻿using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Colors
{
    public class Color : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }

        public virtual ICollection<Wheel> Wheels { get; set; }
    }
}
