namespace S1xxViewer.Types.Interfaces
{
    public interface IPaperChart : IChartProduct
    {
        string FrameDimensions { get; set; }
        IPrintInformation PrintInformation { get; set; }
    }
}