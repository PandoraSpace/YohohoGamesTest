using Client.Components;
using Client.Configs;
using Client.Services;
using Client.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class ItemsGeneratorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ItemsGeneratorConfig _config;
        private readonly Prefabs _prefabs;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<ItemPlaceComponent> _placePool;
        private Timer _timer;

        public ItemsGeneratorSystem(ItemsGeneratorConfig config, Prefabs prefabs)
        {
            _config = config;
            _prefabs = prefabs;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ItemPlaceComponent>().End();
            _placePool = _world.GetPool<ItemPlaceComponent>();
            _timer = new Timer(_config.Time);
        }

        public void Run(IEcsSystems systems)
        {
            _timer.Timing(Time.deltaTime);
            
            if (_timer.IsTimeElapsed == false) return;
            
            foreach (var entity in _filter)
            {
                ref ItemPlaceComponent itemPlace = ref _placePool.Get(entity);
                
                if (itemPlace.IsFilled) continue;

                CreateNewItem(ref itemPlace);
            }
            
            _timer.ReleaseTimer();
        }

        private void CreateNewItem(ref ItemPlaceComponent itemPlaceComponent)
        {
            ItemView itemPrefab = _prefabs.GetItemPrefab(itemPlaceComponent.Id);
            Vector3 spawnPosition = itemPlaceComponent.Transform.position + Vector3.up * itemPlaceComponent.Height;

            ItemView itemGO = Object.Instantiate(itemPrefab, spawnPosition, Quaternion.identity, itemPlaceComponent.Transform);
            
            Vector3 newPosition = spawnPosition + Vector3.up * 
                (itemPlaceComponent.Items.Count * itemGO.Height + itemGO.Height * 0.5f);
            itemGO.transform.position = newPosition;
            
            itemPlaceComponent.Items.Push(itemGO);
        }
    }
}