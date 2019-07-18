﻿using S1xxViewer.Types.Interfaces;
using System.Xml;
using System;

namespace S1xxViewer.Types
{
    public abstract class MetaFeatureBase : IFeature
    {
        public string Id { get; set; }

        public abstract IFeature DeepClone();
        public abstract IFeature FromXml(XmlNode node, XmlNamespaceManager mgr);
    }
}
