using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;

namespace LefterisRentas.Dev.Helpers.Items.Entities
{
    public interface IItemStorage
    {
        public List<DbItem> DbItems { get; set; }
        
        Task<bool> TryGetItem(FixedString32Bytes itemId, out DbItem dbItem);
    }
}