using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding
{
    public class HeuristicModule
    {
        private HeuristicConfigurationInt _heuristicConfiguration;

        public HeuristicModule(HeuristicConfigurationInt config)
        {
            _heuristicConfiguration = config;
        }

        public void CalculateDistance(Vector2Int pointA, Vector2Int pointB, /* MAP ASTRACTION, */ out int distance, out bool neighbours)
        {
            if(AreSameCell(pointA, pointB))
            {
                distance = 0;
                neighbours = false;
                return;
            }

            neighbours = AreNeighbouringCells(pointA, pointB);

            if(neighbours)
            {
                distance =
                    AreAdjacentCells(pointA, pointB) ?
                        _heuristicConfiguration.AdjacentCellCost :
                        (AreDiagonalCells(pointA, pointB) ? _heuristicConfiguration.DiagonalCellCost : 0);
            }
            else
            {
                //  we return estimate
                distance =
                    _heuristicConfiguration.DiagonalTraversalAllowed ?
                        Mathf.RoundToInt(Vector2Int.Distance(pointA, pointB) * _heuristicConfiguration.AdjacentCellCost) :
                        (Mathf.Abs(pointA.x - pointB.x) + Mathf.Abs(pointA.y - pointB.y)) * _heuristicConfiguration.AdjacentCellCost;
            }
        }

        private bool AreSameCell(Vector2Int pointA, Vector2Int pointB)
        {
            return
                pointA.x.Equals(pointB.y) &&
                pointA.x.Equals(pointB.y);
        }

        private bool AreNeighbouringCells(Vector2Int pointA, Vector2Int pointB)
        {
            if (_heuristicConfiguration.DiagonalTraversalAllowed)
            {
                return
                    Mathf.Abs(pointA.x - pointB.x) == 1 ||
                    Mathf.Abs(pointA.y - pointB.y) == 1;
            }
            else
            {
                return
                    AreAdjacentCells(pointA, pointB);
            }
        }

        private bool AreDiagonalCells(Vector2Int pointA, Vector2Int pointB)
        {
            return
                _heuristicConfiguration.DiagonalTraversalAllowed &&
                Mathf.Abs(pointA.x - pointB.x) == 1 &&
                Mathf.Abs(pointA.y - pointB.y) == 1;
        }

        private bool AreAdjacentCells(Vector2Int pointA, Vector2Int pointB)
        {
            int dX = Mathf.Abs(pointA.x - pointB.x);
            int dY = Mathf.Abs(pointA.y - pointB.y);

            return
                (dX == 1 && dY == 0) || (dX == 0 && dY == 1);
        }
    }
}