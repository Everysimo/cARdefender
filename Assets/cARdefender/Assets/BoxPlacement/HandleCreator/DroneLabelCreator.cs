using System.Collections.Generic;

namespace cARdefender.Tests.BoxPlacement.HandleCreator
{
    public class DroneLabelCreator : HandleCreator
    {
        public override BoxConsumerHandle GetHandle()
        {
            HashSet<VeichleTypes> set = new HashSet<VeichleTypes> { VeichleTypes.CAR,VeichleTypes.TRUCK };
            return new BoxConsumerHandle(set);
        }
    }
}