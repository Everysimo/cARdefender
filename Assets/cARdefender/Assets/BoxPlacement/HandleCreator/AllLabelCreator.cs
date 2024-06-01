using System.Collections.Generic;

namespace cARdefender.Tests.BoxPlacement.HandleCreator
{
    public class AllLabelCreator : HandleCreator
    {
        public override BoxConsumerHandle GetHandle()
        {
            HashSet<VeichleTypes> set = new HashSet<VeichleTypes> { VeichleTypes.BICYCLE,VeichleTypes.BUS,VeichleTypes.TRUCK,VeichleTypes.TRAFFIC_LIGHT,VeichleTypes.CAR,VeichleTypes.STOP_SIGN,VeichleTypes.MOTORCYCLE };
            return new BoxConsumerHandle(set);
        }
    }
}