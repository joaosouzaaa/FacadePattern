using System.ComponentModel;

namespace FacadePattern.API.Enums;

public enum EMessage : ushort
{
    [Description("{0} was not found.")]
    NotFound = 0,

    [Description("{0} is invalid.")]
    Invalid,

    [Description("{0} already exists.")]
    Exists
}
