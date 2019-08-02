namespace S1xxViewer.Types.Interfaces
{
    public interface ITextPlacement : IGeoFeature
    {
        double FlipBearing { get; set; }
        int ScaleMinimum { get; set; }
        string Text { get; set; }
        string TextJustification { get; set; }
        string TextType { get; set; }
    }
}