using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class DataSource
{
}

/*The DataSource class:
● The elements in the class should be static because the class itself is static.
● Add a readonly static field for generating random numbers. This field should be initialized
in the class’s declaration.
● The class should consist of data entity arrays, declared internal.
○ Arrays should be initialized with fixed sizes according to the project’s general
description.
○ For every entity add a private method that pushes objects into the appropriate data
entity array.
○ Add a private method s_Initialize that will be called from the class’s default
constructor (static).
○ The s_Initialize method calls the push methods for entity arrays by the
dependency order (initialize a dependent data entity only after its prerequisite was
initialized).
○ Be reasonable with initialized values. The values should be coordinated across per the
requirements listed in the mini-project’s general description.
○ Dates:
■ All missing values of type DateTime should be initialized with
DateTime.MinValue
■ Ordered date values - use TimeSpan and add an interval chosen at random.
Again, be reasonable with the interval range.
○ Object identifiers
■ Implement auto-incrementing identifiers for new objects using a property in the
internal class Config (see below)
■ (non-object) identifier values should be chosen at random as described in the
project’s general description.
○ Add a nested class Config, declared internal.
■ Add internal static fields to index the first available element in each entity
array. These should be initialized with zero values.
■ Add private static fields for the last available running (integer) identifier in
each object where an auto-incremental identifier field exists. These should be
initialized with the next available identifier in each entity array.
■ For every such index field, add only a get()interface that will increment the
field’s value by 1 with each call.

 The role of each such class is to access data only; insert, update, delete, or retrieve information from
an entity. In such case the methods must not
● apply any logic apart from checking the identifiers (described below)
9
● perform I/O operations*/
