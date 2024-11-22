[Flags]
public enum Roles
{
    None = 0,
    MAGE = 1,
    ASSASSIN = 1 << 1,
    TIREUR =  1 << 2,
    TANK =  1 << 3,
    COMBATTANT = 1 << 4,
    SUPPORT = 1 << 5,
}