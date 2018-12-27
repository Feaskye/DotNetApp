using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SkyTeleFault.App_Start
{
    public class DBAccessHelper
    {

        /// <summary>
        /// 数据库连接串
        /// </summary>
        private string ConnectionString
        {
            get
            {
                String strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 "Data Source=" + HttpContext.Current.Server.MapPath("/App_Data/SkyTeleFault.accdb") +
                                 ";Jet OLEDB:Database Password=lyzh@2015";

                return strConn;
            }
        }


        public DataTable GetDataTable(int? id = null)
        {
            DataTable dt = new DataTable();

            //创建连接对象
            OleDbConnection dbConn = new OleDbConnection(ConnectionString);

            try
            {
                //打开数据库
                dbConn.Open();

                string strSqlText = @"SELECT * from EleCable ";
                if (id.HasValue)
                {
                    strSqlText += " where id=" + id;
                }

                OleDbDataAdapter dbAdap = new OleDbDataAdapter(strSqlText, dbConn);
                dbAdap.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
                //NetLog.WriteSysLog("[获取本地机器信息GetMachineInfo]" + ex.Message);
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
            }

            return dt;
        }

        /// <summary>
        /// 返回是否成功
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        internal bool InsertData(NameValueCollection collection)
        {
            var strSqlText = "Insert into EleCable (InputMan,LineName,UpDownOpen,LineNumber,LineCompany,CompanyTel,OutputTime,CommissionTime,IsAnatomy,IsSendTele,AddTime," +
                " ConstructCompany, ConstructMan, ConstructPhone, FaultDate, FaultType, FaultReason, 导体, 半导体, 绝缘层, " +
                "屏蔽层, 铠装, XinNumber, RepairCompany, RepairMan, RepairPhone, FinishTime, UseTime, Description)" +
                " values ('" + collection["InputMan"] + "','" + collection["LineName"] + "','" + collection["UpDownOpen"] + "','" + collection["LineNumber"] 
                + "','" + collection["LineCompany"] + "','" + collection["CompanyTel"] + "','" + collection["OutputTime"] +
                "','" + collection["CommissionTime"] + "','" + collection["IsAnatomy"] + "','" + collection["IsSendTele"] + "','" + collection["AddTime"] + "" +
                "','" + collection["ConstructCompany"] + "','" + collection["ConstructMan"] + "','" + collection["ConstructPhone"] + "','" + collection["FaultDate"] + 
                "','" + collection["FaultType"] + "','" + collection["FaultReason"] + "','" + collection["导体"] + "','" + collection["半导体"] + 
                "','" + collection["绝缘层"] + "','" + collection["屏蔽层"] + "','" + collection["铠装"] + "','" + collection["XinNumber"] +
                "','" + collection["RepairCompany"] + "','" + collection["RepairMan"] + "','" + collection["RepairPhone"] + 
                "','" + collection["FinishTime"] + "','" + collection["UseTime"] + "','" + collection["Description"] + "'" + 
                ")";

            //创建连接对象
            OleDbConnection dbConn = new OleDbConnection(ConnectionString);
            try
            {
                //打开数据库
                dbConn.Open();

                OleDbCommand cmd = new OleDbCommand(strSqlText, dbConn);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
                //NetLog.WriteSysLog("[获取本地机器信息GetMachineInfo]" + ex.Message);
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
            }

            return false;
        }
    }
}