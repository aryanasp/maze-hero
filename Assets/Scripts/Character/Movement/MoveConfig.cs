using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterMoveConfig", menuName = "Game/Character/Move Config", order = 0)]
    public class MoveConfig : ScriptableObject
    {
        public int moveSpeed;
    }
}