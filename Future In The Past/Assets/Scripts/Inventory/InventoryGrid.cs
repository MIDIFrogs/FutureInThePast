using System;
using UnityEngine;

namespace SibGameJam.Inventory
{
    [Serializable]
    public struct InventoryGrid
    {
        [SerializeField, Min(1)] private int width;
        [SerializeField, Min(1)] private int height;

        public readonly int Width => width;
        public readonly int Height => height;

        public InventoryGrid(int _width, int _height)
        {
            width = _width; 
            height = _height;
        }
    }
}