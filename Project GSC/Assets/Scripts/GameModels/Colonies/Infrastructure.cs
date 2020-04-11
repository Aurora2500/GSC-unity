using System.Collections.Generic;

namespace Assets.Scripts.GameModels.Colonies
{
    public class Infrastructure
    {
        LinkedList<Construction> inProgress;
        List<Building> buildings;

        public void Work(float work)
        {
            var Node = inProgress.First;
            do
            {
                var b = Node.Value;
                work = b.Work(work);
                if(b.Finished)
                {
                    buildings.Add(b.Building);
                }
                Node = Node.Next;
            } while (work > 0);
        }
    }
}