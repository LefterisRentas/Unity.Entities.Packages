using MOBA.Common;
using MOBA.Common.Components;
using MOBA.Common.Contracts;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;

namespace MOBA.Client
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation)]
    public partial struct InitializeLocalChampSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NetworkId>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (transform, entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<GhostOwnerIsLocal>().WithNone<OwnerChampTag>().WithEntityAccess())
            {
                ecb.AddComponent<OwnerChampTag>(entity);
                ecb.SetComponent(entity, new ChampMoveVector{ Value = transform.ValueRO.Position });
            }
            ecb.Playback(state.EntityManager);
        }
    }
}