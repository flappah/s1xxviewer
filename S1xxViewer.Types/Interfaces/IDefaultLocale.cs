namespace S1xxViewer.Types.Interfaces
{
    public interface IDefaultLocale : IComplexType
    {
        string Language { get; set; }
        string CharacterEncoding { get; set; }
        string Country { get; set; }
    }
}
