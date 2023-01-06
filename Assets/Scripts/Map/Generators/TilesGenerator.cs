using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public class TilesGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;

        // Start is called before the first frame update
        void Start()
        {
            var tileSize = CalculateTileSize();
            var cameraSize = CalculateCameraSize();
            var tilesCountInHalfHorizontal = (int)(cameraSize.X * 0.5f / tileSize.X);
            var tileCountInHalfVertical = (int)(cameraSize.Y * 0.5f / tileSize.Y);
            GenerateMap(tilesCountInHalfHorizontal, tileCountInHalfVertical, tileSize);
        }

        private void GenerateMap(int tilesCountInHalfHorizontal, int tileCountInHalfVertical,
            (float X, float Y) tilesize)
        {
            for (int i = -tilesCountInHalfHorizontal; i < tilesCountInHalfHorizontal + 1; i++)
            {
                for (int j = -tileCountInHalfVertical; j < tileCountInHalfVertical + 1; j++)
                {
                    var tile = Instantiate(tilePrefab, transform);
                    tile.transform.position = new Vector3(i * tilesize.X, j * tilesize.Y, 0);
                    tile.name = $"({i}, {j})";
                }
            }
        }

        private (float X, float Y) CalculateCameraSize()
        {
            if (Camera.main == null)
            {
                return (-1f, -1f);
            }

            var orthographicSize = Camera.main.orthographicSize;
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
