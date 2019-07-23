using System;
using System.Threading.Tasks;

namespace StoreHouse.Services
{
    public interface ISaver
    {
         bool Save(string content);
    }
}
