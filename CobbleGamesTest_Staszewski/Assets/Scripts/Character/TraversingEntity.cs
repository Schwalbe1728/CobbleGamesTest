using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Characters
{
    public abstract class TraversingEntity
    {
        private Vector2Int _gridPosition;

        public Vector2Int GetPosition()
        {
            return
                _gridPosition;
        }

        protected void UpdatePosition(Vector2Int newPosition)
        {
            _gridPosition = newPosition;
        }
    }
}