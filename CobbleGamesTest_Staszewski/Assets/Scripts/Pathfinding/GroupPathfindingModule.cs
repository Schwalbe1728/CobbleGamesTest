using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding
{
    public class GroupPathfindingModule : PathfindingModule
    {
        public Vector3[] CalculateGroupMovementVectors(Vector2Int target /*, Character[] groupCharacters */)
        {
            throw new System.NotImplementedException();
        }

        private Vector3[] CalculateBoids(Vector2Int target /*, Character[] groupCharacters */)
        {
            throw new System.NotImplementedException();
        }

        private void SeperationCheck(/*Character[] groupCharacters, */ ref Vector3[] directionVectors)
        {
            //  Make sure group members don't run into each other
            throw new System.NotImplementedException();
        }

        private void AlignmentCheck(/*Character[] groupCharacters, */ ref Vector3[] directionVectors)
        {
            //  Make sure group members run in the similar direction
            throw new System.NotImplementedException();
        }

        private void CohesionCheck(/*Character[] groupCharacters, */ ref Vector3[] directionVectors)
        {
            //  Make sure group members stay close to one another
            throw new System.NotImplementedException();
        }
    }
}