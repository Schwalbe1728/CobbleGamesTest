using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Characters
{
    public class CharacterSaveState
    {
        private Character _characterState;

        public Character CharacterState => _characterState;

        public CharacterSaveState(Character characterState)
        {
            _characterState = characterState;
        }
    }
}