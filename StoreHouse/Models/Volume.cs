﻿using System;
using XF.Base.Model;

namespace StoreHouse.Models
{
    public class Volume: Bindable
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }

        public override string ToString()
        {
            return $"{Width}x{Height}x{Length}";
        }
    }
}