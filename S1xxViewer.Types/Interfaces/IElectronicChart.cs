using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface IElectronicChart : IChartProduct
    {
        string[] DatasetName { get; set; }
        IReferenceSpecification ProductSpecification { get; set; }
        DateTime UpdateDate { get; set; }
        int UpdateNumber { get; set; }
    }
}