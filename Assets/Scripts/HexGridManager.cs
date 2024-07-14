using UnityEngine;

namespace Glasshouse
{
    public class HexGridManager : MonoBehaviour
    {
        public int rows;
        public int columns;
        public GameObject hexagonPrefab;

        private Hexagon[,] hexagons;

        void Start()
        {
            CreateGrid();
        }

        void CreateGrid()
        {
            hexagons = new Hexagon[rows, columns];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Vector3 position = CalculateHexPosition(x, y);
                    GameObject hexObject = Instantiate(hexagonPrefab, position, Quaternion.identity);
                    Hexagon hexagon = hexObject.GetComponent<Hexagon>();
                    hexagon.gridPosition = new Vector2Int(x, y);
                    hexagons[x, y] = hexagon;
                }
            }
        }

        Vector3 CalculateHexPosition(int x, int y)
        {
            float xOffset = (x % 2 == 0) ? 0 : 0.5f;
            return new Vector3(x * 1.0f, 0, y * 0.866f + xOffset); // Adjust scale and position as needed
        }

        public Hexagon GetHexagon(Vector2Int position)
        {
            if (position.x >= 0 && position.x < rows && position.y >= 0 && position.y < columns)
            {
                return hexagons[position.x, position.y];
            }
            return null;
        }
    }


}