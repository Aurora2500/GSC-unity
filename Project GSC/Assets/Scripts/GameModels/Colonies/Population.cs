using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.GameModels.Colonies
{
    public class Population : IEnumerable
    {
        List<Demographic> demographics;

        public IEnumerator GetEnumerator()
        {
            yield return demographics;
        }
    }
}