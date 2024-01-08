using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding
{
    [CreateAssetMenu(fileName = "HeuristicConfiguration", menuName = "Scriptable Objects/Heuristic Configuration")]
    public class HeuristicConfigurationInt : ScriptableObject
    {
        [SerializeField]
        [Min(1f)]
        private int _adjacentCellCost = 10;

        [SerializeField]
        [Min(1f)]
        private int _diagonalCellCost = 14;

        [SerializeField]
        private bool _diagonalTraversalAllowed = true;

        public int AdjacentCellCost { get { return _adjacentCellCost; } }
        public int DiagonalCellCost { get { return _diagonalCellCost; } }
        public bool DiagonalTraversalAllowed { get { return _diagonalTraversalAllowed; } }
    }
}