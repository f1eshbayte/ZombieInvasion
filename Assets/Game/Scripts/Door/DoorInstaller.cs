using UnityEngine;
using Zenject;

public class DoorInstaller : MonoInstaller
{
    [Header("Левая дверь")]
    [SerializeField] private Door _leftDoor;
    [SerializeField] private DoorData _leftWoodenDoor;
    [SerializeField] private DoorData _leftStrongWoodenDoor;
    [SerializeField] private DoorData _leftIronDoor;

    [Header("Правая дверь")]
    [SerializeField] private Door _rightDoor;
    [SerializeField] private DoorData _rightWoodenDoor;
    [SerializeField] private DoorData _rightStrongWoodenDoor;
    [SerializeField] private DoorData _rightIronDoor;

    public override void InstallBindings()
    {
        BindDoor(DoorID.LeftDoor, _leftDoor, _leftWoodenDoor, _leftStrongWoodenDoor, _leftIronDoor);
        BindDoor(DoorID.RightDoor, _rightDoor, _rightWoodenDoor, _rightStrongWoodenDoor, _rightIronDoor);
        Container.Bind<DoorUpgradeManager>().AsSingle();
    }

    private void BindDoor(DoorID id, Door door, DoorData wooden, DoorData strongWooden, DoorData iron)
    {
        var upgrade = new DoorUpgrade(wooden, strongWooden, iron);
        upgrade.RegisterDoor(door);

        Container.Bind<DoorUpgrade>().WithId(id).FromInstance(upgrade);
        Container.Bind<DoorData>().WithId(id).FromMethod(ctx => upgrade.GetCurrentDoorData()).AsTransient();

    }
}
