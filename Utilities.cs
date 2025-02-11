

using System.Text;

namespace WertDrof.Encryption
{
    internal static class Utilities
    {
        static readonly char[] allCharacters = (
         "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" + "0123456789" +
         "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя" +
         "!@#$%^&*()-_=+[]{};:'\",<.>/?\\|`~ ") 
         .ToCharArray();

        static readonly Dictionary<char, int> charToIndex = allCharacters
        .Select((c, i) => new { c, i })
        .ToDictionary(x => x.c, x => x.i);

    static readonly Dictionary<int, char> indexToChar = allCharacters
        .Select((c, i) => new { c, i })
        .ToDictionary(x => x.i, x => x.c);

    static int questionMarkIndex = charToIndex['?'];

        internal static List<int> Encode(string input)
    {
        return input.Select(c => charToIndex.TryGetValue(c, out int index) ? index : questionMarkIndex).ToList();
    }

        internal static string Decode(List<int> encoded)
    {
        return new string(encoded.Select(i => indexToChar.ContainsKey(i) ? indexToChar[i] : '?').ToArray());
    }

        //65-90
        internal static string AddSymbolsToEdges(string text )
        {
            char firstSymbol = (char)((text.Length % 26) + 65);
            char secondSymbol = (char)(((7 * text.Length % 26)) + 65);
            text = firstSymbol + text +  secondSymbol;
            return text;
        }
        internal static ulong StringKeyToRealKey(string key)
        {
            key = key.Substring(0, Math.Min(127, key.Length));
            List<int> numbers = Encode(key);
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == 0) numbers[i] = 1;
            }
            long newKey = numbers.Sum() + numbers.Take(Math.Min(6, numbers.Count)).Aggregate(1, (acc, n) => acc * n);
         
            return LongKeyToRealKey(newKey);
        }
        internal static ulong LongKeyToRealKey(long key)
        {
            ulong realKey = ulong.MaxValue - (ulong)key;
            return realKey;
        }

    }
}
