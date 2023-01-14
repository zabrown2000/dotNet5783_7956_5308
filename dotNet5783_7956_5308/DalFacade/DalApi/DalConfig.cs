using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
using DO;
using System.Xml.Linq; //includes tools to handle xml and DOM xml


static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;
    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml") //loads an xml file, parses it and builds a DOM (Document Object Model) tree of type XElement and returns the root element.
            //object of type XElement contains the analyzed (parsed) information of the underlying xml element in the xml file.
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value //retrieves an xml element, originally tagged <dal>, from the DOM tree, while Value retrieves the value of the element
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements() //retrieves the subtrees of the element rooted at <dal-packages> in the DOM.
            ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value); //creates a hash table (a dictionary) where the hash key is the element’s tag and the value is the element’s value
    }
}
