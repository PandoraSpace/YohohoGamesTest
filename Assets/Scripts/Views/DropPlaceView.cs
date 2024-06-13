using System;
using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Views
{
    public class DropPlaceView : MonoBehaviour
    {
        private EcsWorld _world;
        private int _entity;

        public void Init(EcsWorld ecsWorld, int entity)
        {
            _world = ecsWorld;
            _entity = entity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _world.GetPool<DropItemEvent>().Add(_entity);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _world.GetPool<DropItemEvent>().Del(_entity);
            }
        }
    }
}