using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Menu
{
    public class PlayMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Galaxy");
        }
    }
}