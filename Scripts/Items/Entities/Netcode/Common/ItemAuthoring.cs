using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace LefterisRentas.Dev.Helpers.Items.Entities.Netcode.Common
{
    public class ItemAuthoring : MonoBehaviour
    {
        public IItemStorage ItemStorage;
        public Dictionary<FixedString32Bytes, GameObject> ItemPrefabs;
        public string ItemId;
        public readonly ILogger Logger; 
        
        public ItemAuthoring(ILogger logger, Dictionary<FixedString32Bytes, GameObject> itemPrefabs)
        {
            Logger = logger;
            ItemPrefabs = itemPrefabs;
        }

        public class ItemBaker : Baker<ItemAuthoring>
        {
            
            public override void Bake(ItemAuthoring authoring)
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var entity = entityManager.CreateEntity();
                var itemId = new FixedString32Bytes(authoring.ItemId);
                
                if (authoring.ItemStorage.TryGetItem(itemId, out var dbItem).Result)
                {
                    var item = ItemComponent.FromDbItem(dbItem);
                    item.ItemEntity = GetEntity(authoring.ItemPrefabs[itemId], TransformUsageFlags.Dynamic);
                    AddComponent(entity, item);
                }
                else
                {
                    authoring.Logger.LogError(nameof(ItemBaker), $"Item with ID {itemId} not found in storage.");
                }
            }
        }
    }
}