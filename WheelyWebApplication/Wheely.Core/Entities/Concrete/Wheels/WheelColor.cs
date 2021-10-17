﻿using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Colors;

namespace Wheely.Core.Entities.Concrete.Wheels
{
    public class WheelColor : IEntity
    {
        public int WheelId { get; set; }
        public Wheel Wheel { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}