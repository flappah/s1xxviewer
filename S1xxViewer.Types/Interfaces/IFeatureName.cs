namespace S1xxViewer.Types.Interfaces
{
    public interface IFeatureName : IComplexType
    {
        bool DisplayName { get; set; }
        string Language { get; set; }
        string Name { get; set; }
    }
}