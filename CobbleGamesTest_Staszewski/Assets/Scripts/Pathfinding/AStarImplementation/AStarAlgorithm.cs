using System;
using System.Collections;
using System.Collections.Generic;
using Test.Map.Abstraction;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public class AStarAlgorithm
    {
        public Vector2Int[] CalculatePath(Vector2Int start, Vector2Int target, HeuristicModule heuristicModule, MapAbstraction mapAbstract)
        {
            heuristicModule.CalculateDistance(start, target, out int startToTargetDistanceEstimate, out bool areNeighbours);


            AStarNode startNode =
                new AStarNode(
                    start.x,
                    start.y,
                    0,
                    startToTargetDistanceEstimate
                    );

            AStarNode targetNode =
                new AStarNode(
                    target.x,
                    target.y,
                    int.MaxValue / 2,
                    0
                    );

            return
                CalculatePath(startNode, targetNode, heuristicModule, mapAbstract);
        }

        private Vector2Int[] CalculatePath(AStarNode start, AStarNode target, HeuristicModule heuristicModule, MapAbstraction mapAbstract)
        {            
            MinHeap<AStarNode> OPEN = new MinHeap<AStarNode>();
            HashSet<AStarNode> CLOSED = new HashSet<AStarNode>();

            AStarNode current = null;

            if (mapAbstract.GridCoordTraversible(start.X, start.Y))
            {
                OPEN.Insert(start);
            }

            while(OPEN.Empty == false)
            {
                current = OPEN.Get();
                CLOSED.Add(current);

                if( AStarNode.EqualCoords(current, target) )
                {
                    break;
                }

                AppendWithNeighbours(current, target, ref OPEN, ref CLOSED, heuristicModule, mapAbstract);
            }

            return
                current != null ?
                    current.GetCalculatedPathToNode() :
                    null;
        }

        private void AppendWithNeighbours(AStarNode current, AStarNode target, ref MinHeap<AStarNode> OPEN, ref HashSet<AStarNode> CLOSED, HeuristicModule heuristicModule, MapAbstraction mapAbstract)
        {
            Vector2Int[] neighbours =
                mapAbstract.GetTraversibleNeighboursOnGrid(current.X, current.Y);

            if(neighbours != null)
            {
                AStarNode currentNeighbour = null;
                int traversalCostNeighbours = 0;
                int distanceEstimate = 0;
                int dummy = 0;

                for (int i = 0; i < neighbours.Length; i++)
                {
                    AStarNode.Distance(current, neighbours[i], heuristicModule, out traversalCostNeighbours, out dummy);
                    //  NOTE: distanceEstimate returned above is always 0 - current and neighbours[i] are supposed to be neighbours, right?
                    if(dummy != 0)
                    {
                        throw new InvalidOperationException();
                    }

                    AStarNode.Distance(target, neighbours[i], heuristicModule, out dummy, out distanceEstimate);
                                        

                    currentNeighbour =
                        new AStarNode(
                            neighbours[i].x,
                            neighbours[i].y,
                            current.DistanceTraversed + traversalCostNeighbours,
                            Mathf.Max(distanceEstimate, dummy),                     //  CASE:   neighbour[i] of current node is also a neighbour of the target cell => dummy gets filled instead
                            current);

                    if(CLOSED.Contains(currentNeighbour) == false)
                    {
                        OPEN.Insert(currentNeighbour);
                    }
                }
            }
        }
    }
}