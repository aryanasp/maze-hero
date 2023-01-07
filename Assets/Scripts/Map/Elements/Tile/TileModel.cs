using UnityEngine;

namespace Map
{
    
    [System.Serializable]
    public struct Boundary
    {
        public Vector2 minPoint;
        public Vector2 maxPoint;
    }
    public class TileModel
    {
        public Transform Transform;
        public Boundary Boundary;
        
        public bool HasBlock;
        public BlockType BlockType;
    }
}