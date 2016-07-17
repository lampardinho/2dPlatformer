public class MediumAsteroid : AbstractAsteroid
{
    protected override void Init()
    {
        base.Init();
        _health = 4;
        _moveSpeed = 3;
    }
}