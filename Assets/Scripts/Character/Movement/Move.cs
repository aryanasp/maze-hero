using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Game;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using MoveDirection = UnityEngine.EventSystems.MoveDirection;
using Logger = Log.Logger;

namespace Character
{
    public class Move : MonoBehaviour
    {
        [Inject] private GameModel _gameModel;
        [Inject] private MoveModel _moveModel;
        
        [SerializeField] private Transform moveObject;
        private Dictionary<MoveDirection, Vector2> _moveDirectionVectors;

        private void Start()
        {
            InitMoveDirectionVectors();
        }
        

        private void InitMoveDirectionVectors()
        {
            _moveDirectionVectors = new Dictionary<MoveDirection, Vector2>
            {
                [MoveDirection.Up] = Vector2.up,
                [MoveDirection.Down] = Vector2.down,
                [MoveDirection.Right] = Vector2.right,
                [MoveDirection.Left] = Vector2.left
            };
        }
        
        // Update is called once per frame
        void Update()
        {
            Logger.Log($"[Move]: {_moveModel.IsMoving}");
            Logger.Log($"[Move]: {_moveModel.CurrentMoveSpeed}");
            if (_gameModel.IsPausing)
            {
                return;
            }
            if (CheckIfCharacterShouldMove()) return;
            Transport();
        }

        private void Transport()
        {
            var position = (Vector2) moveObject.transform.localPosition;
            position += _moveModel.CurrentMoveSpeed * Time.fixedDeltaTime *
                        _moveDirectionVectors[_moveModel.CurrentDirection];
            moveObject.transform.localPosition = position;
        }

        private bool CheckIfCharacterShouldMove()
        {
            return !_moveModel.IsMoving;
        }
    }

}