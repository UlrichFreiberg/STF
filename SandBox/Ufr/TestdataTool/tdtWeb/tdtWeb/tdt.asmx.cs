using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace tdtWeb
{
    /// <summary>
    /// Summary description for tdt
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class tdt : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetAllVacantCprNumbers(string testEnvironment)
        {
            var dc = new TestdataToolEntities();

            var CprRow = from result in dc.CprNumbers
                         where result.Environment == testEnvironment && string.IsNullOrEmpty(result.UsedBy)
                         select result;

            var jss = new JavaScriptSerializer();
            return jss.Serialize(CprRow);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string GetCprNumber(string testEnvironment, string reservationId)
        {
            var dc = new TestdataToolEntities();
            tdtWeb.CprNumber cprRow;

            var ReservationIdRequested = !string.IsNullOrEmpty(reservationId);
            var cprRows = dc.CprNumbers.Where(cprNo => cprNo.Environment == testEnvironment);

            cprRow = cprRows.FirstOrDefault();

            if (ReservationIdRequested)
            {
                // find a row matching the reservation
                var matchingCprRows = cprRows.Where(cprNo => cprNo.UsedBy == reservationId);

                // if not found get a unused and reserve that one
                if (matchingCprRows.Count() == 0)
                {
                    cprRow = cprRows.FirstOrDefault();
                    cprRow.UsedBy = reservationId;
                    dc.SaveChanges();
                }
            }
            else
            {
                // find one that is not reserved
                var matchingCprRows = cprRows.Where(cprNo => cprNo.UsedBy == null);
                cprRow = matchingCprRows.FirstOrDefault();
            }

            // Convert to JSON
            var jss = new JavaScriptSerializer();
            return jss.Serialize(cprRow);
        }
    }
}
