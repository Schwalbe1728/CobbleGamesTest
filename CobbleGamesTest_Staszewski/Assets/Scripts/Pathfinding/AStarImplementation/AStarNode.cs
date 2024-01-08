using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public class AStarNode : IHeapItem<int>     //  .NET pre .NET 7 doesn't allow for generic limited to number, so I bit the bullet and decided T == int
    {
        private const string COORD_TO_STRING_FORMAT = "({0},{1})";
        private const string TOSTRING_FORMAT = "({0},{1}), Parent: {2}, F= {3}, G= {4}, Total={5}";

        public int Priority => _distanceToTargetEstimated + _distanceTraversed;

        private int _distanceTraversed;
        private int _distanceToTargetEstimated;

        private AStarNode _parent;

        private Vector2Int _coords;


        public AStarNode(int xCoord, int yCoord, int distanceTraversed, int distanceToTarget, AStarNode parent = null)
        {
            _coords = new Vector2Int(xCoord, yCoord);

            _distanceTraversed = distanceTraversed;
            _distanceToTargetEstimated = distanceToTarget;

            _parent = parent;
        }

        public int X { get { return _coords.x ; } }
        public int Y { get { return _coords.y; } }

        public int DistanceTraversed { get { return _distanceTraversed; } }
        public int DistanceToTargetEstimated { get { return _distanceToTargetEstimated; } }
        
        public Vector2Int[] GetCalculatedPathToNode()
        {
            //  Technically, this should be done better, as in - not handled by this class. Still better than public access to parent node.

            List<Vector2Int> result = new List<Vector2Int>();
            result.Add(this._coords);

            bool notFinished = true;
            AStarNode current = this;

            while(notFinished)
            {
                notFinished = current.AppendPath(ref result);
                current = current._parent;
            }

            return result.ToArray();
        }

        public static bool EqualCoords(AStarNode nodeA, AStarNode nodeB)
        {
            return
                nodeA.X.Equals(nodeB.X) &&
                nodeA.Y.Equals(nodeB.Y);
        }

        public static void Distance(AStarNode nodeA, AStarNode nodeB, HeuristicModule heuristic, out int traversalCostNeighbours, out int traversalCostEstimated)
        {
            int distance = 0;

            heuristic.CalculateDistance(nodeA._coords, nodeB._coords, out distance, out bool areNeighbours);

            if(areNeighbours)
            {
                traversalCostNeighbours = distance;
                traversalCostEstimated = 0;
            }
            else
            {
                traversalCostNeighbours = 0;
                traversalCostEstimated = distance;
            }
        }

        public static void Distance(AStarNode nodeA, Vector2Int pointB, HeuristicModule heuristic, out int traversalCostNeighbours, out int traversalCostEstimated)
        {
            int distance = 0;

            heuristic.CalculateDistance(nodeA._coords, pointB, out distance, out bool areNeighbours);

            if (areNeighbours)
            {
                traversalCostNeighbours = distance;
                traversalCostEstimated = 0;
            }
            else
            {
                traversalCostNeighbours = 0;
                traversalCostEstimated = distance;
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as AStarNode;

            return
                item != null &&

                AStarNode.EqualCoords
                (
                    this,
                    item
                );
        }        

        public override string ToString()
        {
            return
                string.Format( 
                        TOSTRING_FORMAT, 
                        X,
                        Y,
                        (_parent != null? string.Format(COORD_TO_STRING_FORMAT, _parent.X, _parent.Y) : string.Empty ),
                        _distanceTraversed,
                        _distanceToTargetEstimated,
                        (_distanceToTargetEstimated + _distanceTraversed).ToString()
                    );
        }

        
        public override int GetHashCode()
        {
            return
                _coords.GetHashCode();
        }

        private bool AppendPath(ref List<Vector2Int> resultPath)
        {
            if(resultPath == null)
            {
                throw new System.ArgumentNullException("Path container not initialized.");
            }

            bool result = _parent != null;

            if(result)
            {
                resultPath.Insert(0, _parent._coords);
            }

            return result;
        }
    }
}