using System.Collections;
using BaseClass;
using UnityEngine;
using Zenject;

namespace Map
{
    public class TilesGenerator : JobBehaviour
    {
        [SerializeField] private GameObject tilePrefab;

        [Inject] public MapModel MapModel;
        private UnityEngine.Camera _camera;
        
        protected override IEnumerator StartJob()
        {
            AssignCameraAndConfigurePosition();
            MapModel.IsTilesGenerated = false;
            var tileSize = CalculateTileSize();
            var cameraSize = CalculateCameraSize();
            var tilesCountInHalfHorizontal = (int)(cameraSize.X * 0.5f / tileSize.X);
            var tileCountInHalfVertical = (int)(cameraSize.Y * 0.5f / tileSize.Y);
            GenerateTiles(tilesCountInHalfHorizontal, tileCountInHalfVertical, tileSize);
            yield return new WaitForEndOfFrame();
            MapModel.IsTilesGenerated = true;
        }

        private void AssignCameraAndConfigurePosition()
        {
            _camera = UnityEngine.Camera.main;
            _camera.transform.position = new Vector3(0, 0, 0);
        }

        private void GenerateTiles(int tilesCountInHalfHorizontal, int tileCountInHalfVertical,
            (float X, float Y) tilesize)
        {
            for (int i = -tilesCountInHalfHorizontal; i < tilesCountInHalfHorizontal + 1; i++)
            {
                for (int j = -tileCountInHalfVertical; j < tileCountInHalfVertical + 1; j++)
                {
                    var tile = Instantiate(tilePrefab, transform);
                    tile.name = $"({i}, {j})";
                    
                    var position = new Vector3(i * tilesize.X, j * tilesize.Y, 0);
                    tile.transform.position = position;
                    
                    var tileModel = ConfigureTileModel(tilesize, tile, position);
                    MapModel.Tiles[new Vector2(i, j)] = tileModel;
                }
            }
        }

        private TileModel ConfigureTileModel((float X, float Y) tilesize, GameObject tile, Vector3 position)
        {
            var tileModel = new TileModel
            {
                Transform = tile.transform,
                Boundary = new Boundary
                {
                    minPoint = new Vector2(position.x - 0.5f * tilesize.X, position.y - 0.5f * tilesize.Y),
                    maxPoint = new Vector2(position.x + 0.5f * tilesize.X, position.y + 0.5f * tilesize.Y),
                }
            };
            return tileModel;
        }

        private (float X, float Y) CalculateCameraSize()
        {
            if (_camera == null)
            {
                return (-1f, -1f);
            }
            var orthographicSize = _camera.orthographicSize;
            var cameraSizeY = 2 * orthographicSize;
            var cameraSizeX = cameraSizeY * Screen.width / Screen.height;
            return (cameraSizeX, cameraSizeY);
        }

        private (float X, float Y) CalculateTileSize()
        {
            var sprite = tilePrefab.GetComponent<SpriteRenderer>().sprite;
            var tileSizeX = (sprite.rect.width / sprite.pixelsPerUnit);
            var tileSizeY = (sprite.rect.height / sprite.pixelsPerUnit);
            return (tileSizeX, tileSizeY);
        }

    }
}
