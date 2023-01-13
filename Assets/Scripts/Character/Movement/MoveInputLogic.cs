using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Logger = Log.Logger;

namespace Character
{
    public class MoveLogic : MonoBehaviour
    {
        [Inject] private MoveConfig _moveConfig;
        [Inject] private MoveModel _moveModel;

        private void Update()
        {
            Logger.Log($"[MoveLogic]: {_moveModel.IsMoving}");
            Logger.Log($"[MoveLogic]: {_moveModel.CurrentMoveSpeed}");
            var axisDirection = "Horizontal";
            if (Input.GetButton(axisDirection))
            {
                Move(axisDirection, MoveDirection.Left, MoveDirection.Right);
                return;
            }

            axisDirection = "Vertical";
            if (Input.GetButton(axisDirection))
            {
                Move(axisDirection, MoveDirection.Down, MoveDirection.Up);
                return;
            }

            Halt();
        }

        private void Halt()
        {
            _moveModel.CurrentMoveSpeed = 0;
            _moveModel.CurrentDirection = MoveDirection.None;
            _moveModel.IsMoving = false;
        }

        private void Move(string axisDirection, MoveDirection minusAxis, MoveDirection positiveAxis)
        {
            var direction = MoveDirection.None;
            var axis = Input.GetAxis(axisDirection);
            direction = axis >= 0 ? positiveAxis : minusAxis;
            _moveModel.CurrentMoveSpeed = (int) Mathf.Abs(_moveConfig.moveSpeed * axis);
            _moveModel.CurrentDirection = direction;
            _moveModel.IsMoving = true;
        }
    }
}