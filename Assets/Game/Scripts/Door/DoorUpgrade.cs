public class DoorUpgrade
{
    private int _currentLevel = 1;

    private readonly DoorData _wooderDoor;
    private readonly DoorData _strongWooderDoor;
    private readonly DoorData _ironDoor;    
    
    private Door _doorInstance; // ссылка на текущую дверь
    

    public DoorUpgrade(DoorData wooden, DoorData strongWooden, DoorData iron)
    {
        _wooderDoor = wooden;
        _strongWooderDoor = strongWooden;
        _ironDoor = iron;
    }

    public DoorData GetCurrentDoorData()
    {
        return _currentLevel switch
        {
            1 => _wooderDoor,
            2 => _strongWooderDoor,
            3 => _ironDoor,
            _ => _wooderDoor
        };
    }
    public void RegisterDoor(Door door)
    {
        _doorInstance = door;
        _doorInstance.SetData(GetCurrentDoorData());
    }
    public void Upgrade()
    {
        if (_currentLevel < 3)
        {
            _currentLevel++;
            if (_doorInstance != null)
            {
                _doorInstance.SetData(GetCurrentDoorData());
                RepairDoor();
            }
        }
    }

    public void RepairDoor()
    {
        _doorInstance.Repair();
    }
}
