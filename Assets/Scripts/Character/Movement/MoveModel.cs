using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character
{
    public class MoveModel
    {
        private bool _isMoving;
        private float _currentMoveSpeed;
        private MoveDirection _currentDirection;
        public Action OnMoveStateChanged = () => { };

        public bool IsMoving
        {
            get => _isMoving;
            set
            {
                if (value == _isMoving) return;
                _isMoving = value;
                OnMoveStateChanged.Invoke();
            }
        }

        public MoveDirection CurrentDirection
        {
            get => _currentDirection;
            set
            {
                if (value == _currentDirection) return;
                _currentDirection = value;
                OnMoveStateChanged?.Invoke();

            }
        }

        public float CurrentMoveSpeed
        {
            get => _currentMoveSpeed;
            set
            {
                if (Math.Abs(value - _currentMoveSpeed) < 0.05f) return;
                _currentMoveSpeed = value;
                OnMoveStateChanged.Invoke();
            }
        }
    }
}