using RMC.Core.Architectures.Mini.Controller.Commands;

namespace cARdefender.Assets.Enemies.Drone.Scripts.Commands
{
    public class OnBoxObtainedAttachToVehicleCommand : ICommand
    {
        private bool _iAttachedToVeichle;

        OnBoxObtainedAttachToVehicleCommand(bool isAttched)
        {
            _iAttachedToVeichle = isAttched;
        }
    }
}