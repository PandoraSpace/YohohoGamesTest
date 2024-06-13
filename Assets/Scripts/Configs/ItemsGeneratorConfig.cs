using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu]
    public class ItemsGeneratorConfig : ScriptableObject
    {
        public float Time;
        [Min(1)] public int MaxGenerateItem;
    }
}