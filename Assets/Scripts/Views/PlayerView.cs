using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _itemsPoint;

        public Transform ItemsPoint => _itemsPoint;
    }
}