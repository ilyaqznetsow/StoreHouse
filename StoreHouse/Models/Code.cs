using System;
using XF.Base.Model;

namespace StoreHouse.Models
{
    public class Code:Bindable
    {
        public Guid? Id { get; set; }
        public object Picture { get; set; }
    }
}
