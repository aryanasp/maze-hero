using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ai.GeneticAlgorithm
{
    public class AiGaManager : MonoBehaviour
    {
        private readonly List<IAiGaLogic> _aiLogics = new List<IAiGaLogic>();
        [Inject] private AiGaConfig _aiGaConfig;
        
        private void Start()
        {
            if (CheckAiConfigIsActive())
            {
                var activeType = _aiGaConfig.aiType;
                _aiLogics.Find(item => item.GetAiType() == activeType).Enable();
            }
        }

        private bool CheckAiConfigIsActive()
        {
            return _aiGaConfig.isActive;
        }

        public void Subscribe(IAiGaLogic aiGaLogic)
        {
            _aiLogics.Add(aiGaLogic);
        }

        public void UnSubscribe(IAiGaLogic aiGaLogic)
        {
            _aiLogics.Remove(aiGaLogic);
        }
    }
}