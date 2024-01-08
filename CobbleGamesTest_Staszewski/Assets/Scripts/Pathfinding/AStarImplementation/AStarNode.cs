using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public class AStarNode : IHeapItem<int>
    {
        private const string COORD_TO_STRING_FORMAT = "({0},{1})";
        private const string TOSTRING_FORMAT = "({0},{1}), Parent: {2}, F= {3}, G= {4}, Total={5}";

        public int Priority => _distanceToTargetEstimated + _distanceTraversed;

        private int _distanceTraversed;
        private int _distanceToTargetEstimated;

        private AStarNode _parent;

        private Tuple<int, int> _coords;


        public AStarNode(int xCoord, int yCoord, int distanceTraversed, int distanceToTarget, AStarNode parent = null)
        {
            _coords = new Tuple<int, int>(xCoord, yCoord);

            _distanceTraversed = distanceTraversed;
            _distanceToTargetEstimated = distanceToTarget;

            _parent = parent;
        }

        public int X { get { return _coords.Item1 ; } }
        public int Y { get { return _coords.Item2; } }
        
        public AStarNode[] GetCalculatedPathToNode()
        {
            //  Technically, this should be done better, as in - not handled by this class. Still better than public access to parent node.

            List<AStarNode> result = new List<AStarNode>();
            result.Add(this);

            bool notFinished = true;

            while(notFinished)
            {
                notFinished = AppendPath(ref result);
            }

            return result.ToArray();
        }

        public static bool EqualCoords(AStarNode nodeA, AStarNode nodeB)
        {
            return
                nodeA.X.Equals(nodeB.X) &&
                nodeA.Y.Equals(nodeB.Y);
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

        private bool AppendPath(ref List<AStarNode> resultPath)
        {
            if(resultPath == null)
            {
                throw new System.ArgumentNullException("Path container not initialized.");
            }

            bool result = _parent != null;

            if(result)
            {
                resultPath.Insert(0, _parent);
            }

            return result;
        }
    }
}