﻿using System;

namespace S1xxViewer.Types.Interfaces
{
    public interface IGraphic : IComplexType
    {
        IBearingInformation BearingInformation { get; set; }
        string[] PictorialRepresentation { get; set; }
        string PictureCaption { get; set; }
        string PictureInformation { get; set; }
        string SourceDate { get; set; }
    }
}