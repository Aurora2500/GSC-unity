using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System.IO;
using Assets.Scripts.Mono;
using Assets.Scripts.Managers;
using Assets.Scripts.Save;

namespace Assets.Scripts.UI.Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public GameObject pauseMenuUI;
        public GameObject SaveMenuUI;

        public GameObject slotPanel;
        public GameObject slotPrefab;

        public GameController gameController;
        public TMP_InputField inputField;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        #region Pause Resume

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            SaveMenuUI.SetActive(false);
            GalaxyCameraScript.locked = false;
            gameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            GalaxyCameraScript.locked = true;
            gameIsPaused = true;
        }

        #endregion

        #region Save Load

        #region SaveSlots
        public void DisplaySaves()
        {
            KillSaves();

            var saves = SaveSystem.GetSaves();
            foreach (var save in saves)
            {
                var name = Path.GetFileNameWithoutExtension(save.Name);
                var time = save.LastAccessTime.ToString();

                var slot = Instantiate(slotPrefab, slotPanel.transform);
                var script = slot.GetComponent<SaveSlot>();
                var gcScript = gameController.GetComponent<GameController>();
                script.SetValues(name, time, gcScript, this);
            }
        }

        void KillSaves()
        {
            foreach (Transform child in slotPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
        #endregion
        public void NewSave()
        {
            gameController.Save(inputField.text);
            DisplaySaves();
        }

        public void DeleteSave(string SaveName)
        {
            SaveSystem.DeleteGame(SaveName);
            DisplaySaves();
        }

        #endregion
    }
}