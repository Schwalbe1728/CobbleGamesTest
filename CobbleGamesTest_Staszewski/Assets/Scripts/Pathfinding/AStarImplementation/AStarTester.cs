using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Test.Pathfinding.AStar
{ 
    public class AStarTester : MonoBehaviour
    {
        [SerializeField]
        private int _numberOfTestItems = 10;

        [SerializeField]
        private int _minPriority = 0;

        [SerializeField]
        private int _maxPriority = 100;

        [SerializeField]
        private int _minXCoord = -5;

        [SerializeField]
        private int _maxXCoord = 5;

        [SerializeField]
        private int _minYCoord = -5;

        [SerializeField]
        private int _maxYCoord = 5;

        private void Start()
        {
            TestMinHeap();
        }

        private void TestMinHeap()
        {
            StringBuilder builder = new StringBuilder();

            AStarNode[] testItems = GenerateHeapItems();
            MinHeap<AStarNode> heap = new MinHeap<AStarNode>();

            for(int i = 0; i < testItems.Length; i++)
            {
                builder.AppendLine(string.Format("Insert: {0}", testItems[i].ToString()));
                heap.Insert(testItems[i]);
            }

            Debug.Log(builder.ToString());

            builder.Clear();

            AStarNode current = null;
            int index = 1;

            while(heap.Empty == false)
            {
                current = heap.Get();
                builder.AppendLine(string.Format("{0}:   {1}", index++, current.ToString()));
            }

            Debug.LogFormat("Done;\n\r{0}", builder.ToString());
        }

        private AStarNode[] GenerateHeapItems()
        {
            AStarNode[] result = new AStarNode[_numberOfTestItems];

            for(int i = 0; i < result.Length; i++)
            {
                result[i] =
                    new AStarNode(
                        Random.Range(_minXCoord, _maxXCoord),
                        Random.Range(_minYCoord, _maxYCoord),
                        Random.Range(_minPriority, _maxPriority),
                        Random.Range(_minPriority, _maxPriority)
                        );
            }

            return result;
        }
    }
}