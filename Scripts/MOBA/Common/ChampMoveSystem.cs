using MOBA.Common.Components;
using MOBA.Common.Contracts;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;

namespace MOBA.Common
{
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    public partial struct ChampMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, moveVector, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<ChampMoveVector>, RefRO<CharacterMoveSpeed>>().WithAll<Simulate>())
            {
                var moveTarget = moveVector.ValueRO.Value;
                var moveDirection = moveTarget - transform.ValueRW.Position;
                
                if (math.length(moveDirection) > 0.01f)
                {
                    var moveDelta = math.normalize(moveDirection) * moveSpeed.ValueRO.Value * deltaTime;
                    transform.ValueRW.Position += moveDelta;
                    transform.ValueRW.Rotation = quaternion.LookRotationSafe(moveDelta, math.up());
                }
            }
        }
    }
}