namespace S1xxViewer.Types.Interfaces
{
    public interface IProductSpecification : IComplexType
    {
        string Date { get; set; }
        string ISSN { get; set; }
        string Name { get; set; }
        string Version { get; set; }
    }
}
