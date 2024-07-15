using Unity.Mathematics;
using Unity.NetCode;

namespace MOBA.Common.Contracts
{
    [GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
    public struct ChampMoveVector : IInputComponentData
    {
        [GhostField(Quantization = 0)] public float3 Value;
    }
}