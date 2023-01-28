using System;
using System.Collections.Generic;
using Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public bool doReport;
        public int roundDuration;
        public int roundCount;
    }
}