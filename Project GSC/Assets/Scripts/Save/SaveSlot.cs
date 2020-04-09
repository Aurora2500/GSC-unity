using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Assets.Scripts.Managers;
using Assets.Scripts.UI.Menu;

namespace Assets.Scripts.Save
{
    public class SaveSlot : MonoBehaviour
    {
        public GameObject textName;
        string sName;
        public GameObject time;
        GameController gc;
        PauseMenu pm;

        public void SetValues(string saveName, string saveTime, GameController gcScript, PauseMenu pmScript)
        {
            var nText = textName.GetComponent<TextMeshProUGUI>();
            sName = saveName;
            nText.SetText(saveName);
            var tText = time.GetComponent<TextMeshProUGUI>();
            tText.SetText(saveTime);

            gc = gcScript;
            pm = pmScript;
        }

        public void Save()
        {
            gc.Save(sName);
        }
        public void Load()
        {
            gc.Load(sName);
        }

        public void DeleteSave()
        {
            pm.DeleteSave(sName);
        }
    }
}