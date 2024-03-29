﻿using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Categories
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Wheel> Wheels { get; set; }
    }
}
