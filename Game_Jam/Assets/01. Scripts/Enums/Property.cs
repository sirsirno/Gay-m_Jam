using System;

[Flags]
public enum Property
{
    NONE = 0,
    FIRE = 1 << 0,
    WATER = 1 << 1,
    FIRE_WATER = FIRE | WATER
}
