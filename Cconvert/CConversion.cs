using Cconvert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace Cconvert
{
    public class CConversion : IDisposable
    {
        public string GetConvertedAmount(string fromCur, string toCur, double rate, double amounttoconvert)
        {
            var res = new Common();

            return res.GetResponseJson(new ConversionAmount
            {
                amount = amounttoconvert,
                fromCur = fromCur,
                toCur = toCur,
                rate = rate
            });

        }

        public string RetreiveToken(string token, string userid)
        {
            var res = new Common();

            return res.GetResponseJson(new
            {
                token = token,
                user = userid,

            });

        }
        public bool ValidateValues(string fromCur, string toCur, double amount, string apikey)
        {
            Regex pattern = new Regex(@"[a-zA-Z]");
            if (fromCur.Trim() == "")
                return false;

            if (!pattern.IsMatch(fromCur))
            {
                return false;
            }
            if (fromCur.Trim().Count() > 3 || toCur.Trim().Count() < 3)
                return false;

            if (toCur.Trim() == "")
                return false;
            if (!pattern.IsMatch(toCur))
            {
                return false;
            }
            if (toCur.Trim().Count() < 3 || toCur.Trim().Count() > 3)
            {
                return false;
            }
            if (amount == 0)
                return false;
            if (apikey.Trim() == "")
                return false;
            return true;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
