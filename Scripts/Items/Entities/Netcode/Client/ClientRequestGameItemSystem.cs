using LefterisRentas.Dev.Helpers.Items.Entities.Netcode.Common.Contracts;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;

namespace LefterisRentas.Dev.Helpers.Items.Entities.Netcode.Client
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation)]
    public partial struct ClientRequestGameItemSystem : ISystem
    {
        private EntityQuery _pendingNetworkItemQuery;
        
        public void OnCreate(ref SystemState state)
        {
            var builder = new EntityQueryBuilder(Allocator.Temp).WithAll<NetworkId>().WithNone<NetworkStreamInGame>();
            _pendingNetworkItemQuery = state.GetEntityQuery(builder);
            state.RequireForUpdate(_pendingNetworkItemQuery);
            state.RequireForUpdate<ClientItemRequest>();
            state.RequireForUpdate<ItemPrefabs>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var requestedTeam = SystemAPI.GetSingleton<ClientItemRequest>().ItemId;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var pendingNetworkItemsQuery = _pendingNetworkItemQuery.ToEntityArray(Allocator.Temp);

            ecb.Playback(state.EntityManager);
        }
    }
}