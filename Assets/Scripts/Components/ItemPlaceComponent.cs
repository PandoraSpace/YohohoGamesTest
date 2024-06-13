using System.Collections.Generic;
using Client.Views;
using UnityEngine;

namespace Client.Components
{
    public struct ItemPlaceComponent
    {
        public Stack<ItemView> Items;
        public Transform Transform;
        public int MaxItems;
        public int Id;
        public float Height;

        public bool IsFilled => Items.Count >= MaxItems;
    }
}