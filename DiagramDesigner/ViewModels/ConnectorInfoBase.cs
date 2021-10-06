namespace DiagramDesigner
{
    public abstract class ConnectorInfoBase : INPCBase
    {
        private static double connectorWidth = 8;
        private static double connectorHeight = 8;

        public ConnectorInfoBase(ConnectorOrientation orientation)
        {
            this.Orientation = orientation;
            this.MultiplicityType = MultiplicityTypeEnum.Unspecified;
        }

        public ConnectorOrientation Orientation { get; private set; }

        private MultiplicityTypeEnum multiplicityType;
        public MultiplicityTypeEnum MultiplicityType
        {
            get { return multiplicityType; }
            set { multiplicityType = value; NotifyChanged("MultiplicityType"); }
        }


        public static double ConnectorWidth
        {
            get { return connectorWidth; }
        }

        public static double ConnectorHeight
        {
            get { return connectorHeight; }
        }
    }
}
