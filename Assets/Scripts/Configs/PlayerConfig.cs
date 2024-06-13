using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu()]
    public class PlayerConfig : ScriptableObject
    {
        public float MoveSpeed;
        public int MaxItems;
    }
}