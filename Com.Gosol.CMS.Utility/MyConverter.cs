using System;
using System.Text;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
namespace Com.Gosol.CMS.Utility
{
    /// <summary>
    /// Summary description for MyConverter
    /// </summary>
    public class MyConverter
    {

        private static string StripDiacritics(string accented)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");

            string strFormD = accented.Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD , String.Empty).Replace('\u0111' , 'd').Replace('\u0110' , 'D');
        }


        public static string ConverToUTF( string inputtext )
        {
            return HttpUtility.HtmlDecode(inputtext);
        }
        public static string Convert( string codau , int articleID )
        {
            if( !SQLHelper.IsNullOrEmpty(codau) && codau!="noname" )
            {
                codau = HttpUtility.HtmlDecode(codau);
                string khongdau = StripDiacritics(codau);
                khongdau = Regex.Replace(khongdau , @"[^a-zA-Z_0-9\s]" , String.Empty);
                khongdau = Regex.Replace(khongdau.Trim() , @"\s{2,}" , " ");

                khongdau = khongdau.Replace(" " , "-");
                khongdau = Regex.Replace(khongdau , @"-{2,}" , @"-");
                
                return khongdau;
            }
            else
            {
                return articleID.ToString();
            }
        }
    }

}
