using Character;
using Game;
using UnityEngine.EventSystems;
using Zenject;

namespace Ai.GeneticAlgorithm.TimeAsState
{
    public class MovementTimeAsStateAiGaActor : ITimeAsStateAiGaActor
    {
        private readonly GameConfig _gameConfig;
        private readonly GameModel _gameModel;
        private readonly MoveModel _characterCurrentMoveModel;
        private MoveModel[] _moveDna;

        public MovementTimeAsStateAiGaActor(GameConfig gameConfig, GameModel gameModel, MoveModel characterCurrentMoveModel)
        {
            _gameConfig = gameConfig;
            _gameModel = gameModel;
            _characterCurrentMoveModel = characterCurrentMoveModel;
            InitializeDna();
        }

        private void InitializeDna()
        {
            _moveDna = new MoveModel[_gameConfig.roundDuration];
            for (int i = 0; i < _gameConfig.roundDuration; i++)
            {
                _moveDna[i] = new MoveModel();
            }
        }

        public void AssignCurrentStateActionsToCharacter()
        {
            _characterCurrentMoveModel.CurrentMoveSpeed = _moveDna[_gameModel.CurrentRoundTimePassed].CurrentMoveSpeed;
            _characterCurrentMoveModel.CurrentDirection = _moveDna[_gameModel.CurrentRoundTimePassed].CurrentDirection;
            _characterCurrentMoveModel.IsMoving = _moveDna[_gameModel.CurrentRoundTimePassed].IsMoving;
        }

        public void Halt()
        {
            _characterCurrentMoveModel.CurrentMoveSpeed = 0;
            _characterCurrentMoveModel.CurrentDirection = MoveDirection.Down;
            _characterCurrentMoveModel.IsMoving = false;
        }
    }
}