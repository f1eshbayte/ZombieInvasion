using UnityEngine.Events;
using Zenject;

public class DoorService 
{
    private readonly DoorUpgrade _leftDoorUpgrade;
    private readonly DoorUpgrade _rightDoorUpgrade;

    public event UnityAction OnDoorsUpgraded; // новое событие
    
    public DoorService([Inject(Id = DoorID.LeftDoor)] DoorUpgrade left, [Inject(Id = DoorID.RightDoor)] DoorUpgrade right)
    {
        _leftDoorUpgrade = left;
        _rightDoorUpgrade = right;
    }

    public void UpgradeBothDoors()
    {
        _leftDoorUpgrade.Upgrade();
        _rightDoorUpgrade.Upgrade();
        OnDoorsUpgraded?.Invoke(); // уведомляем всех подписчиков
    }

    public void RepairBothDoors()
    {
        _leftDoorUpgrade.RepairDoor();
        _rightDoorUpgrade.RepairDoor();
    }
}