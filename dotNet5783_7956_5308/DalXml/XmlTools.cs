using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

static internal class XmlTools
{
    static string suffixPath = @"xml\";

    #region XElement
    #region load to file
    public static XElement LoadListFromXMLElement(string filePath)
    {
        try
        {
            if (File.Exists(suffixPath + filePath))
            {
                return XElement.Load(suffixPath + filePath);
            }
            else
            {
                XElement rootElem = new XElement(filePath);
                if (filePath == @"config.xml") { }
                //rootElem.Add(new XElement("droneRunningNum", 1)); //להוריד
                rootElem.Save(suffixPath + filePath);
                return rootElem;
            }
        }
        catch (Exception ex)
        {
            throw new DO.LoadingException(filePath, $"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion
    #region save to file
    public static void SaveListToXMLElement(XElement rootElem, string filePath)
    {
        try
        {
            rootElem.Save(suffixPath + filePath);
        }
        catch (Exception ex)
        {
            throw new DO.LoadingException(suffixPath + filePath, $"fail to create xml file: {suffixPath + filePath}", ex);
        }
    }

    #endregion
    #endregion

    #region XmlSerializer
    //save a complete list in a specific file- throw exception in case of problems..
    //for the using with XMLSerializer..
    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(suffixPath + filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            throw new DO.LoadingException(suffixPath + filePath, $"fail to create xml file: {suffixPath + filePath}", ex);
        }
    }

    //load a complete list from a specific file- throw exception in case of problems..
    //for the using with XMLSerializer..
    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(suffixPath + filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(suffixPath + filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file)!;
                file.Close();
                return list!;
            }
            else
                return new List<T>();
        }
        catch (Exception ex)
        {
            throw new DO.LoadingException(suffixPath + filePath, $"fail to load xml file: {suffixPath + filePath}", ex);
        }
    }
    #endregion
}
