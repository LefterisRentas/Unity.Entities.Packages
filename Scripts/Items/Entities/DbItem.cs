using Unity.Collections;
using UnityEngine;

namespace LefterisRentas.Dev.Helpers.Items.Entities
{
    public class DbItem : ScriptableObject, IItemData
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
    }
}