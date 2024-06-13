using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class HUDUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData;
        
        private EcsWorld _world;
        private EcsFilter _takeEventFilter;
        private EcsFilter _dropEventFilter;
        private EcsFilter _playerFilter;
        private EcsPool<PlayerComponent> _playerPool;

        public HUDUpdateSystem(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _takeEventFilter = _world.Filter<TakeItemEvent>().End();
            _dropEventFilter = _world.Filter<DropItemEvent>().End();
            _playerFilter = _world.Filter<PlayerComponent>().End();
            _playerPool = _world.GetPool<PlayerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var takeEntity in _takeEventFilter)
                UpdateInfo();

            foreach (var dropEntity in _dropEventFilter)
                UpdateInfo();
        }

        private void UpdateInfo()
        {
            foreach (var playerEntity in _playerFilter)
            {
                ref var playerComponent = ref _playerPool.Get(playerEntity);

                Sprite icon = null;

                if (playerComponent.Items.Count != 0)
                    icon = playerComponent.Items.Peek().Icon;
                
                _sceneData.ItemIcon.sprite = icon;
                _sceneData.ItemAmount.text = playerComponent.Items.Count.ToString();
            }
        }
    }
}