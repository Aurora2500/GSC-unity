﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameModels.Astronomy
{
    public class SolarSystem
    {
        private const int MaxPlanet = 5;

        public Vector3 Position { get; set; }

        public string Name { get; set; }


        public Galaxy galaxy;

        public List<Planet> planets;
        private int systemSize;

        public List<int> linkedSystemIndex;
        public Dictionary<int, Link> links;
        public int Index { get; private set; }
        public Star star { get; private set; }

        public GameObject galaxyStar;
        GalaxyStar galaxyStarScript;

        public SolarSystem(string name, int index, Vector3 pos, Galaxy g, int[] laneIndexes)
        {
            Name = name;
            Index = index;
            Position = pos;

            galaxy = g;

            linkedSystemIndex = new List<int>();
            links = new Dictionary<int, Link>();

            for (int i = 0; i < laneIndexes.Length; i++)
            {
                var laneIndex = laneIndexes[i];
                LinkWithStarIndex(laneIndex);
            }


            Render();
        }

        #region save
        public SolarSystem(SolarSystemData data, Galaxy g)
        {
            Name = data.name;
            Index = data.index;

            linkedSystemIndex = new List<int>();
            links = new Dictionary<int, Link>();
            galaxy = g;

            for (int i = 0; i < data.linkedSystems.Length; i++)
            {
                linkedSystemIndex.Add(data.linkedSystems[i]);
            }

            Position = new Vector3(data.position[0], data.position[1], data.position[2]);

            Render();
        }

        public void LoadLinks()
        {
            for (int i = 0; i < linkedSystemIndex.Count; i++)
            {
                if (linkedSystemIndex[i] < Index)
                {
                    // get other star system
                    var otherSS = galaxy.solarSystems[linkedSystemIndex[i]];

                    // create the link
                    var prefab = Resources.Load("StarLink") as GameObject;
                    var link = new Link(Position, Index, otherSS.Position, otherSS.Index, prefab);

                    //update both solar systems with each other's pressence and the link
                    links.Add(linkedSystemIndex[i], link);
                    otherSS.links.Add(Index, link);
                }
            }
        }

        #endregion
        public void Render()
        {
            var prefab = Resources.Load("Galaxy Star System") as GameObject;
            galaxyStar = Object.Instantiate(prefab);
            galaxyStar.transform.position = Position;
            galaxyStar.name = Name;
            galaxyStarScript = galaxyStar.GetComponent<GalaxyStar>();
            galaxyStarScript.solarSystem = this;
            galaxyStarScript.UpdateText();
        }

        #region planets
        public void Generate(int numberOfPlanets)
        {

            star = new Star();
            star.system = this;

            for (int i = 0; i < numberOfPlanets; i++)
            {
                Planet firstPlanet = new Planet();
                AddPlanet(firstPlanet);
            }
        }

        public void AddPlanet(Planet planet)
        {
            planet.System = this;
            planets.Add(planet);
            systemSize += 1;
        }

        public void RemovePlanet(Planet planet)
        {
            planet.Remove();
            planets.Remove(planet);
            systemSize -= 1;
        }
        #endregion

        #region linked systems
        public void AttachLink(int ssIndex, Link link)
        {
            linkedSystemIndex.Add(ssIndex);
            links.Add(ssIndex, link);
        }

        public void LinkWithStarIndex(int index)
        {
            // get other star system
            var otherSS = galaxy.solarSystems[index];



            // create the link
            var prefab = Resources.Load("StarLink") as GameObject;

            var link = new Link(Position, Index, otherSS.Position, otherSS.Index, prefab);

            //update both solar systems with each other's pressence and the link
            AttachLink(index, link);
            otherSS.AttachLink(Index, link);
        }
        #endregion

        public void Destroy()
        {
            star = null;
            planets = null;
            Object.Destroy(galaxyStar);
            foreach (var link in links)
            {
                link.Value.Destroy();
            }
        }
    }
}