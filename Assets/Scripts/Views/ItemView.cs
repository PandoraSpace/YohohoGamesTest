using UnityEngine;

namespace Client.Views
{
    [RequireComponent(typeof(BoxCollider))]
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _icon;

        public int Id => _id;
        public float Height { get; private set; }
        public Sprite Icon => _icon;

        private void Awake()
        {
            Height = GetComponent<BoxCollider>().size.y;
        }
    }
}