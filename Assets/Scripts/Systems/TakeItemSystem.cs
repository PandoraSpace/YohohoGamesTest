using Client.Components;
using Client.Services;
using Client.Views;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class TakeItemSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _itemPlaceFilter;
        private EcsPool<ItemPlaceComponent> _pool;
        private Timer _timer;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<PlayerComponent>().End();
            _itemPlaceFilter = _world.Filter<ItemPlaceComponent>().Inc<TakeItemEvent>().End();
            _pool = _world.GetPool<ItemPlaceComponent>();
            _timer = new Timer(0.2f);
        }

        public void Run(IEcsSystems systems)
        {
            _timer.Timing(Time.deltaTime);
            
            if (_timer.IsTimeElapsed == false) return;
            
            ref var playerComponent = ref GetPlayerComponent();
            
            foreach (var placeEntity in _itemPlaceFilter)
            {
                ref var placeComponent = ref _pool.Get(placeEntity);
                
                if (placeComponent.Items.Count == 0 || playerComponent.IsFilled) return;
                if (playerComponent.CurrentItemId != placeComponent.Id && playerComponent.Items.Count > 0) return;

                ItemView item = placeComponent.Items.Pop();
                playerComponent.Items.Push(item);
                
                SetNewPosition(ref playerComponent, item);
                playerComponent.CurrentItemId = item.Id;
            }

            _timer.ReleaseTimer();
        }

        private void SetNewPosition(ref PlayerComponent playerComponent, ItemView item)
        {
             Vector3 newPosition = playerComponent.PlayerView.ItemsPoint.localPosition +
                                   Vector3.up * (playerComponent.Items.Count * item.Height);
            
            item.transform.SetParent(playerComponent.PlayerView.transform);
            item.transform.DOLocalMove(newPosition, 0.1f);
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