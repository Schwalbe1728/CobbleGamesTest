using System.Collections;
using System.Collections.Generic;
using Test.Map.Abstraction;
using UnityEngine;

namespace Test.Pathfinding
{
    public class PathfindingModule
    {
        protected MapAbstraction mapAbstraction;
        protected HeuristicModule heuristic;

        public void Initialize(MapAbstraction map, HeuristicModule heuristic)
        {
            mapAbstraction = map;
            this.heuristic = heuristic;
        }

        public Vector2Int[] CalculatePathTo(/* TraversingEntity character, */ Vector2Int destination )
        {
            throw new System.NotImplementedException();
        }
    }
}