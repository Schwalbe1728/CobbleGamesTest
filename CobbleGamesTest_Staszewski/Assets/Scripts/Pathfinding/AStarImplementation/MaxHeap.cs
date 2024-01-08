using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public class MaxHeap<T> : Heap<T> where T : IHeapItem<int>
    {
        protected override bool HeapCriterium(T encountered, T candidate)
        {
            return
                encountered.Priority >= candidate.Priority;
        }
    }
}