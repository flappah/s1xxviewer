using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface IElectronicChart : IChartProduct
    {
        string[] DatasetName { get; set; }
        IReferenceSpecification ProductSpecification { get; set; }
        string UpdateDate { get; set; }
        string UpdateNumber { get; set; }
    }
}