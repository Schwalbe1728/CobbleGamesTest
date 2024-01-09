using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Characters
{
    public class CharacterRepresentation : MonoBehaviour
    {
        private Character _characterRepresented;

        public void CharacterMoved(Vector2Int newGridPosition, Vector3 newMapPosition)
        {
            _characterRepresented.CharacterMoved(newGridPosition);

            throw new System.NotImplementedException();
        }
    }
}