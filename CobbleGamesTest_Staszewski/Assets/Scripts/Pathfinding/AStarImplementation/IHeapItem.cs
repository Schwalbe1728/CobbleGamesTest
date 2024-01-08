using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public interface IHeapItem<T>
    {
        public T Priority { get; }
    }
}