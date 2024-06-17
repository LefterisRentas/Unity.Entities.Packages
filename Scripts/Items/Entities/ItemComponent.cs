using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;

namespace LefterisRentas.Dev.Helpers.Items.Entities
{
    public struct ItemComponent : IItemData
    {
        #region Interface members
        
        public int ItemId { get; set; }
        public FixedString64Bytes ItemName { get; set; }
        public FixedString512Bytes ItemDescription { get; set; }
        public float ItemValue { get; set; }
        public float ItemWeight { get; set; }
        public float MaxQuantity { get; set; }
        public bool IsTradeable { get; set; }
        public bool IsEquipable { get; set; }
        public bool IsPickable { get; set; }
        public bool IsDropable { get; set; }
        
        #endregion
        
        public Entity ItemPrefab { get; set; }
    }
}