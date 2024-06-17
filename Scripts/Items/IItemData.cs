using Unity.Collections;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace LefterisRentas.Dev.Helpers.Items
{
    public interface IItemData : IComponentData
    {
        #region Item General Info

        public int ItemId { get; set; }

        public FixedString64Bytes ItemName { get; set; }
        
        public FixedString512Bytes ItemDescription { get; set; }

        #endregion

        #region Item General Values

        public float ItemValue { get; set; }

        public float ItemWeight { get; set; }

        #endregion

        #region Item Quantity Info Values
        
        public float MaxQuantity { get; set; }
        
        #endregion

        #region Item Bools

        public bool IsTradeable { get; set; }

        public bool IsEquipable { get; set; }

        public bool IsPickable { get; set; }

        public bool IsDropable { get; set; }

        #endregion
    }
}