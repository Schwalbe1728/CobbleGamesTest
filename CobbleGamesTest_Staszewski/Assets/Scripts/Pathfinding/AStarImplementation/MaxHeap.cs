using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Pathfinding.AStar
{
    public class MaxHeap : Heap<IHeapItem<int>>
    {
        protected override bool HeapCriterium(IHeapItem<int> encountered, IHeapItem<int> candidate)
        {
            return
                encountered.Priority >= candidate.Priority;
        }
    }
}