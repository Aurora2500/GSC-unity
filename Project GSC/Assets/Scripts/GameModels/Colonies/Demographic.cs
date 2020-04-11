using Assets.Scripts.GameModels.GameContent;
using Assets.Scripts.Logic.DataStructure;

namespace Assets.Scripts.GameModels.Colonies
{
    class Demographic
    {
        // in milions
        public float Size { get; private set; }

        public int LocationID { get; private set; }

        public Species Species { get; private set; }

        public Culture Culture { get; private set; }

        public Percentage<int> Politics { get; private set; }
    }
}