using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client 
{
    public sealed class MoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsPool<MoveComponent> _pool;
        private EcsFilter _filter;

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
                ref MoveComponent move = ref _pool.Get(entity);

                if (move.Direction != Vector3.zero)
                {
                    Move(move);
                    Rotate(move);
                    Animation(move, 1f);
                }
                else
                {
                    Animation(move, 0f);
                }
            }
        }

        private void Move(MoveComponent move)
        {
            Vector3 offset = move.Direction * (move.MoveSpeed * Time.fixedDeltaTime);
            move.Rigidbody.MovePosition(move.Rigidbody.position + offset);
        }

        private void Rotate(MoveComponent move)
        {
            if (Vector3.Angle(move.Transform.forward, move.Direction) > 0)
            {
                Vector3 newDirection =
                    Vector3.RotateTowards(move.Transform.forward, move.Direction, 5f, 0);
                
                move.Transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }

        private void Animation(MoveComponent move, float speed)
        {
            move.Animator.SetFloat("Speed_f", speed);
        }
    }
}