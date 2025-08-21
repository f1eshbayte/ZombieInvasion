using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<ShootSignal>();
        Container.DeclareSignal<MoveLeftSignal>();
        Container.DeclareSignal<MoveRightSignal>();
    }
}