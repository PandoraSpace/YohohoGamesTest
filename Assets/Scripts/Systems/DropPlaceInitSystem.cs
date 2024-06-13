using Client.Components;
using Leopotam.EcsLite;

namespace Client
{
    public class DropPlaceInitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData;
        private EcsWorld _world;

        public DropPlaceInitSystem(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            var dropPlaceEntity = _world.NewEntity();
            _sceneData.DropPlace.Init(_world, dropPlaceEntity);

            ref var dropPlaceComponent = ref _world.GetPool<DropPlaceComponent>().Add(dropPlaceEntity);
            dropPlaceComponent.Transform = _sceneData.DropPlace.transform;
        }
    }
}