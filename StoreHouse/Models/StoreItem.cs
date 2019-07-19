using System;
using SQLite;
using XF.Base.Model;

namespace StoreHouse.Models
{
    public class StoreItem: Bindable
    {
        public StoreItem()
        {

        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? AssignDate { get; set; }
       // public Volume Volume { get; set; }
        public Guid CodeId { get; set; }
        public int PlaceId { get; set; }
        public string Type { get; set; }
    }
}
