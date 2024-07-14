using UnityEngine;

namespace Glasshouse
{
    public class Hexagon : MonoBehaviour
    {
        public Vector2Int gridPosition;
        public bool isPowered;
        public int powerValue;
        public int rotation; // 0 to 5 for the 6 directions

        public virtual void PowerUp(int value)
        {
            isPowered = true;
            powerValue = value;
            UpdateSprite();
        }

        public virtual void PowerDown()
        {
            isPowered = false;
            powerValue = 0;
            UpdateSprite();
        }

        public void Rotate(bool clockwise)
        {
            if (clockwise)
                rotation = (rotation + 1) % 6;
            else
                rotation = (rotation + 5) % 6; // (rotation - 1 + 6) % 6
            UpdateSprite();
        }

        public virtual void UpdateSprite()
        {
            // Logic to update the sprite based on the current state
        }

        public virtual void Display()
        {
            // Display hexagon on the grid with the current state and power value
        }
    }
}