using MOBA.Client;
using MOBA.Common;

namespace LefterisRentas.Dev.Helpers.Netcode.ConnectionManager
{
    public partial class ClientConnectionManager
    {
        private void StartClientWithHooks()
        {
            var clientWorld = StartClient();
            var team = _teamDropdown.value switch
            {
                0 => TeamType.AutoAssign,
                1 => TeamType.Blue,
                2 => TeamType.Red,
                _ => TeamType.None
            };
            var teamRequestEntity = clientWorld.EntityManager.CreateEntity();
            clientWorld.EntityManager.AddComponentData(teamRequestEntity, new ClientTeamRequest
            {
                Value = team
            });
        }

        private void StartServerWithHooks()
        {
            var serverWorld = StartServer();
        }
    }
}