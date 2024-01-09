using System.Collections;
using System.Collections.Generic;
using Test.Map.Abstraction;
using Test.Pathfinding.Boids;
using UnityEngine;

namespace Test.Pathfinding
{
    public class GroupPathfindingModule : PathfindingModule
    {
        private BoidsParametresConfiguration _boidsConfig = null;

        public void Initialize(MapAbstraction map, HeuristicModule heuristic, BoidsParametresConfiguration boidsConfig)
        {
            Initialize(map, heuristic);
            _boidsConfig = boidsConfig;
        }

        /// <summary>
        /// Calculates movement direction vectors of the entire group.
        /// Current target is the current grid cell leader is heading towards on the path to overall destination.
        /// Leader is exempt from boids calculations
        /// </summary>
        /// <param name="currentTarget"></param>
        /// <param name="leaderIndex"></param>
        /// <returns></returns>
        public Vector2[] CalculateGroupMovementVectors(Vector2Int currentTarget, int leaderIndex /*, Character[] groupCharacters */)
        {
            throw new System.NotImplementedException();
        }

        private Vector2[] CalculateBoids(Vector2Int target, int leaderIndex /*, Character[] groupCharacters */)
        {
            int count = 10;                     //  TODO: count = group.Length instead

            Vector2Int followPoint = target;    //  NOTE: we might decide that followPoint should be the leader's current cell - hence a temporary container like this
            Vector2[] directions = new Vector2[count];
            float[,] influenceStrengthMatrix = null;   //  = CalculateInfluenceStrengthMatrix

            for(int i = 0; i < count; i++)
            {   
                //  TODO: initialize direction vectors
                //result[i] = i != leaderIndex ? followPoint - group[i].position : target - group[i].position
            }

            Vector2[] seperationVectors = SeperationCheck(leaderIndex, influenceStrengthMatrix);
            Vector2[] obstacleAvoidanceVectors = ObstacleAvoidanceCheck(leaderIndex, mapAbstraction);
            Vector2[] alignmentVectors = AlignmentCheck(leaderIndex, influenceStrengthMatrix, directions);
            Vector2[] cohesionVectors = CohesionCheck(leaderIndex, influenceStrengthMatrix);

            for(int i = 0; i < directions.Length; i++)
            {
                if (i != leaderIndex)
                {
                    directions[i] +=
                      _boidsConfig.AvoidanceStrength * seperationVectors[i] +
                      _boidsConfig.AvoidanceStrength * obstacleAvoidanceVectors[i] +
                      _boidsConfig.AlignmentStrength * alignmentVectors[i] +
                      _boidsConfig.CohesionStrength * cohesionVectors[i];
                }

                directions[i] = directions[i].normalized;
            }

            return
                directions;
        }

        private float[,] CalculateInfluenceStrengthMatrix(Vector2[] positions)
        {
            float[,] result = new float[positions.Length, positions.Length];

            for(int y = 0; y < positions.Length; y++)
            {
                for (int x = 0; x < positions.Length; x++)
                {
                    result[x, y] =
                        _boidsConfig.CalculateInfluenceMultiplier(Vector2.Distance(positions[x], positions[y]));
                }
            }

            return
                result;
        }

        private Vector2[] SeperationCheck( /*Character[] groupCharacters, */ int leaderIndex, float[,] influenceMatrix)
        {
            //  Steer away from nearby boids

            /*
             *  This should've been based off of the directions towards Characters, not their directions - otherwise this code is mostly correct (consult AlignmentCheck)
            Vector2[] avoidanceVectors = new Vector2[directionVectors.Length];

            for(int i = 0; i < directionVectors.Length; i++)
            {
                avoidanceVectors[i] = Vector2.zero;

                for(int j = 0; j < directionVectors.Length; j++)
                {
                    avoidanceVectors[i] +=
                        -directionVectors[j] * influenceMatrix[i, j];
                }
            }

            return avoidanceVectors;
            */

            throw new System.NotImplementedException();
        }

        private Vector2[] ObstacleAvoidanceCheck(/*Character[] groupCharacters, */int leaderIndex, MapAbstraction mapAbstraction)
        {
            //  for each character that isn't the leader
            //  attempt to poll for non traversible cells
            //  results are the weighted sum of (character - obstacle)
            //  where weights are equal to influence based on distance between the two

            throw new System.NotImplementedException();
        }

        private Vector2[] AlignmentCheck(/*Character[] groupCharacters, */ int leaderIndex, float[,] influenceMatrix, Vector2[] directionVectors)
        {
            //  Steer to move in the similar direction as nearby boids
            Vector2[] alignmentCorrectionVectors = new Vector2[directionVectors.Length];

            for (int i = 0; i < directionVectors.Length; i++)
            {
                alignmentCorrectionVectors[i] = Vector2.zero;

                if (i != leaderIndex)
                {                    
                    for (int j = 0; j < directionVectors.Length; j++)
                    {
                        if (influenceMatrix[i, j] > 0f)             //  NOTE: this check is to lower the amount of practically useless Vector2 structs generated
                        {
                            alignmentCorrectionVectors[i] +=
                                influenceMatrix[i, j] * 
                                directionVectors[j];
                        }
                    }

                    alignmentCorrectionVectors[i] =
                        alignmentCorrectionVectors[i].normalized;
                }
            }

            return alignmentCorrectionVectors;
        }

        private Vector2[] CohesionCheck(/*Character[] groupCharacters, */ int leaderIndex, float[,] influenceMatrix)
        {
            //  Steer towards the center of any nearby boids
            //  aka weighted average of groupCharacters positions (weight is influence)
            throw new System.NotImplementedException();
        }
    }
}