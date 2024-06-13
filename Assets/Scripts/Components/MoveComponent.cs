using UnityEngine;

namespace Client.Components
{
    public struct MoveComponent
    {
        public Rigidbody Rigidbody;
        public Transform Transform;
        public Animator Animator;
        public float MoveSpeed;
        public Vector3 Direction;
    }
}