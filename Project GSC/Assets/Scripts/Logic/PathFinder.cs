using Assets.Scripts.GameModels.Astronomy;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public static class PathFinder
    {
        public static bool TryPath(Galaxy g, int start, int end)
        {
            return ValidPath(g, start, end);
        }

        public static bool TryPath(Galaxy g, int start, int end, out IEnumerable<int> path)
        {
            if (ValidPath(g, start, end))
            {
                path = FindPath(g, start, end);
                return true;
            }
            path = default;
            return false;
        }

        private static bool ValidPath(Galaxy g, int start, int end)
        {
            Queue<int> queue = new Queue<int>();
            HashSet<int> checkedSystems = new HashSet<int>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                if (current == end) return true;
                SolarSystem ss = g.solarSystems[current];
                foreach (int otherSystemIndex in ss.linkedSystemIndex)
                {
                    if (!checkedSystems.Contains(otherSystemIndex))
                    {
                        checkedSystems.Add(otherSystemIndex);
                        queue.Enqueue(otherSystemIndex);
                    }
                }
            }
            return false;
        }

        private static IEnumerable<int> FindPath(Galaxy galaxy, int start, int end)
        {
            //start a heap the size of the galaxy
            PQ pq = new PQ(galaxy.Size);

            //distance array to each star
            float[] distances = new float[galaxy.Size];
            int[] previous = new int[galaxy.Size];
            distances[start] = 0;

            for (int i = 0; i < galaxy.Size; i++)
            {
                if (i != start)
                {
                    distances[i] = Mathf.Infinity;

                }
                pq.AddWithPriority(i, distances[i]);
            }

            bool searching = true;
            while (searching)
            {
                var currentSearchingIndex = pq.GetLowest().value;
                SolarSystem currentSolarSystem = galaxy.solarSystems[currentSearchingIndex];
                for (int i = 0; i < currentSolarSystem.linkedSystemIndex.Count; i++)
                {
                    int otherIndex = currentSolarSystem.linkedSystemIndex[i];
                    float newDistance = distances[currentSearchingIndex] + currentSolarSystem.links[otherIndex].Distance;
                    if (newDistance < distances[otherIndex])
                    {
                        distances[otherIndex] = newDistance;
                        previous[otherIndex] = currentSearchingIndex;
                        pq.DecreasePriority(otherIndex, newDistance);
                    }
                }

                searching = !pq.IsEmpty() && distances[end] > pq.Peek().priority;
            }
            var sequence = new Stack<int>();
            int target = end;
            while (target != start)
            {
                sequence.Push(target);
                target = previous[target];
            }
            return sequence;
        }


        [DebuggerDisplay("{value, nq} : {priority, nq}")]
        private struct Node
        {
            public readonly int value;
            public float priority;
            public Node(int v, float p)
            {
                value = v;
                priority = p;
            }

            public void UpdatePriority(float newp)
            {
                priority = newp;
            }
        }

        private class PQ
        {
            private int size = 0;

            private Node[] items;
            private int?[] valueIndex;


            public PQ(int capacity)
            {
                items = new Node[capacity];
                valueIndex = new int?[capacity];
            }

            private int GetLeftChildIndex(int parentIndex) { return 2 * parentIndex + 1; }
            private int GetRightChildIndex(int parentIndex) { return 2 * parentIndex + 2; }
            private int GetParentIndex(int childIndex) { return (childIndex - 1) / 2; }

            private bool HasLeftChild(int index) { return GetLeftChildIndex(index) < size; }
            private bool HasRightChild(int index) { return GetRightChildIndex(index) < size; }
            private bool HasParent(int index) { return GetParentIndex(index) >= 0; }

            private float LeftChildCost(int index) { return items[GetLeftChildIndex(index)].priority; }
            private float RightChildCost(int index) { return items[GetRightChildIndex(index)].priority; }
            private float ParentCost(int index) { return items[GetParentIndex(index)].priority; }
            private float IndexCost(int index) { return items[index].priority; }

            public bool IsEmpty()
            {
                return size == 0;
            }

            private void Swap(int indexOne, int indexTwo)
            {
                var temp = items[indexOne];
                items[indexOne] = items[indexTwo];
                items[indexTwo] = temp;
                valueIndex[temp.value] = indexTwo;
                valueIndex[items[indexOne].value] = indexOne;
            }

            public Node Peek()
            {
                if (size == 0) throw new IllegalStateException();
                return items[0];
            }

            public Node GetLowest()
            {
                if (size == 0) throw new IllegalStateException();
                var returnItem = items[0];
                valueIndex[returnItem.value] = null;
                items[0] = items[--size];
                valueIndex[items[0].value] = 0;
                HeapifyDown();
                return returnItem;
            }

            public void AddWithPriority(int value, float priority)
            {
                Node item = new Node(value, priority);
                items[size] = item;
                valueIndex[value] = size;
                size++;
                HeapifyUp();
            }

            public void DecreasePriority(int value, float priority)
            {
                int index = valueIndex[value] ?? -1;
                if (index == -1) return;
                items[index].UpdatePriority(priority);
                HeapifyFrom(index);
            }

            private void HeapifyUp()
            {
                int index = size - 1;
                while (HasParent(index) && ParentCost(index) > IndexCost(index))
                {
                    Swap(GetParentIndex(index), index);
                    index = GetParentIndex(index);
                }
            }

            private void HeapifyFrom(int index)
            {
                while (HasParent(index) && ParentCost(index) > IndexCost(index))
                {
                    Swap(GetParentIndex(index), index);
                    index = GetParentIndex(index);
                }
            }

            private void HeapifyDown()
            {
                int index = 0;
                while (HasLeftChild(index))
                {
                    int smallerChildIndex = GetLeftChildIndex(index);
                    if (HasRightChild(index) && RightChildCost(index) < LeftChildCost(index))
                    {
                        smallerChildIndex = GetRightChildIndex(index);
                    }

                    if (IndexCost(index) < IndexCost(smallerChildIndex))
                    {
                        break;
                    }
                    else
                    {
                        Swap(index, smallerChildIndex);
                    }
                    index = smallerChildIndex;
                }
            }

        }

        [System.Serializable]
        private class IllegalStateException : System.Exception
        {
            public IllegalStateException() { }
            public IllegalStateException(string message) : base(message) { }
            public IllegalStateException(string message, System.Exception inner) : base(message, inner) { }
            protected IllegalStateException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}