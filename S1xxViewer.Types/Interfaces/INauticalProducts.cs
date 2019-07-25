namespace S1xxViewer.Types.Interfaces
{
    public interface INauticalProducts : IProduct
    {
        string DataSetName { get; set; }
        string Keywords { get; set; }
        IOnlineResource OnlineResource { get; set; }
        IReferenceSpecification ProductSpecification { get; set; }
        string PublicationNumber { get; set; }
        IReferenceSpecification ServiceDesign { get; set; }
        IReferenceSpecification ServiceSpecification { get; set; }
        string ServiceStatus { get; set; }
        string Version { get; set; }
    }
}