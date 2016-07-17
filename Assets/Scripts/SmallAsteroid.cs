public class SmallAsteroid : AbstractAsteroid
{
    protected override void Init()
    {
        base.Init();
        _health = 3;
        _moveSpeed = 4;
    }
}