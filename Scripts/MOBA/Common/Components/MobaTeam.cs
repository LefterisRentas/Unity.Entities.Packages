using Unity.Entities;
using Unity.NetCode;

namespace MOBA.Common.Components
{
    public struct MobaTeam : IComponentData
    {
        [GhostField] public TeamType Value;
    }
}