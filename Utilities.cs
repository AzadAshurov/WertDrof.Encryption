

using System.Text;

namespace WertDrof.Encryption
{
    public static class Utilities
    {
        static readonly char[] allCharacters = (
         "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
         "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя" +
         "0123456789!@#$%^&*()-_=+[]{};:'\",<.>/?\\|`~ ") 
         .ToCharArray();

        static readonly Dictionary<char, int> charToIndex = allCharacters
        .Select((c, i) => new { c, i })
        .ToDictionary(x => x.c, x => x.i);

    static readonly Dictionary<int, char> indexToChar = allCharacters
        .Select((c, i) => new { c, i })
        .ToDictionary(x => x.i, x => x.c);

    static int questionMarkIndex = charToIndex['?'];

    public static List<int> Encode(string input)
    {
        return input.Select(c => charToIndex.TryGetValue(c, out int index) ? index : questionMarkIndex).ToList();
    }

    public static string Decode(List<int> encoded)
    {
        return new string(encoded.Select(i => indexToChar.ContainsKey(i) ? indexToChar[i] : '?').ToArray());
    }

        //65-90
        public static string AddSymbolsToEdges(string text )
        {
            char firstSymbol = (char)((text.Length % 26) + 65);
            char secondSymbol = (char)(((7 * text.Length % 26)) + 65);
            text = firstSymbol + text +  secondSymbol;
            return text;
        }
        }
}
