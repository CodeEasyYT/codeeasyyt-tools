using UnityEngine;

namespace CodeEasyYT.Utilities.GridSystem
{
    public class GridGizmosViewer : MonoBehaviour
    {
        public int width;
        public int height;
        public float cellSize;
        public Color debugColor;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = debugColor;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1));
                    Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y));
                }
            }

            Gizmos.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height));
            Gizmos.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height));
        }

        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * cellSize + new Vector2(transform.position.x, transform.position.y);
        }
    }
}