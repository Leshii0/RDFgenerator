using System;
using System.Collections.Generic;
using System.Text;

namespace RDFCompiler.ParserObj
{
    public enum ePropTypes
    {
        property,
        enumeration,
        function
    }
    public class Property
    {
        string _header = "";
        List<string> _domains = new List<string>();
        List<string> _ranges = new List<string>();
        ePropTypes _type = ePropTypes.property;
        Dictionary<string, string> renameList = new Dictionary<string, string> {
            {"Name","имеет_имя" },
            {"Add","содержит" },
            {"Attributes","атрибуты" },
            {"BaseTypesAdd","наследуется_от" },
            {"MembersAdd","содержит" },
            {"Type","имеет_тип" },
            {"ReturnType","имеет_тип" },
        };
        public ePropTypes Type
        {
            get => _type;
        }
        public string Header
        {
            get => ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header); set => _header = value;
        }
        public List<string> Domain
        {
            get => _domains;
            set => _domains = value;
        }
        public List<string> Range
        {
            get => _ranges;
            set => _ranges = value;
        }
        public Property(string header, ePropTypes type)
        {
            _header = header;
            _type = type;
        }

        public string PropertyRDF
        {
            get
            {
                if (_type == ePropTypes.property)
                {
                    var head = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " " + "rdf:type rdf:Property.";
                    var domain = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " rdf:domain ";
                    foreach (string str in _domains)
                    {
                        domain += str + ",";
                    }
                    domain = domain.Substring(0, domain.Length - 1) + ".";
                    var range = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " rdf:range ";
                    foreach (string str in _ranges)
                    {
                        range += str + ",";
                    }
                    range = range.Substring(0, range.Length - 1) + ".";

                    return head + Environment.NewLine + domain + Environment.NewLine + range;
                }
                else if (_type == ePropTypes.enumeration)
                {
                    var head = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " " + "rdf:type rdf:Property.";
                    var domain = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " rdf:domain ";
                    foreach (string str in _domains)
                    {
                        domain += str + ",";
                    }
                    domain = domain.Substring(0, domain.Length - 1) + ".";
                    var range = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " rdf:range rdfs:Container.";
                    //foreach (string str in _ranges)
                    //{
                    //    range += str + ",";
                    //}
                    //range = range.Substring(0, range.Length - 1) + ".";

                    return head + Environment.NewLine + domain + Environment.NewLine + range;
                }
                else if (_type == ePropTypes.function)
                {
                    var head = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " " + "rdf:type rdf:Property.";
                    var domain = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " rdf:domain ";
                    foreach (string str in _domains)
                    {
                        domain += str + ",";
                    }
                    domain = domain.Substring(0, domain.Length - 1) + ".";
                    var range = ":" + (renameList.ContainsKey(_header) ? renameList[_header] : _header) + " rdf:range ";
                    foreach (string str in _ranges)
                    {
                        range += str + ",";
                    }
                    range = range.Substring(0, range.Length - 1) + ".";

                    return head + Environment.NewLine + domain + Environment.NewLine + range;
                }
                else
                {
                    return "";
                }

            }
        }
    }
}
