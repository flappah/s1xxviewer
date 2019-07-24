namespace S1xxViewer.Types.Interfaces
{
    public interface IUnderkeelClearanceAllowanceArea : IGeoFeature
    {
        IUnderkeelAllowance UnderkeelAllowance { get; set; }
        string WaterLevelTrend { get; set; }
    }
}