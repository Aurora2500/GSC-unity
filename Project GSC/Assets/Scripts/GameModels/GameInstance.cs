﻿using Assets.Scripts.GameModels.Astronomy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSC
{
    public class GameInstance
    {
        public Galaxy galaxy;

        public GameInstance()
        {
            galaxy = new Galaxy();
        }

        public GameInstance(GameData data)
        {
            galaxy = new Galaxy(data.galaxy);
        }

        public void Destroy()
        {
            galaxy.Destroy();
        }
    }
}