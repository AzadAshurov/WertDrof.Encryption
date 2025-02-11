namespace WertDrof.Encryption
{
    public static class HashEncryption
    {
        public static string  Hasher(string inputText, string key , bool cyrillicLetters = false)
        {
           ulong realKey = Utilities.StringKeyToRealKey(key);
            return  GenerateHash(inputText, realKey, cyrillicLetters);    
        }
        public static string Hasher(string inputText, long key, bool cyrillicLetters = false)
        {
            ulong realKey = Utilities.LongKeyToRealKey(key);
            return GenerateHash(inputText, realKey, cyrillicLetters);        
        }
        private static string GenerateHash(string inputText, ulong realKey, bool cyrillicLetters)
        {
            int availableSymbols = cyrillicLetters ? 128 : 62;
            List<int> digits = realKey.ToString().Select(c => c - '0').ToList();
            digits.Reverse();
            int getNumber =0;
            int j = 0;
            List<int> piramid = Utilities.Encode(inputText);
            List<int> result = new List<int>();
            int n = (255 / piramid.Count) + 1;
            for (int i = 0; i < n; i++)
            {
                List<int> piramidCopy = new List<int>(piramid);
                while (true)
                {
                    getNumber = getNumber + digits[j % digits.Count];
                
                    j++;
                    
                    result.Add(Math.Abs(((piramidCopy[getNumber% piramidCopy.Count])+getNumber)% availableSymbols));
                    if(piramidCopy.Count == 1) { break; }
                    for(int l = 0; l<piramidCopy.Count-1; l++)
                    {
                        piramidCopy[l] = piramidCopy[l] + piramidCopy[l + 1];
                    }
                    piramidCopy.RemoveAt(piramidCopy.Count - 1);
                }
            }
  
            string outputText = Utilities.Decode(result);
            return outputText;
        }
    }
}
 