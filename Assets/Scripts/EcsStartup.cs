using Leopotam.EcsLite;
using UnityEngine;

namespace Client 
{ 
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private Configurations _configs;
        private EcsWorld _world;        
        private IEcsSystems _systems;
        private IEcsSystems _systemsFixed;

        private void Start ()
        {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systemsFixed = new EcsSystems (_world);
            
            _systems
                .Add(new PlayerInitSystem(_sceneData, _configs.Player))
                .Add(new ItemPlaceInitSystem(_sceneData, _configs.ItemsGeneratorConfig))
                .Add(new DropPlaceInitSystem(_sceneData))
                .Add(new JoystickInputSystem(_sceneData))
                .Add(new ItemsGeneratorSystem(_configs.ItemsGeneratorConfig, _configs.Prefabs))
                .Add(new TakeItemSystem())
                .Add(new DropItemSystem())
                .Add(new HUDUpdateSystem(_sceneData))
#if UNITY_EDITOR
                //.Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Init ();
            
            _systemsFixed
                .Add(new MoveSystem())
                .Init();
        }

        private void Update () 
        {
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            _systemsFixed?.Run();
        }

        private void OnDestroy () 
        {
            if (_systems != null) 
            {
                _systems.Destroy ();
                _systems = null;
            }
            
            if (_systemsFixed != null) 
            {
                _systemsFixed.Destroy ();
                _systemsFixed = null;
            }
            
            if (_world != null) 
            {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}