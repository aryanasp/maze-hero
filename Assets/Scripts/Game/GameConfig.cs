﻿using System.Collections.Generic;
using Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [MinValue(0.02d)]
        public float updateEpochTime;
        public int roundDuration;
        public int roundCount;
    }
}