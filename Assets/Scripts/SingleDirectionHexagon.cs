namespace Glasshouse
{
    public class SingleDirectionHexagon : Hexagon
    {
        public override void UpdateSprite()
        {
            // Update the sprite for single direction hexagon based on rotation and power state
        }

        public void TogglePower()
        {
            if (isPowered)
                PowerDown();
            else
                PowerUp(1); // Example power value
        }
    }


}