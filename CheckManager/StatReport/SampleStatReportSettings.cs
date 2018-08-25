using SSIT.DataField;
using SSIT.EncodeBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SSIT.QM.CheckManager.StatReport
{
    public class SampleStatReportSettings : XMLSettingFile<SampleStatReportSettings>
    {
        public SampleStatReportSettings()
        {
            StatFields = new FieldCollection();
            HeadFields = new FieldCollection();
            DecisionsFields = new FieldCollection();
        }

        public override string FileName
        {
            get { return "SampleStatReportSettings.config"; }
        }

        [XmlArray(ElementName = "statfields")]
        [XmlArrayItem(ElementName = "field", Type = typeof(DataFieldAttribute))]
        public FieldCollection StatFields{get;set;}     

        [XmlArray(ElementName = "headfields")]
        [XmlArrayItem(ElementName = "field", Type = typeof(DataFieldAttribute))]
        public FieldCollection HeadFields{get;set;}

        [XmlArray(ElementName = "decisionsfields")]
        [XmlArrayItem(ElementName = "field", Type = typeof(DataFieldAttribute))]
        public FieldCollection DecisionsFields { get; set; }
        
    }
}
