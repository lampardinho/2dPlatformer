namespace Assets.Scripts
{
    public class MediumAsteroid : AbstractAsteroid
    {
        private void OnEnable()
        {
            Health = 4;
            MoveSpeed = 3;
        }
    }
}