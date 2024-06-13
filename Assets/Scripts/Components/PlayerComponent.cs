using System.Collections.Generic;
using Client.Views;

namespace Client.Components
{
    public struct PlayerComponent
    {
        public Stack<ItemView> Items;
        public PlayerView PlayerView;
        public int MaxItems;
        public int CurrentItemId;

        public bool IsFilled => Items.Count >= MaxItems;
    }
}