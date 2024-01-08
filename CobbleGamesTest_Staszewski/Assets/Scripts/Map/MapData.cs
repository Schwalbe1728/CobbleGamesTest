using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Map.Abstraction
{
    public class MapData
    {
        private float _realWidth;
        private float _realHeight;
        private Vector3 _centerCoord;

        private GameObject[] _obstacles;

        public float Width { get { return _realWidth; } }
        public float Height { get { return _realHeight; } }

        public Vector3 CenterCoord { get { return _centerCoord; } } 

        //  TODO: a safe way to poll for those Unity GameObjects - probably a method that accepts some kind of interface
    }
}