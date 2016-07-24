namespace Assets.Scripts
{
    public class SmallAsteroid : AbstractAsteroid
    {
        private void OnEnable()
        {
            Health = 3;
            MoveSpeed = 4;
        }
    }
}