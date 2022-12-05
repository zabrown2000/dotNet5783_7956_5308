namespace DO;

/// <summary>
/// struct to hold all the enums we'll need for this namespace
/// </summary>
public struct Enums
{
    /// <summary>
    /// Enum for the categories
    /// </summary>
    public enum Categories { Mixer=1, Blender, Oven, Fridge, Freezer, Stove, Kettle };

    /// <summary>
    /// Enum for action to take on an entity
    /// </summary>
    public enum ActionType { Add=1, Delete, Update, ReadId, ReadAll };

    /// <summary>
    /// Enum for choosing which entity to work with
    /// </summary>
    public enum EntityType { Exit, Products, Orders, OrderItems };
}



