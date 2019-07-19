using System;
using SQLite;
using XF.Base.Model;

namespace StoreHouse.Models
{
    public class StorePlace : Bindable
    {
        public StorePlace()
        {

        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int VerticalPosition { get; set; }
        public int HorizontalPosition { get; set; }
        public int? CurrentlyLockedBy { get; set; }

        public override string ToString()
        {
            return $"{Name}, V:{VerticalPosition}, H:{HorizontalPosition}";
        }
    }
}
