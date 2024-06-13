using System.Collections.Generic;
using Client.Components;
using Client.Configs;
using Client.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData;
        private readonly PlayerConfig _playerConfig;

        public PlayerInitSystem(SceneData sceneData, PlayerConfig config)
        {
            _sceneData = sceneData;
            _playerConfig = config;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            int playerEntity = world.NewEntity();

            ref MoveComponent move = ref world.GetPool<MoveComponent>().Add(playerEntity);
            move.Rigidbody = _sceneData.Player.GetComponent<Rigidbody>();
            move.Animator = _sceneData.Player.GetComponent<Animator>();
            move.Transform = _sceneData.Player.GetComponent<Transform>();
            move.MoveSpeed = _playerConfig.MoveSpeed;

            ref PlayerComponent player = ref world.GetPool<PlayerComponent>().Add(playerEntity);
            player.Items = new Stack<ItemView>(_playerConfig.MaxItems);
            player.MaxItems = _playerConfig.MaxItems;
            player.PlayerView = _sceneData.Player;
            player.CurrentItemId = -1;
        }
    }
}