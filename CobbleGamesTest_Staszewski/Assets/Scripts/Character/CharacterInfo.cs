using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Characters
{
    public class CharacterInfo
    {
        private string _identifier;
        private CharacterAttributes _attributes;

        public string Identifier => _identifier;
        public int Speed => _attributes.Speed;
        public int Mobility => _attributes.Mobility;
        public int Durability => _attributes.Durability;
    }
}