using UnityEngine;
using Zenject;

namespace Map
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject mazePrefab;
        [SerializeField] private MazeConfig mazeConfig;

        [Inject] public MapModel MapModel;
        
        private void Start()
        {
            
        }
    }
}