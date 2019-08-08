﻿namespace S1xxViewer.Types.Interfaces
{
    public interface IHorizontalPositionalUncertainty : IComplexType
    {
        string UncertaintyFixed { get; set; }
        string UncertaintyVariable { get; set; }
    }
}