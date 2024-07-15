using Unity.Collections;
using Unity.Entities;

namespace LefterisRentas.Dev.Helpers.Items.Entities.Netcode
{
    public struct ItemPrefabs: IComponentData
    {
        public NativeHashMap<FixedString32Bytes, Entity> Item;
    }
}