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
        [SerializeField] public DesignTileCommands command;
        [SerializeField] public BlockType blockType;

        [Inject] private DesignModel _designModel;
        [Inject] private MapModel _mapModel;
        [Inject] private MazeConfig _mazeConfig;

        private ITileDesignCommand _leftMouseCommand;
        private ITileDesignCommand _rightMouseCommand;
        private TileModel _indicatedTileModel;

        private void Start()
        {
            _rightMouseCommand = new DeleteBlockDesignCommand();
        }

        public void Update()
        {
            if (!_designModel.IsDesignMode) return;
            HandleMouseLeftClickAction();
            HandleMouseRightClickAction();
        }

        private void HandleMouseRightClickAction()
        {
            if (!Input.GetMouseButton(1)) return;
            Debug.Log("mouse clicked");
            var indicatedPosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_indicatedTileModel == null || !MapModel.IsInTileBoundary(_indicatedTileModel, indicatedPosition))
            {
                Debug.Log("Change Tile");
                FindIndicatedTile(indicatedPosition);
            }
            _rightMouseCommand.DoAction(_indicatedTileModel, BlockType.None);
        }

        private void HandleMouseLeftClickAction()
        {
            if (!Input.GetMouseButton(0)) return;
            Debug.Log("mouse clicked");
            var indicatedPosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_indicatedTileModel == null || !MapModel.IsInTileBoundary(_indicatedTileModel, indicatedPosition))
            {
                Debug.Log("Change Tile");
                FindIndicatedTile(indicatedPosition);
            }

            if (_leftMouseCommand == null || _leftMouseCommand.GetCommand() != command)
            {
                _leftMouseCommand = FindCurrentCommand();
            }

            _leftMouseCommand.DoAction(_indicatedTileModel, blockType);
        }

        private ITileDesignCommand FindCurrentCommand()
        {
            if (command == DesignTileCommands.Delete)
            {
                command = DesignTileCommands.Select;
            }
            switch (command)
            {
                case DesignTileCommands.Select:
                    _leftMouseCommand = new EditBlockDesignCommand(_mapModel, _mazeConfig);
                    break;
                case DesignTileCommands.Edit:
                    _leftMouseCommand = new EditBlockDesignCommand(_mapModel, _mazeConfig);
                    break;
                case DesignTileCommands.Delete:
                    _leftMouseCommand = new DeleteBlockDesignCommand();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _leftMouseCommand;
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