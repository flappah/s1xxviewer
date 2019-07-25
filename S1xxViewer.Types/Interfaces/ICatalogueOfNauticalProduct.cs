namespace S1xxViewer.Types.Interfaces
{
    public interface ICatalogueOfNauticalProduct : IGeoFeature
    {
        int EditionNumber { get; set; }
        IGraphic[] Graphic { get; set; }
        string IssueDate { get; set; }
        int MarineResourceName { get; set; }
    }
}