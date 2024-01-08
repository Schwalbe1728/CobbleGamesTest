
using System;
using System.Collections.Generic;

namespace Test.Pathfinding.AStar
{
    public abstract class Heap<T> where T : IHeapItem<int>
    {
        private T[] _heapArray;
        private int _heapCount;

        private Dictionary<T, int> _lookUpTable;

        public Heap()
        {
            _heapCount = 0;
            _heapArray = new T[2];

            _lookUpTable = new Dictionary<T, int>();
        }

        public bool Empty { get { return _heapCount == 0; } }

        public T Peek()
        {
            return
                _heapArray[1];
        }

        public T Get()
        {
            T result = Peek();

            _heapCount--;
            _lookUpTable.Remove(result);

            if (_heapCount > 0)
            {
                _heapArray[1] = _heapArray[_heapCount + 1];
                DownHeap(1, _heapCount);

                if(_heapCount < _heapArray.Length / 4)      //  rationale: less resizing in fringe cases within AStar algorithm
                {
                    DownsizeArray();
                }
            }            

            return result;
        }

        public void Insert(T item)
        {
            if (_lookUpTable.ContainsKey(item))
            {
                int oldItemIndex = _lookUpTable[item];

                if (HeapCriterium(_heapArray[oldItemIndex], item) == false)
                {
                    _lookUpTable.Remove(_heapArray[oldItemIndex]);
                    _lookUpTable.Add(item, oldItemIndex);
                    _heapArray[oldItemIndex] = item;

                    UpHeap(oldItemIndex);
                }
            }
            else
            {
                InsertItem(item);
            }
        }

        public void CreateHeap(T[] replacementItemArray)
        {
            _heapCount = replacementItemArray.Length;
            _heapArray = new T[_heapCount + 1];

            for(int i = 1; i < _heapCount; i++)
            {
                _heapArray[i] = replacementItemArray[i - 1];
            }

            CreateHeap();
        }

        public void CreateHeap()
        {
            for(int i = _heapCount / 2; i >= 1; i--)
            {
                DownHeap(i, _heapCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encountered">The element potentially "pushed" in the opposite direction</param>
        /// <param name="candidate"></param>
        /// <returns>TRUE when tree criterium between the items is preserved; FALSE means swapping is needed</returns>
        protected abstract bool HeapCriterium(T encountered, T candidate);

        protected bool HeapCriterium(int encounteredIndex, int candidateIndex)
        {
            return
                HeapCriterium(
                    _heapArray[encounteredIndex],
                    _heapArray[candidateIndex]
                    );
        }

        private void InsertItem(T item, bool upHeap = true)
        {
            if (_heapCount > _heapArray.Length - 2)
            {
                UpsizeArray();
            }

            _heapArray[++_heapCount] = item;
            _lookUpTable.Add(item, _heapCount);

            if (upHeap && _heapCount > 1)
            {
                UpHeap(_heapCount);
            }
        }

        private void DownHeap(int rootIndex, int limitIndex)
        {
            int swapIndex = 2 * rootIndex;
            T item = _heapArray[rootIndex];

            while( swapIndex <= limitIndex && swapIndex <= _heapCount )     //  the other check is practically redundant, but better safe than sorry
            {
                if( swapIndex + 1 <= limitIndex )
                {
                    if( HeapCriterium(swapIndex + 1, swapIndex) )   //  select the child node better suited for potential swap
                    {
                        swapIndex += 1;
                    }
                }

                if( HeapCriterium(item, _heapArray[swapIndex]) == false )
                {
                    _lookUpTable[_heapArray[swapIndex]] = rootIndex;

                    _heapArray[rootIndex] = _heapArray[swapIndex];                    
                    rootIndex = swapIndex;
                    swapIndex = 2 * rootIndex;
                }
                else
                {
                    break;
                }
            }

            _heapArray[rootIndex] = item;
            _lookUpTable[item] = rootIndex;
        }

        private void UpHeap(int itemIndex)
        {
            T item = _heapArray[itemIndex];

            while( 
                itemIndex > 1 &&
                HeapCriterium(_heapArray[itemIndex / 2], item) == false )
            {
                _lookUpTable[_heapArray[itemIndex / 2]] = itemIndex;
                _heapArray[itemIndex] = _heapArray[itemIndex / 2];          //  pushing the items down on the path to the top
                itemIndex /= 2;
            }

            _heapArray[itemIndex] = item;                                   //  we've found where the item belongs priority wise
            _lookUpTable[item] = itemIndex;
        }              

        #region Heap Array Size
        private void UpsizeArray()
        {
            Array.Resize(ref _heapArray, 2 * _heapArray.Length);
        }

        private void DownsizeArray()
        {
            if (_heapArray.Length > 2)
            {
                Array.Resize(ref _heapArray, _heapArray.Length / 2);
            }
        }
        #endregion        

    }
}