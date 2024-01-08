using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public class AStarAlgorithm<T> where T: Tuple<int, int>
    {
        public T[] CalculatePath(T start, T target)
        {
            throw new System.NotImplementedException();

            //  TODO: map abstract and heuristic classes required to be passed down. to private CalculatePath as well.
        }

        private AStarNode[] CalculatePath(AStarNode start, AStarNode target)
        {            
            MinHeap<AStarNode> OPEN = new MinHeap<AStarNode>();
            HashSet<AStarNode> CLOSED = new HashSet<AStarNode>();

            AStarNode current = null;

            OPEN.Insert(start);

            while(OPEN.Empty == false)
            {
                current = OPEN.Get();
                CLOSED.Add(current);

                if( AStarNode.EqualCoords(current, target) )
                {
                    break;
                }

                AppendWithNeighbours(current, ref OPEN, ref CLOSED);
            }

            return
                current.GetCalculatedPathToNode();
        }

        private void AppendWithNeighbours(AStarNode current, ref MinHeap<AStarNode> OPEN, ref HashSet<AStarNode> CLOSED)
        {
            throw new System.NotImplementedException();

            //  1. Generate nodes neighbouring to current
            //  2. For each neighbour, if that neighbour isn't already in CLOSED, insert to OPEN

            //  TODO:
            //  - heuristic class: gives estimated distance between two fields
        }
    }
}