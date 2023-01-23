


namespace YouTubeNotificator.Domain.Helpers
{
    public static class StringUtils
    {
        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string[] SplitString(this string str, int maxLen)
        {
            var rest = str;
            var res = new List<string>();

            while (rest.Length > maxLen)
            {
                var rStr = rest.Substring(0, maxLen).Reverse();
                var rInd = rStr.IndexOf(Environment.NewLine.Reverse(), StringComparison.InvariantCultureIgnoreCase);
                var ind = rInd > 0 ? maxLen - rInd : maxLen;
                res.Add(rest.Substring(0, ind));
                rest = rest.Substring(ind + 1);
            }

            if (rest.Length > 0)
            {
                res.Add(rest);
            }

            return res.ToArray();
        }

    }
}
