using MOBA.Common;
using MOBA.Common.Components;
using MOBA.Common.Contracts;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;

namespace MOBA.Server
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    public partial struct ServerProcessGameEntryRequestSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NetworkTime>();
            state.RequireForUpdate<MobaPrefabs>();
            var builder = new EntityQueryBuilder(Allocator.Temp).WithAll<MobaTeamRequest, ReceiveRpcCommandRequest>();
            state.RequireForUpdate(state.GetEntityQuery(builder));
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var championPrefab = SystemAPI.GetSingleton<MobaPrefabs>().Champion;
            foreach (var (teamRequest, requestSource, requestEntity) in SystemAPI.Query<MobaTeamRequest, ReceiveRpcCommandRequest>().WithEntityAccess())
            {
                ecb.DestroyEntity(requestEntity);
                ecb.AddComponent<NetworkStreamInGame>(requestSource.SourceConnection);

                var requestedTeamType = teamRequest.Value;
                
                if (requestedTeamType == TeamType.AutoAssign)
                {
                    requestedTeamType = TeamType.Blue;
                }

                var clientId = SystemAPI.GetComponent<NetworkId>(requestSource.SourceConnection).Value;
                
                Debug.Log($"Server: Assigning team {requestedTeamType} to client {clientId}");

                var createdChamp = ecb.Instantiate(championPrefab);
                ecb.SetName(createdChamp, "Champion " + clientId);
                var spawnPosition = new float3(11, 10, 28);
                var newTransform = LocalTransform.FromPosition(spawnPosition);
                ecb.SetComponent(createdChamp, newTransform);
                ecb.SetComponent(createdChamp, new GhostOwner{ NetworkId = clientId });
                ecb.SetComponent(createdChamp, new MobaTeam{ Value = requestedTeamType });
                
                ecb.AppendToBuffer(requestSource.SourceConnection, new LinkedEntityGroup{ Value = createdChamp });
            }
            ecb.Playback(state.EntityManager);
        }
    }
}