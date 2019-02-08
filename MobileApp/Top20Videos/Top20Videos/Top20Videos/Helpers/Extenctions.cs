namespace Top20Videos.Helpers
{
    public static class Extenctions
    {
 
        /// <summary>
        /// To get limited text
        /// </summary>
        /// <param name="value">text</param>
        /// <returns>string</returns>
        public static string GetLimitedText(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) { return ""; }

            if (value.Length > length)
            {
                string tmp = value.Substring(0, length);
                return tmp.Contains(" ") ? tmp.Substring(0, tmp.LastIndexOf(" ")) : tmp;
            }
            else
            {
                return value;
            }

        }
            }
    
}
