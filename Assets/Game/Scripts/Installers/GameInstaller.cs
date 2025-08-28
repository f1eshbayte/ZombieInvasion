using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private AidKit _aidKitPrefab;
    [SerializeField] private Menu _menu;
    public override void InstallBindings()
    {
        InstallSignalBus();
        BindPlayer();
        BindAidKit();
        BindMenu();
    }

    private void BindMenu()
    {
        Container.Bind<Menu>().FromInstance(_menu).AsSingle();
    }

    private void BindAidKit()
    {
        Container.Bind<AidKit>().FromComponentInNewPrefab(_aidKitPrefab).AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle();
    }

    private void InstallSignalBus()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ShootSignal>();
        Container.DeclareSignal<MoveLeftSignal>();
        Container.DeclareSignal<MoveRightSignal>();
        Container.DeclareSignal<HealSignal>();
    }
}