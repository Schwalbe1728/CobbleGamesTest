using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Map.Abstraction
{
    [CreateAssetMenu(fileName = "MapGridConfig", menuName = "Map Grid Configuration")]
    public class MapGridConfiguration : ScriptableObject
    {
        [SerializeField]
        [Min(1)]
        private int _gridWidthInCells = 10;
        
        [SerializeField]
        [Min(1)]
        private int _gridHeightInCells = 10;

        public int GridWidth { get { return _gridWidthInCells; } }
        public int GridHeight { get { return _gridHeightInCells; } }
    }
}