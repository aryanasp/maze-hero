using System;
using System.Linq;
using Map;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Design.Screens.Map
{
    public class TileDrawer : MonoBehaviour
    {
        [SerializeField] public DesignModeCommands command;
        [SerializeField] public BlockType blockType;

        [Inject] private DesignModel _designModel;
        [Inject] private MapModel _mapModel;
        [Inject] private MazeConfig _mazeConfig;

        private IMapDesignModeCommand _currentCommand;
        private TileModel _indicatedTileModel;

        public void Update()
        {
            if (!_designModel.IsDesignMode) return;
            if (!Input.GetMouseButton(0)) return;
            Debug.Log("mouse clicked");
            var indicatedPosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_indicatedTileModel == null || !MapModel.IsInTileBoundary(_indicatedTileModel, indicatedPosition))
            {
                Debug.Log("Change Tile");
                FindIndicatedTile(indicatedPosition);
            }

            if (_currentCommand == null || _currentCommand.GetCommand() != command)
            {
                _currentCommand = FindCurrentCommand();
            }

            _currentCommand.DoAction(_indicatedTileModel, blockType);
        }

        private IMapDesignModeCommand FindCurrentCommand()
        {
            switch (command)
            {
                case DesignModeCommands.Select:
                    _currentCommand = new EditMapDesignModeCommand(_mapModel, _mazeConfig);
                    break;
                case DesignModeCommands.Edit:
                    _currentCommand = new EditMapDesignModeCommand(_mapModel, _mazeConfig);
                    break;
                case DesignModeCommands.Delete:
                    _currentCommand = new EditMapDesignModeCommand(_mapModel, _mazeConfig);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _currentCommand;
        }

        private void FindIndicatedTile(Vector3 indicatedPosition)
        {
            foreach (var pair in
                     _mapModel.Tiles.Where(pair => MapModel.IsInTileBoundary(pair.Value, indicatedPosition)))
            {
                _indicatedTileModel = pair.Value;
            }
        }
#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            if (!_designModel.IsDesignMode) return;
            EditorUtility.SetDirty(_mazeConfig);
            _mazeConfig.mapObjects.Clear();
            foreach (var tilePair in _mapModel.Tiles)
            {
                if (tilePair.Value.HasBlock)
                {
                    _mazeConfig.mapObjects.Add(new MapObject
                    {
                        blockType = tilePair.Value.BlockType,
                        position = tilePair.Key
                    });
                }
            }
        }
#endif
    }
}