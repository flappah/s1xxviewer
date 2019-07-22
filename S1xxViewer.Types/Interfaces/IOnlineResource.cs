namespace S1xxViewer.Types.Interfaces
{
    public interface IOnlineResource : IComplexType
    {
        string Linkage { get; set; }
        string NameOfResource { get; set; }
        string OnlineDescription { get; set; }
    }
}