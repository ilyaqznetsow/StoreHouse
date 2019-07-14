using System;
using XF.Base.Model;

namespace StoreHouse.Models
{
    public class StoreItem: Bindable
    {
        public Guid? Id  => Guid.NewGuid();
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? AssignDate { get; set; }
        public Volume Volume { get; set; }
        public Guid? CodeId { get; set; }
        public StorePlace Place { get; set; }
        public string Type { get; set; }
    }
}
