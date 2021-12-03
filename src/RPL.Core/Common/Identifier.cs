namespace RPL.Core.Common
{
    public static class Identifier
    {
        public static string Serialize(string name)
        {
            return char.ToLowerInvariant(name[0]) + name[1..];
        }
    }
}
