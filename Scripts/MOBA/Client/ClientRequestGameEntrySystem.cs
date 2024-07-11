﻿using MOBA.Common.Contracts;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;

namespace MOBA.Client
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation)]
    public partial struct ClientRequestGameEntrySystem : ISystem
    {
        private EntityQuery _pendingNetworkIdQuery;
        
        public void OnCreate(ref SystemState state)
        {
            var builder = new EntityQueryBuilder(Allocator.Temp).WithAll<NetworkId>().WithNone<NetworkStreamInGame>();
            _pendingNetworkIdQuery = state.GetEntityQuery(builder);
            state.RequireForUpdate(_pendingNetworkIdQuery);
            state.RequireForUpdate<ClientTeamRequest>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var requestedTeam = SystemAPI.GetSingleton<ClientTeamRequest>().Value;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var pendingNetworkIds = _pendingNetworkIdQuery.ToEntityArray(Allocator.Temp);
            
            foreach (var pendingNetworkId in pendingNetworkIds)
            {
                ecb.AddComponent<NetworkStreamInGame>(pendingNetworkId);
                var requestEntity = ecb.CreateEntity();
                ecb.AddComponent(requestEntity, new MobaTeamRequest { Value = requestedTeam });
                ecb.AddComponent(requestEntity, new SendRpcCommandRequest { TargetConnection = pendingNetworkId });
            }
            ecb.Playback(state.EntityManager);
        }
    }
}