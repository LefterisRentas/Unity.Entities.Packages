using MOBA.Common.Contracts;
using Unity.Entities;
using UnityEngine;

namespace MOBA.Common.Components
{
    public class ChampAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public class ChampBaker : Baker<ChampAuthoring>
        {
            public override void Bake(ChampAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ChampTag>(entity);
                AddComponent<NewChampTag>(entity);
                AddComponent<MobaTeam>(entity);
                AddComponent<ChampMoveVector>(entity);
                AddComponent(entity, new CharacterMoveSpeed { Value = authoring.MoveSpeed });
            }
        }
    }
}