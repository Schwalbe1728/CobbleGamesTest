using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Characters
{
    public class Character : TraversingEntity
    {
        private CharacterInfo _characterInfo;

        /*
        public CharacterInfo GetInfo()
        {
            return _characterInfo;
        }
        */

        public string Identifier => _characterInfo.Identifier;
        public int AttributeSpeed => _characterInfo.Speed;
        public int AttributeMobility => _characterInfo.Mobility;
        public int AttributeDurability => _characterInfo.Durability;

        public void CharacterMoved(Vector2Int newPosition)
        {
            UpdatePosition(newPosition);
        }

    }
}