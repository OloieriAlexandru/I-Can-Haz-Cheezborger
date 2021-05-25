using System;

namespace BusinessLogic.Utils
{
    public static class Base64Extractor
    {
        public static bool Extract(string image, ref string imageType, ref byte[] imageBytes)
        {
            string[] mainSplit = image.Split(';', 2);
            if (mainSplit.Length != 2)
            {
                return false;
            }
            string[] leftSplit = mainSplit[0].Split(':');
            if (leftSplit.Length != 2)
            {
                return false;
            }
            imageType = leftSplit[0];
            string[] rightSplit = mainSplit[1].Split(',', 2);
            if (rightSplit.Length != 2)
            {
                return false;
            }
            imageBytes = Convert.FromBase64String(rightSplit[1]);
            return true;
        }
    }
}
