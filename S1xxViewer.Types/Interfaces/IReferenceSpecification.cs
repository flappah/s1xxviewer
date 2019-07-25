using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface IReferenceSpecification : IComplexType
    {
        DateTime Date { get; set; }
        string Name { get; set; }
        string Version { get; set; }
    }
}