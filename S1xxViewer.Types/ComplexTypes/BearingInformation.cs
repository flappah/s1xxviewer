﻿using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class BearingInformation : ComplexTypeBase, IBearingInformation
    {
        public string CardinalDirection { get; set; }
        public double Distance { get; set; }
        public IInformation[] Information { get; set; }
        public IOrientation Orientation { get; set; }
        public double[] SectorBearing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new BearingInformation
            {
                CardinalDirection = CardinalDirection,
                Distance = Distance,
                Information = Information == null
                    ? new Information[0]
                    : Array.ConvertAll(Information, i => i.DeepClone() as IInformation),
                Orientation = Orientation == null
                    ? new Orientation()
                    : Orientation.DeepClone() as IOrientation,
                SectorBearing = SectorBearing == null
                    ? new double[0]
                    : Array.ConvertAll(SectorBearing, sb => sb)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IComplexType FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            var cardinalDirectionNode = node.FirstChild.SelectSingleNode("cardinalDirection", mgr);
            if (cardinalDirectionNode != null && cardinalDirectionNode.HasChildNodes)
            {
                CardinalDirection = cardinalDirectionNode.FirstChild.InnerText;
            }

            var distanceNode = node.FirstChild.SelectSingleNode("distance", mgr);
            if (distanceNode != null && distanceNode.HasChildNodes)
            {
                double distance;
                if (!double.TryParse(distanceNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out distance))
                {
                    distance = 0.0;
                }
                Distance = distance;
            }

            var informationNodes = node.FirstChild.SelectNodes("information", mgr);
            if (informationNodes != null && informationNodes.Count > 0)
            {
                var informations = new List<Information>();
                foreach (XmlNode informationNode in informationNodes)
                {
                    if (informationNode != null && informationNode.HasChildNodes)
                    {
                        var newInformation = new Information();
                        newInformation.FromXml(informationNode.FirstChild, mgr);
                        informations.Add(newInformation);
                    }
                }
                Information = informations.ToArray();
            }

            var orientationNode = node.FirstChild.SelectSingleNode("orientation", mgr);
            if (orientationNode != null && orientationNode.HasChildNodes)
            {
                Orientation = new Orientation();
                Orientation.FromXml(orientationNode.FirstChild, mgr);
            }

            var sectorBearingNodes = node.FirstChild.SelectNodes("sectorBearing", mgr);
            if (sectorBearingNodes != null && sectorBearingNodes.Count > 0)
            {
                var bearings = new List<double>();
                foreach(XmlNode sectorBearingNode in sectorBearingNodes)
                {
                    if (sectorBearingNode != null && sectorBearingNode.HasChildNodes)
                    {
                        double bearing;
                        if (!double.TryParse(sectorBearingNode.FirstChild.InnerText, NumberStyles.Float, new CultureInfo("en-US"), out bearing))
                        {
                            bearing = 0.0;
                        }
                        bearings.Add(bearing);
                    }
                }
                SectorBearing = bearings.ToArray();
            }

            return this;
        }
    }
}
