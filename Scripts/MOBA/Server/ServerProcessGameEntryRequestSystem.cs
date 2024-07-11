using MOBA.Common;
using MOBA.Common.Contracts;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace MOBA.Server
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    public partial struct ServerProcessGameEntryRequestSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var builder = new EntityQueryBuilder(Allocator.Temp).WithAll<MobaTeamRequest, ReceiveRpcCommandRequest>();
            state.RequireForUpdate(state.GetEntityQuery(builder));
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (teamRequest, requestSource, requestEntity) in SystemAPI.Query<MobaTeamRequest, ReceiveRpcCommandRequest>().WithEntityAccess())
            {
                ecb.DestroyEntity(requestEntity);
                ecb.AddComponent<NetworkStreamInGame>(requestSource.SourceConnection);

                var requestTeamType = teamRequest.Value;
                
                if (requestTeamType == TeamType.AutoAssign)
                {
                    requestTeamType = TeamType.Blue;
                }

                var clientId = SystemAPI.GetComponent<NetworkId>(requestSource.SourceConnection).Value;
                
                Debug.unityLogger.LogError("Server: ",$"Assigning team {requestTeamType} to client {clientId}");
            }
            ecb.Playback(state.EntityManager);
        }
    }
}