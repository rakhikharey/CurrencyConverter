using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cconvert.Model;


namespace Cconvert
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CroweCurrencyConversionAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CroweCurrencyConversionAPI.svc or CroweCurrencyConversionAPI.svc.cs at the Solution Explorer and start debugging.
    public class CroweCurrencyConversionAPI : ICroweCurrencyConversionAPI
    {
        public string CurrrencyConvert(string fromCur, string toCur, double amount,  string apikey)
        {
            string resultJson = "";
            Common comm = new Common();
            using (var conversion = new CConversion())
            {
                if (conversion.ValidateValues(fromCur, toCur, amount, apikey))
                {
                    double rate = 0.0;
                    using (var db = new Cconvert.Model.cconverterEntities())
                    {
                        var crates = db.CurRates.FirstOrDefault(u => u.fromC == fromCur && u.toC == toCur);
                        if (crates != null)
                        {
                            long source = 0;
                            var sources =  db.sourcemasters.FirstOrDefault(u => u.accesskey == apikey && u.active == true);
                            if(sources!=null)
                            {
                                source = sources.id;
                            }
                            else
                            {
                                var logins = db.logindetails.FirstOrDefault(u => u.token == apikey && u.isactive == true);
                                if(logins!=null)
                                {
                                    source = logins.sourceid;
                                }
                            }
                            if (source > 0)
                            {

                                ObjectParameter stat = new ObjectParameter("status", typeof(string));
                                try
                                {
                                    var result = db.GetCurrencyRate(fromCur, toCur, Convert.ToInt64(source), apikey, stat);
                                    foreach (var s in result)
                                    {
                                        rate = s.rate;
                                    }
                                    var conversionJson = conversion.GetConvertedAmount(fromCur, toCur, rate, amount);
                                    resultJson = conversionJson;
                                }
                                catch
                                {
                                    resultJson = comm.GetErrorMessage("Some error occur");

                                }
                            }
                            else
                                resultJson = comm.GetErrorMessage("Source does not exists");
                        }
                        else
                            resultJson = comm.GetErrorMessage("Conversion rate not available");
                    }
                   
                }
                else
                    resultJson = comm.GetErrorMessage("Some parameters are missing or incorrect");
            }
            return resultJson;

        }



        public string LoginSource(string user, string passcode)
        {
            string resultJson = "";
            var crypto = new SimpleCrypto.PBKDF2();
            Common comm = new Common();
            LoginUser_Result logindetail = new LoginUser_Result();
            using (var db = new Cconvert.Model.cconverterEntities())
            {
                var source = db.sourcemasters.FirstOrDefault(u => u.userid == user && u.active == true);
                if (source != null)
                {
                    if (source.loginrequired)
                    {
                        if (source.passcode == crypto.Compute(passcode, source.passwordsalt))
                        {
                            try
                            {
                                ObjectParameter token = new ObjectParameter("token", typeof(string));
                                var loginUser = db.LoginUser(user, source.passcode, token);
                                foreach (var s in loginUser)
                                {
                                    logindetail = s;
                                    break;

                                }
                                using (var conversion = new CConversion())
                                {
                                    resultJson = conversion.RetreiveToken(token.Value.ToString(),logindetail.userid);
                                }
                            }
                            catch(Exception ex)
                            {
                                resultJson = comm.GetErrorMessage("Some error occur");
                            }
                        }
                    }
                    else
                        resultJson = comm.GetErrorMessage("login not required ! please use your access key to fetch");
                }
                else
                {
                    resultJson = comm.GetErrorMessage("User does not exists");
                }
            }
            return resultJson;
        }
    }
}
