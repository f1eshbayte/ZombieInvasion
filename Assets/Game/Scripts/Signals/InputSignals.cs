public class ShootSignal
{
    public bool IsDown;

    public ShootSignal(bool isDown)
    {
        IsDown = isDown;
    }
}

public class MoveLeftSignal
{
    public bool IsDown;

    public MoveLeftSignal(bool isDown)
    {
        IsDown = isDown;
    }
}

public class MoveRightSignal
{
    public bool IsDown;

    public MoveRightSignal(bool isDown)
    {
        IsDown = isDown;
    }
}

public class HealSignal
{
    public bool IsDown;

    public HealSignal(bool isDown)
    {
        IsDown = isDown;
    }
}