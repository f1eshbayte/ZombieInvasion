using UnityEngine;
using Zenject;

public class DoorUpgradeManager
{
    private readonly DoorUpgrade _leftDoorUpgrade;
    private readonly DoorUpgrade _rightDoorUpgrade;

    public DoorUpgradeManager(
        [Inject(Id = DoorID.LeftDoor)] DoorUpgrade left,
        [Inject(Id = DoorID.RightDoor)] DoorUpgrade right)
    {
        _leftDoorUpgrade = left;
        _rightDoorUpgrade = right;
    }

    public void UpgradeBothDoors()
    {
        _leftDoorUpgrade.Upgrade();
        _rightDoorUpgrade.Upgrade();

        Debug.Log("Обе двери апгрейднуты!");
    }
}