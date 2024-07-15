using Unity.NetCode;

namespace MOBA.Common.Contracts
{
    public struct MobaTeamRequest : IRpcCommand
    {
        public TeamType Value;
    }
}