using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    public override void InstallBindings()
    {
        InstallSignalBus();
        BindPlayer();
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
    }
}