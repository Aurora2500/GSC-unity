using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GSC;
using Assets.Scripts.Logic;
using Assets.Scripts.Save;

namespace Assets.Scripts.Managers
{
    public class GameController : MonoBehaviour
    {

        //TODO: get it from user
        public int numberOfStars = 50;
        public int maxRadius = 10;
        public int minRadius = 3;

        public GameObject galaxyManager;

        GameInstance game;

        // Start is called before the first frame update
        void OnEnable()
        {
            GenerateGame();
        }
        void GenerateGame()
        {
            game = new GameInstance();
            GalaxyManager gmScript = galaxyManager.GetComponent<GalaxyManager>();

            gmScript.Generate(numberOfStars, maxRadius, minRadius, game.galaxy);
        }




        #region saves
        public void Save(string saveName)
        {
            SaveSystem.SaveGame(game, saveName);
        }

        public void Load(string saveName)
        {

            var data = SaveSystem.LoadGame(saveName);
            game.Destroy();
            game = new GameInstance(data);
        }
        #endregion

        void Update()
        {
            // quick save
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Debug.Log("Saving...");
                Save("QUICKSAVE");
            }

            //quick load
            if (Input.GetKeyDown(KeyCode.F9))
            {
                Debug.Log("Loading...");
                Load("QUICKSAVE");
            }
        }

        void EndTurn()
        {

        }

        public bool TryPath(int start, int end)
        {
            return PathFinder.TryPath(game.galaxy, start, end);
        }

        public bool TryPath(int start, int end, out IEnumerable<int> path)
        {
            return PathFinder.TryPath(game.galaxy, start, end, out path);
        }
    }
}