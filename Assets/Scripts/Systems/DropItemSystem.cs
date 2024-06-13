using Client.Components;
using Client.Services;
using Client.Views;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class DropItemSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _dropPlaceFilter;
        private EcsFilter _playerFilter;
        private Timer _timer;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _dropPlaceFilter = _world.Filter<DropPlaceComponent>().Inc<DropItemEvent>().End();
            _playerFilter = _world.Filter<PlayerComponent>().End();
            _timer = new Timer(0.2f);
        }

        public void Run(IEcsSystems systems)
        {
            _timer.Timing(Time.deltaTime);
            
            if (_timer.IsTimeElapsed == false) return;
            
            ref var playerComponent = ref GetPlayerComponent();
            
            if (playerComponent.Items.Count == 0) return;
            
            foreach (var dropPlaceEntity in _dropPlaceFilter)
            {
                ref var dropPlaceComponent = ref _world.GetPool<DropPlaceComponent>().Get(dropPlaceEntity);
                
                ItemView item = playerComponent.Items.Pop();
                item.transform.DOMove(dropPlaceComponent.Transform.position, 0.1f)
                    .OnComplete(() => Object.Destroy(item.gameObject));
            }
            
            _timer.ReleaseTimer();
        }
        
        private ref PlayerComponent GetPlayerComponent()
        {
            int playerEntity = 0;
            
            foreach (var player in _playerFilter)
                playerEntity = player;

            return ref _world.GetPool<PlayerComponent>().Get(playerEntity);
        }
    }
}