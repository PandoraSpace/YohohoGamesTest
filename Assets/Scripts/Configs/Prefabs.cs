using System.Linq;
using Client.Views;
using UnityEngine;

namespace Client.Configs
{
    [CreateAssetMenu]
    public class Prefabs : ScriptableObject
    {
        public ItemView[] Items;

        public ItemView GetItemPrefab(int id)
        {
            return Items.FirstOrDefault(item => item.Id == id);
        }
    }
}