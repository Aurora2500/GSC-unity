using System;
using UnityEngine;

namespace Assets.Scripts.GameModels.Colonies
{
    class Construction
    {
        public float WorkLeft { get; private set; }

        public bool Finished { get; private set; }

        // return leftover work points
        public float Work(float work)
        {
            if (work > 0)
            {
                throw new ArgumentException($"work must be positive, given integer was {work}", "work");
            }

            float leftOver = Mathf.Max(work - WorkLeft, 0);

            WorkLeft = Mathf.Max(0, WorkLeft - work);

            Finished = WorkLeft > 0;

            return leftOver;
        }

        public Building Building;
    }
}