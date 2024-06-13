using System.Collections.Generic;
using Client.Components;
using Client.Configs;
using Client.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class ItemPlaceInitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData;
        private readonly ItemsGeneratorConfig _config;
        private EcsWorld _world;

        public ItemPlaceInitSystem(SceneData sceneData, ItemsGeneratorConfig config)
        {
            _sceneData = sceneData;
            _config = config;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            var pool = _world.GetPool<ItemPlaceComponent>();
            int id = 0;
            
            foreach (GeneratePlaceView place in _sceneData.PlacesItem)
            {
                var placeEntity = _world.NewEntity();
                place.Init(_world, placeEntity);
                
                ref var itemPlaceComponent = ref pool.Add(placeEntity);
                itemPlaceComponent.Transform = place.transform;
                itemPlaceComponent.Id = id;
                itemPlaceComponent.MaxItems = _config.MaxGenerateItem;
                itemPlaceComponent.Height = place.GetComponent<BoxCollider>().size.y;
                itemPlaceComponent.Items = new Stack<ItemView>(_config.MaxGenerateItem);

                id++;
            }
        }
    }
}