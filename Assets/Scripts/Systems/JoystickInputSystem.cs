using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class JoystickInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<MoveComponent> _pool;

        public JoystickInputSystem(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<MoveComponent>().End();
            _pool = _world.GetPool<MoveComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var moveComponent = ref _pool.Get(entity);

                moveComponent.Direction = new Vector3(_sceneData.Joystick.Direction.x, 0f, _sceneData.Joystick.Direction.y);
            }
        }
    }
}