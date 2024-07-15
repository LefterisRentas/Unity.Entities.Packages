using MOBA.Common.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace MOBA.Common
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct InitializeCharacterSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (physicsMass, mobaTeam, entity) in SystemAPI.Query<RefRW<PhysicsMass>, MobaTeam>().WithAny<NewChampTag>().WithEntityAccess())
            {
                physicsMass.ValueRW.InverseInertia[0] = 0;
                physicsMass.ValueRW.InverseInertia[1] = 0;
                physicsMass.ValueRW.InverseInertia[2] = 0;
                ecb.RemoveComponent<NewChampTag>(entity);
            }
            ecb.Playback(state.EntityManager);
        }
    }
}