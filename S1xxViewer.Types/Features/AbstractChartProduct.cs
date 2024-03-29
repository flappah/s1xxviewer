﻿using S1xxViewer.Types.Interfaces;
using System.Collections.Generic;
using System.Xml;

namespace S1xxViewer.Types.Features
{
    public abstract class AbstractChartProduct : CatalogueElements, IAbstractChartProduct
    {
        public string ChartNumber { get; set; }
        public string DistributionStatus { get; set; }
        public string[] CompilationScale { get; set; }
        public string SpecificUsage { get; set; }
        public string ProducerCode { get; set; }
        public string OriginalChartNumber { get; set; }
        public string ProducerNation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public override IFeature FromXml(XmlNode node, XmlNamespaceManager mgr)
        {
            if (node == null || !node.HasChildNodes) return this;

            base.FromXml(node, mgr);

            //public string ChartNumber { get; set; }
            var chartNumberNode = node.FirstChild.SelectSingleNode("chartNumber", mgr);
            if (chartNumberNode != null && chartNumberNode.HasChildNodes)
            {
                ChartNumber = chartNumberNode.FirstChild.InnerText;
            }

            //public string DistributionStatus { get; set; }
            var distributionStatusNode = node.FirstChild.SelectSingleNode("distributionStatus", mgr);
            if (distributionStatusNode != null && distributionStatusNode.HasChildNodes)
            {
                DistributionStatus = distributionStatusNode.FirstChild.InnerText;
            }

            //public string CompilationScale { get; set; }
            var compilationScaleNodes = node.FirstChild.SelectNodes("compilationScale", mgr);
            if (compilationScaleNodes != null && compilationScaleNodes.Count > 0)
            {
                var compilationScales = new List<string>();
                foreach (XmlNode categoryOfCargoNode in compilationScaleNodes)
                {
                    if (categoryOfCargoNode != null && categoryOfCargoNode.HasChildNodes)
                    {
                        compilationScales.Add(categoryOfCargoNode.InnerText);
                    }
                }
                CompilationScale = compilationScales.ToArray();
            }

            //public string SpecificUsage { get; set; }
            var specificUsageNode = node.FirstChild.SelectSingleNode("specificUsage", mgr);
            if (specificUsageNode != null && specificUsageNode.HasChildNodes)
            {
                SpecificUsage = specificUsageNode.FirstChild.InnerText;
            }

            //public string ProducerCode { get; set; }
            var producerCodeNode = node.FirstChild.SelectSingleNode("producerCode", mgr);
            if (producerCodeNode != null && producerCodeNode.HasChildNodes)
            {
                ProducerCode = producerCodeNode.FirstChild.InnerText;
            }

            //public string OriginalChartNumber { get; set; }
            var originalChartNumberNode = node.FirstChild.SelectSingleNode("originalChartNumber", mgr);
            if (originalChartNumberNode != null && originalChartNumberNode.HasChildNodes)
            {
                OriginalChartNumber = originalChartNumberNode.FirstChild.InnerText;
            }

            //public string ProducerNation { get; set; }
            var producerNationNode = node.FirstChild.SelectSingleNode("producerNation", mgr);
            if (producerNationNode != null && producerNationNode.HasChildNodes)
            {
                ProducerNation = producerNationNode.FirstChild.InnerText;
            }

            return this;
        }
    }
}
