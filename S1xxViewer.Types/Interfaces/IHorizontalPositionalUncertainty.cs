namespace S1xxViewer.Types.Interfaces
{
    public interface IHorizontalPositionalUncertainty : IComplexType
    {
        double UncertaintyFixed { get; set; }
        double UncertaintyVariable { get; set; }
    }
}