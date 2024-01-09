using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.Boids
{
    [CreateAssetMenu(fileName = "BoidsConfig", menuName = "Scriptable Objects/Boids Configuration")]
    public class BoidsParametresConfiguration : ScriptableObject
    {
        [SerializeField]
        private float _boidInfluenceRange = 5f;

        [SerializeField]
        [Min(0.1f)]
        private float _boidsAvoidanceStrength = 1f;

        [SerializeField]
        [Min(0.1f)]
        private float _boidsAlignmentStrength = 1f;

        [SerializeField]
        [Min(0.1f)]
        private float _boidsCohesionStrength = 1f;

        [SerializeField]
        private AnimationCurve _influenceStrength = AnimationCurve.Linear(0, 1f, 1f, 0f);

        public float CalculateInfluenceMultiplier(float distance)
        {
            return
                _influenceStrength.Evaluate(distance / _boidInfluenceRange);
        }

        public float AvoidanceStrength => _boidsAvoidanceStrength;

        public float AlignmentStrength => _boidsAlignmentStrength;

        public float CohesionStrength => _boidsCohesionStrength;
    }
}