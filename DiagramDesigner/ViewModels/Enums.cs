namespace DiagramDesigner
{
    public enum ConnectionTypeEnum
    {
        Inheritance = 0,
        Association = 1,
        Aggregation = 2,
        Composition = 3
    }
    
    public enum MultiplicityTypeEnum
    {
        Unspecified = 0,
        Zero = 1,
        ZeroToOne = 2,
        ZeroToMany = 3,
        One = 4,
        OneToMany = 5,
        ManyToMany = 6
    }
}
