namespace DO;

/// <summary>
/// struct to hold all the enums we'll need for this namespace
/// </summary>
public struct Enums
{
    public enum Categories { Mixer, Blender, Oven, Fridge, Freezer, Stove, Kettle };

    public enum ActionType { Add=1, Delete, Update, ReadId, ReadAll };

    public enum EntityType { Exit, Products, Orders, OrderItems };
}



