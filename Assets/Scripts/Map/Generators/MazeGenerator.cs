using System.Collections;
using BaseClass;
using UnityEngine;
using Zenject;

namespace Map
{
    public class MazeGenerator : JobBehaviour
    {
        [SerializeField] private MazeConfig mazeConfig;
        
        [Inject] public MapModel MapModel;

        protected override IEnumerator StartJob()
        {
            MapModel.IsMazeGenerated = false;
            yield return new WaitUntil(() => MapModel.IsTilesGenerated);
                
        }
    }
}