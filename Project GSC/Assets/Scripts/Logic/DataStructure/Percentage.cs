using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Logic.DataStructure
{
    public class Percentage<T>
    {
        Dictionary<T, float> percentages;

        public IReadOnlyDictionary<T, float> Percentages { get => percentages; }

        public int Count { get => percentages.Count; }

        public Percentage()
        {
            percentages = new Dictionary<T, float>();
        }

        public Percentage(Dictionary<T, float> unnormalized) : this()
        {
            float k = unnormalized.Sum(n => n.Value);
            foreach (var key in unnormalized.Keys)
            {
                percentages[key] = unnormalized[key] / k;
            }
        }

        public IReadOnlyList<KeyValuePair<T, float>> Ordered
        {
            get
            {
                List<KeyValuePair<T, float>> dictList = new List<KeyValuePair<T, float>>();
                foreach (var item in percentages.OrderBy(key => -key.Value))
                {
                    dictList.Add(item);
                }
                return dictList;
            }
        }

        public float this[T key]
        {
            get => percentages[key];
        }

        /// <summary>
        /// Adds a category to the percentage and resizes all other values acordingly
        /// </summary>
        /// <param name="category"> what category it changes or adds </param>
        /// <param name="original"> the size of the original sample </param>
        /// <param name="added"> the size of the new sample  being added </param>
        public void Add(T category, float original, float added)
        {
            var keys = new List<T>(percentages.Keys);

            foreach (var key in keys)
            {
                var percent = percentages[key];

                percentages[key] = percent * original / (original + added);
            }

            if (percentages.ContainsKey(category))
            {
                percentages[category] += added / (original + added);
            }
            else
            {
                percentages[category] = added / (original + added);
            }
        }

        public void Add(Percentage<T> other, float original, float otherSize)
        {
            var keys = new List<T>(percentages.Keys);

            foreach (var key in keys)
            {
                var percent = percentages[key];

                percentages[key] = percent * original / (original + otherSize);
            }

            foreach (var category in other.percentages.Keys)
            {
                if (percentages.ContainsKey(category))
                {
                    percentages[category] += other[category] * otherSize / (original + otherSize);
                }
                else
                {
                    percentages[category] = other[category] * otherSize / (original + otherSize);
                }
            }
        }

        public void Sway(T category, float ammount)
        {
            if (ammount < 0 || ammount > 1) throw new ArgumentException($"ammount must be a value between 0 and 1, given ammount is {ammount}", "ammount");

            var keys = new List<T>(percentages.Keys);

            foreach (var key in keys)
            {
                percentages[key] *= 1f - ammount;
            }

            if (percentages.ContainsKey(category))
            {
                percentages[category] += ammount;
            }
            else
            {
                percentages[category] = ammount;
            }

            RemoveEmpty();
        }

        void RemoveEmpty()
        {
            var emptyKeys = percentages.Where(kvp => kvp.Value < float.Epsilon).Select(kvp => kvp.Key).ToList();

            foreach (var key in emptyKeys)
            {
                percentages.Remove(key);
            }
        }
    }
}