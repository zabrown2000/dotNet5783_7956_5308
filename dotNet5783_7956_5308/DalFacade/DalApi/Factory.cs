using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

using DO;
using System.Reflection;
using static DalApi.DalConfig; // static namespace import allows using the static class methods as if they were in the global scope


public static class Factory
{
    public static IDal? Get()
    {
        string dalType = s_dalName
        ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
        string dal = s_dalPackages[dalType] //subscript operator [] in a hash table retrieves the value of the hash element s_dalPackages[dalType]
        ?? throw new DalConfigException($"Package for {dalType} is not found in packages list");
        try
        {
            Assembly.Load(dal ?? throw new DalConfigException($"Package {dal} is null")); //Assembly.Load will load an assembly (a .dll file or a module) that contains a package library (a collection of classes).
        }
        catch (Exception)
        {
            throw new DalConfigException("Failed to load {dal}.dll package");
        }
        Type? type = Type.GetType($"Dal.{dal}, {dal}") //uses a reflection (metadata) of the object Dal.DalList (from the module DalList.dll).
            ?? throw new DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");
        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)? //type.GetProperty returns the metadata of a property named Instance that must be static with public access permissions.
                    .GetValue(null) as IDal //Calling GetValue(null) on that Instance property from above returns the value of that static property (the only value that should be generated in our Singleton class DalList)
            ?? throw new DalConfigException($"Class {dal} is not singleton or Instance property not found");
    }
}
