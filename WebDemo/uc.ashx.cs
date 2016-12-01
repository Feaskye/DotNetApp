using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DS.Web.UCenter;
using DS.Web.UCenter.Api;

namespace WebDemo
{
    /// <summary>
    /// uc 的摘要说明
    /// </summary>
    public class uc : DS.Web.UCenter.Api.UcApiBase
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public override ApiReturn DeleteUser(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn RenameUser(int uid, string oldUserName, string newUserName)
        {
            throw new NotImplementedException();
        }

        public override UcTagReturns GetTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn SynLogin(int uid)
        {
            HttpContext.Current.Session["uid"] = uid;
            return ApiReturn.Success;
        }

        public override ApiReturn SynLogout()
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdatePw(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateBadWords(UcBadWords badWords)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateHosts(UcHosts hosts)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateApps(UcApps apps)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateClient(UcClientSetting client)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateCredit(int uid, int credit, int amount)
        {
            throw new NotImplementedException();
        }

        public override UcCreditSettingReturns GetCreditSettings()
        {
            throw new NotImplementedException();
        }

        public override ApiReturn GetCredit(int uid, int credit)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateCreditSettings(UcCreditSettings creditSettings)
        {
            throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}