using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameModels.Astronomy;

namespace Assets.Scripts.Managers
{
    public class GalaxyManager : MonoBehaviour
    {

        const int MAX_SYSTEMS = 150;
        const float MAX_RADIUS = 50;
        const float MIN_RADIUS = 2f;
        const float Z_VARIANCE = 0.2f;
        const float MIN_DISTANCE = 2f;
        const float LINK_DIST = 5f;

        public void Generate(int numOfStars, float maxRadius, float minRadius, Galaxy galaxy)
        {
            StartCoroutine(SystemGeneration(numOfStars, maxRadius, minRadius, galaxy));
        }

        public IEnumerator SystemGeneration(int numOfStars, float maxRadius, float minRadius, Galaxy galaxy)
        {
            for (int i = 0; i < numOfStars; i++)
            {
                float dist;
                float angle;

                Vector3 ssPos;

                int numOfChecks = 0;
                do
                {
                    dist = Random.Range(minRadius, maxRadius);
                    angle = Random.Range(0, 2 * Mathf.PI);


                    ssPos = new Vector3(dist * Mathf.Cos(angle), dist * Mathf.Sin(angle), Random.Range(-Z_VARIANCE, Z_VARIANCE));
                    numOfChecks++;
                    if (Physics.CheckSphere(ssPos, MIN_DISTANCE))
                    {
                    }
                } while (numOfChecks < 999 && Physics.CheckSphere(ssPos, MIN_DISTANCE)
                );
                var availableLanes = ValidIndexLinks(ssPos);
                SolarSystem ss = new SolarSystem($"Star-{i}", i, ssPos, galaxy, availableLanes);
                ss.Generate(3);
                galaxy.AddSolarSystem(ss);

                yield return new WaitForFixedUpdate();
            }
        }

        #region Linking

        int[] ValidIndexLinks(Vector3 pos)
        {
            List<int> returnIndexList = new List<int>(); // initiating list, it will contain the index of every star system it can connect to
            Collider[] starsInRange = Physics.OverlapSphere(pos, LINK_DIST); // get an array of all the stars in a certain range
            for (int i = 0; i < starsInRange.Length; i++) // we will first loop trough every star system in range to see if it can be connected
            {
                bool canConnect = true;   // we will first assume two star systems can be linked, but if we find one that doesn't, then set it to false
                                          // this is the star system that we will check if it can be added
                SolarSystem ssToAdd = starsInRange[i].gameObject.GetComponentInParent<GalaxyStar>().solarSystem;
                for (int j = 0; j < starsInRange.Length; j++) //loop trough the stars in range again to check if they have a conflicting lane
                {
                    if (starsInRange[i] == starsInRange[j]) // if the star we want to add and the star we are checking for
                    {                                                            // conflicts is the same, just ignore it
                        continue;
                    }
                    // this is the other star system that has links that might be conflicting
                    SolarSystem inTheWaySS = starsInRange[j].gameObject.GetComponentInParent<GalaxyStar>().solarSystem;
                    foreach (var link in inTheWaySS.links)
                    {
                        if (SegmentsCross( //this method returns true given the start and end of two segments
                                                pos,                           // the start of the new hypothetical lane goes from the position of the new star system
                                                ssToAdd.Position,     // the end position of the same lane goes at the position of the star we are checking to link with 
                                                link.Value.Start,                // the start position of the second segment is the start of the link
                                                link.Value.End))                 // the end position of the second segment is the end of the link
                        {
                            canConnect = false;     // if both segments intersect, we cant connect them thus we set canConnect to false
                        }
                    }
                }
                if (canConnect)   // check if there have been no conflicting lanes
                {
                    returnIndexList.Add(ssToAdd.Index); //if there have not been, we can add  the index of the star we can connect to to the list
                }
            }
            return returnIndexList.ToArray(); //return the list of all the star systems we can connect to
        }

        bool SegmentsCross(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
        {
            var o1 = ClockwiseOrientation(p1, q1, p2);
            var o2 = ClockwiseOrientation(p1, q1, q2);
            var o3 = ClockwiseOrientation(p2, q2, p1);
            var o4 = ClockwiseOrientation(p2, q2, q1);
            return o1 != o2 && o3 != o4;
        }

        bool ClockwiseOrientation(Vector2 a, Vector2 b, Vector2 c)
        {
            return (b.y - a.y) * (c.x - b.x) - (c.y - b.y) * (b.x - a.x) > 0;
        }

        #endregion
    }
}