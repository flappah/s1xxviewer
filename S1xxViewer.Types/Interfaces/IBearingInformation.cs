namespace S1xxViewer.Types.Interfaces
{
    public interface IBearingInformation : IComplexType
     {
        string CardinalDirection { get; set; }
        double Distance { get; set; }
        IInformation[] Information { get; set; }
        IOrientation Orientation { get; set; }
        double[] SectorBearing { get; set; }
    }
}