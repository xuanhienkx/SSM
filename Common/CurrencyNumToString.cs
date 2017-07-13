using System;
using System.Linq;
using System.Linq.Expressions;
using SSM.Common;


namespace SSm.Common
{
    public enum CodeCurrency
    {
        VND,
        USD,
    }
    public static class CurrencyNumToString
    {
        public static string DecimalToString(this decimal number, CodeCurrency code)
        {
            number = decimal.Round(number, 2, MidpointRounding.AwayFromZero);
            string wordNumber = string.Empty;
            string[] arrNumber = number.ToString().Split('.');
            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToString(wholePart);

            switch (code)
            {
                case CodeCurrency.USD:
                    wordNumber = (wholePart == 0 ? "Không " : strWholePart) + (wholePart == 1 ? "us dollar và " : "us dollars và ");
                    if (arrNumber.Length > 1)
                    {
                        long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
                        string strFarctionPart = NumberToString(fractionPart);
                        //var le = arrNumber[1].Length == 2 && arrNumber[1].StartsWith("0") ? "phẩy không " + strFarctionPart : " phẩy " + strFarctionPart; 
                        wordNumber += strFarctionPart + (fractionPart > 1 ? "cents" : "cent ");
                    }
                    //wordNumber += "đo la./.";
                    break;
                case CodeCurrency.VND:
                default:
                    wordNumber = (wholePart == 0 ? "Không " : strWholePart);
                    wordNumber += "đồng chẵn./.";
                    break;
            }

            return wordNumber.Trim().FirstCharToUpper();
        }
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            input = input.Trim();
            var str = input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            return str.Replace("  ", " ");
        }
        private static string NumberToString(decimal number)
        {
            string s = number.ToString("#");
            string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
            int i, j, donvi, chuc, tram;
            string str = " ";
            bool booAm = false;
            decimal decS = 0;

            try
            {
                decS = Convert.ToDecimal(s.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.Message);
            }
            if (decS < 0)
            {
                decS = -decS;
                s = decS.ToString();
                booAm = true;
            }
            i = s.Length;
            if (i == 0)
                str = so[0] + str;
            else
            {
                j = 0;
                while (i > 0)
                {
                    donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                    i--;
                    if (i > 0)
                        chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        chuc = -1;
                    i--;
                    if (i > 0)
                        tram = Convert.ToInt32(s.Substring(i - 1, 1));
                    else
                        tram = -1;
                    i--;
                    if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                        str = hang[j] + str;
                    j++;
                    if (j > 3) j = 1;
                    if ((donvi == 1) && (chuc > 1))
                        str = "một " + str;
                    else
                    {
                        if ((donvi == 5) && (chuc > 0))
                            str = "lăm " + str;
                        else if (donvi > 0)
                            str = so[donvi] + " " + str;
                    }
                    if (chuc < 0)
                        break;
                    else
                    {
                        if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                        if (chuc == 1) str = "mười " + str;
                        if (chuc > 1) str = so[chuc] + " mươi " + str;
                    }
                    if (tram < 0) break;
                    else
                    {
                        if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                    }
                    str = " " + str;
                }
            }
            if (booAm) str = "Âm " + str;
            return str;
        }
    }
}