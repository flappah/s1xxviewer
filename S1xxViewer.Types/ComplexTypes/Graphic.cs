﻿using S1xxViewer.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.ComplexTypes
{
    public class Graphic : ComplexTypeBase, IGraphic
    {
        public string[] PictorialRepresentation { get; set; }
        public string PictureCaption { get; set; }
        public DateTime SourceDate { get; set; }
        public string PictureInformation { get; set; }
        public IBearingInformation BearingInformation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IComplexType DeepClone()
        {
            return new Graphic
            {
                PictorialRepresentation = PictorialRepresentation == null
                    ? new string[0]
                    : Array.ConvertAll(PictorialRepresentation, s => s),
                PictureCaption = PictureCaption,
                SourceDate = SourceDate,
                PictureInformation = PictureInformation,
                BearingInformation = BearingInformation == null
                    ? new BearingInformation()
                    : BearingInformation.DeepClone() as IBearingInformation
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
            var pictorialRepresentationNodes = node.FirstChild.SelectNodes("pictorialRepresentation");
            if (pictorialRepresentationNodes != null && pictorialRepresentationNodes.Count > 0)
            {
                var representations = new List<string>();
                foreach(XmlNode pictorialRepresentationNode in pictorialRepresentationNodes)
                {
                    if (pictorialRepresentationNode != null && pictorialRepresentationNode.HasChildNodes)
                    {
                        var representation = pictorialRepresentationNode.FirstChild.InnerText;
                        representations.Add(representation);
                    }
                }
                PictorialRepresentation = representations.ToArray();
            }

            var pictureCaptionNode = node.FirstChild.SelectSingleNode("pictureCaptionNode");
            if (pictureCaptionNode != null && pictureCaptionNode.HasChildNodes)
            {
                PictureCaption = pictureCaptionNode.FirstChild.InnerText;
            }

            var sourceDateNode = node.FirstChild.SelectSingleNode("sourceDate");
            if (sourceDateNode != null && sourceDateNode.HasChildNodes)
            {
                DateTime sourceDate;
                if (!DateTime.TryParse(sourceDateNode.FirstChild.InnerText, out sourceDate))
                {
                    sourceDate = DateTime.MinValue;
                }
                SourceDate = sourceDate;
            }

            var pictureInformationNode = node.FirstChild.SelectSingleNode("pictureInformation");
            if (pictureInformationNode != null && pictureInformationNode.HasChildNodes)
            {
                PictureInformation = pictureInformationNode.FirstChild.InnerText;
            }

            var bearingInformationNode = node.FirstChild.SelectSingleNode("bearingInformation");
            if (bearingInformationNode != null && bearingInformationNode.HasChildNodes)
            {
                var bearingInformation = new BearingInformation();
                bearingInformation.FromXml(bearingInformationNode.FirstChild, mgr);
                BearingInformation = bearingInformation;
            }

            return this;
        }
    }
}
