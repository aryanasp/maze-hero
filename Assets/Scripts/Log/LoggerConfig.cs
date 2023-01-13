using UnityEngine;

namespace Log
{
    [CreateAssetMenu(fileName = "LoggerConfig", menuName = "Game/Monitor/Logger Config", order = 0)]
    public class LoggerConfig : ScriptableObject
    {
        public bool log;
        public bool warnings;
        public bool errors;
        public bool exceptions;
    }
}