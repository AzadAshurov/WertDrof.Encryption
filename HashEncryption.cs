namespace WertDrof.Encryption
{
    public static class HashEncryption
    {
        public static string  Hasher(string inputText, string key)
        {
           key = key.Substring(0, Math.Min(127, key.Length));
            List<int> numbers= Utilities.Encode(key);
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == 0) numbers[i] = 1;             
            }
            //int count = Math.Min(6, numbers.Count);
            //long mult = numbers.Take(count).Aggregate(1, (acc, n) => acc * n);
            long newKey = numbers.Sum() + numbers.Take(Math.Min(6, numbers.Count)).Aggregate(1, (acc, n) => acc * n);
            string encryptedText = Hasher(inputText, newKey);
            return encryptedText;
        }
        public static string Hasher(string inputText, long key)
        {            
            ulong realKey = ulong.MaxValue - (ulong)key;
            string encryptedText = GenerateHash(inputText, realKey);
            return encryptedText;
        }
        private static string GenerateHash(string inputText, ulong realKey)
        {
            return inputText;
        }
    }
}
