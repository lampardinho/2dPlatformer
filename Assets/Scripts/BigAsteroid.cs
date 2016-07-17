public class BigAsteroid : AbstractAsteroid
{
    protected override void Init ()
    {
        base.Init();
        _health = 1;
        _moveSpeed = 2;
    }
}
