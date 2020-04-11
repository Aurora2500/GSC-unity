using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Colonies
{
    public class Colony
    {
        public int Index { get; private set; }
        public int OwnerID { get; private set; }

        public Population Pop { get; private set; }
        public Infrastructure Infrastructure { get; private set; }
    }
}