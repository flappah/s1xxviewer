namespace S1xxViewer.Types.Interfaces
{
    public interface IUnderkeelAllowance : IComplexType
    {
        double UnderkeelAllowanceFixed { get; set; }
        double UnderkeelAllowanceVariableBeamBased { get; set; }
        double UnderkeelAllowanceVariableDraughtBased { get; set; }
    }
}