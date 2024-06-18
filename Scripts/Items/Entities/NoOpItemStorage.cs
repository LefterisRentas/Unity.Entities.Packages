using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace LefterisRentas.Dev.Helpers.Items.Entities
{
    public class NoOpItemStorage : IItemStorage
    {
        public List<DbItem> DbItems {
            get
            {
                if (_dbItems == null)
                {
                    _dbItems = Items.ConvertAll(item => ScriptableObject.CreateInstance<DbItem>());
                }
                return _dbItems;
            }
            set => _dbItems = value;}

        private List<DbItem> _dbItems;
        
        public List<DbItem> Items { get; set; }
        
        public Task<bool> TryGetItem(FixedString32Bytes itemId, out DbItem dbItem)
        {
            dbItem = DbItems.Find(item => item.ItemId.Equals(itemId));
            return Task.FromResult(dbItem != null);
        }
    }
}