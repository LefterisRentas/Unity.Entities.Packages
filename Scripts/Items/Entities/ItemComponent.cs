using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace LefterisRentas.Dev.Helpers.Items.Entities
{
    public struct ItemComponent : IItemData
    {
        public FixedString32Bytes ItemId { get; set; }
        public FixedString64Bytes ItemName { get; set; }
        public FixedString512Bytes ItemDescription { get; set; }
        public float ItemValue { get; set; }
        public float ItemWeight { get; set; }
        public float MaxQuantity { get; set; }
        public bool IsTradeable { get; set; }
        public bool IsEquipable { get; set; }
        public bool IsPickable { get; set; }
        public bool IsDropable { get; set; }
        
        public FixedString32Bytes ItemPrefabId { get; set; }
        
        public Entity ItemEntity { get; set; }

        public bool CanPickUp()
        {
            return IsPickable;
        }
        
        public static ItemComponent FromDbItem(DbItem dbItem)
        {
            return new ItemComponent
            {
                ItemId = dbItem.ItemId,
                ItemName = dbItem.ItemName,
                ItemDescription = dbItem.ItemDescription,
                ItemValue = dbItem.ItemValue,
                ItemWeight = dbItem.ItemWeight,
                MaxQuantity = dbItem.MaxQuantity,
                IsTradeable = dbItem.IsTradeable,
                IsEquipable = dbItem.IsEquipable,
                IsPickable = dbItem.IsPickable,
                IsDropable = dbItem.IsDropable,
                ItemPrefabId = dbItem.ItemPrefabId
            };
        }
    }
}