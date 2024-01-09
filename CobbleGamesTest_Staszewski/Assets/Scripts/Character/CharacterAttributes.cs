using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Characters
{
    public class CharacterAttributes
    {
        private int _speed;
        private int _mobility;
        private int _durability;

        public int Speed => _speed;
        public int Mobility => _mobility;
        public int Durability => _durability;
    }
}