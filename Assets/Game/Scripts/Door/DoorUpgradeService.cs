using Zenject;

public class DoorUpgradeService 
{
    private readonly DoorUpgrade _leftDoorUpgrade;
    private readonly DoorUpgrade _rightDoorUpgrade;

    public DoorUpgradeService([Inject(Id = DoorID.LeftDoor)] DoorUpgrade left, [Inject(Id = DoorID.RightDoor)] DoorUpgrade right)
    {
        _leftDoorUpgrade = left;
        _rightDoorUpgrade = right;
    }

    public void UpgradeBothDoors()
    {
        _leftDoorUpgrade.Upgrade();
        _rightDoorUpgrade.Upgrade();
    }
}