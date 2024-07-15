using MOBA.Common.Components;
using MOBA.Common.Contracts;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MOBA.Client
{
    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial class ChampMoveInputSystem : SystemBase
    {
        private MobaInputActions _inputActions;
        private CollisionFilter _selectionFilter;

        protected override void OnCreate()
        {
            _inputActions = new MobaInputActions();
            _selectionFilter = new CollisionFilter
            {
                BelongsTo = 1 << 5,
                CollidesWith = 1 << 0
            };
            RequireForUpdate<OwnerChampTag>();
        }

        protected override void OnStartRunning()
        {
            _inputActions.Enable();
            _inputActions.Gameplay.SelectMovePosition.performed += OnMove;
        }

        protected override void OnUpdate()
        {
        }
        
        private void OnMove(InputAction.CallbackContext context)
        {
            var collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;
            var champEntity = SystemAPI.GetSingletonEntity<OwnerChampTag>();
            var position = context.ReadValue<Vector2>();
            EntityManager.SetComponentData(champEntity, new ChampMoveVector
            {
                Value = new float3(position.x, 0, position.y)
            });
        }
        
        protected override void OnStopRunning()
        {
            _inputActions.Disable();
            _inputActions.Gameplay.SelectMovePosition.performed -= OnMove;
        }
        
        protected override void OnDestroy()
        {
            _inputActions.Dispose();
        }
    }
}