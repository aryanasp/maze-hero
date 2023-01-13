using System;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Zenject;

namespace Ai.GeneticAlgorithm.TimeAsState
{
    public class AiGaTimeAsStateLogic : MonoBehaviour, IAiGaLogic
    {
        [Inject] private AiGaManager _aiGaManager;
        
        [Inject] private GameConfig _gameConfig;
        [Inject] private GameModel _gameModel;
        private List<ITimeAsStateAiGaActor> _aiActors;

        private void Awake()
        {
            Disable();
            _aiGaManager.Subscribe(this);
        }

        private void OnDestroy()
        {
            _aiGaManager.UnSubscribe(this);
        }

        private void OnEnable()
        {
            _aiActors = new List<ITimeAsStateAiGaActor>();
            _aiActors.ForEach(item => _gameModel.OnRoundTimePassedUpdate += item.AssignCurrentStateActionsToCharacter);
        }

        private void OnDisable()
        {
            _aiActors.ForEach(item => _gameModel.OnRoundTimePassedUpdate -= item.AssignCurrentStateActionsToCharacter);
            _aiActors.Clear();
        }

        private void FixedUpdate()
        {
            if (CheckIfGameTimeFinished())
            {
                _aiActors.ForEach(item => item.Halt());
            }
        }


        private bool CheckIfGameTimeFinished()
        {
            return _gameModel.CurrentRound >= _gameConfig.roundDuration;
        }

        #region IAiLogic

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public AiType GetAiType()
        {
            return AiType.TimeAsState;
        }

        #endregion
    }
}