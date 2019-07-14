using System;
using XF.Base.Model;

namespace StoreHouse.Models
{
    public class StorePlace : Bindable
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int VerticalPosition { get; set; }
        public int HorizontalPosition { get; set; }

        public override string ToString()
        {
            return $"{Name}, V:{VerticalPosition}, H:{HorizontalPosition}";
        }
    }
}
