using Unity.Entities;
using UnityEngine;

namespace MOBA.Common
{
    public class MobaPrefabsAuthoring : MonoBehaviour
    {
        public GameObject Champion;
        
        public class MobaPrefabsBaker : Baker<MobaPrefabsAuthoring>
        {
            public override void Bake(MobaPrefabsAuthoring authoring)
            {
                var prefabContainerEntity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(prefabContainerEntity, new MobaPrefabs
                {
                    Champion = GetEntity(authoring.Champion, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}