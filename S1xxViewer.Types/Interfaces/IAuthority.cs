namespace S1xxViewer.Types.Interfaces
{
    public interface IAuthority : IInformationFeature, IS122Feature
    {
        string CategoryOfAuthority { get; set; }
        ITextContent TextContent { get; set; }

        ILink[] Links { get; set; }
    }
}