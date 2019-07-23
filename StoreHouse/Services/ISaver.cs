using System;
using System.Threading.Tasks;

namespace StoreHouse.Services
{
    public interface ISaver
    {
         Task<bool> Save(string content);
    }
}
