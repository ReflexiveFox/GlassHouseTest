namespace Glasshouse
{
    public class TargetHexagon : Hexagon
    {
        public int requiredPower;

        public override void PowerUp(int value)
        {
            base.PowerUp(value);
            // Additional logic for target hexagon
        }

        public override void PowerDown()
        {
            base.PowerDown();
            // Additional logic for target hexagon
        }

        public override void UpdateSprite()
        {
            // Update the sprite for target hexagon based on power state and required power
        }
    }


}