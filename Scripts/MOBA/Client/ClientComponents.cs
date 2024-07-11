using MOBA.Common;
using Unity.Entities;

namespace MOBA.Client
{
    public struct ClientTeamRequest : IComponentData
    {
        public TeamType Value;
    }
}