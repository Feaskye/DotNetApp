using SkyTeleFault.Models;
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
        internal Tuple<bool,string> InsertData(NameValueCollection collection)
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
                var b = cmd.ExecuteNonQuery() > 0;
                return new Tuple<bool, string>(b,"");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.Message.Contains("数据"))
                {
                    msg = "日期格式有误或下拉框未选择！";
                }
                return new Tuple<bool, string>(false, msg);
                //throw ex;
                //NetLog.WriteSysLog("[获取本地机器信息GetMachineInfo]" + ex.Message);
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
            }
        }

        /// <summary>
        /// 返回是否成功
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        internal Tuple<bool, string> UpdateData(NameValueCollection collection)
        {
            var strSqlText = "update EleCable set InputMan='" + collection["InputMan"] + "',LineName='" + collection["LineName"] + "',UpDownOpen='" + collection["UpDownOpen"] + "',LineNumber='" + collection["LineNumber"]
                + "',LineCompany='" + collection["LineCompany"] + "',CompanyTel='" + collection["CompanyTel"] + "',OutputTime='" + collection["OutputTime"] +
                "',CommissionTime='" + collection["CommissionTime"] + "',IsAnatomy='" + collection["IsAnatomy"] + "',IsSendTele='" + collection["IsSendTele"] + "',AddTime='" + collection["AddTime"] + "" +
                "',ConstructCompany='" + collection["ConstructCompany"] + "',ConstructMan='" + collection["ConstructMan"] + "',ConstructPhone='" + collection["ConstructPhone"] + "',FaultDate='" + collection["FaultDate"] +
                "',FaultType='" + collection["FaultType"] + "',FaultReason='" + collection["FaultReason"] + "',导体='" + collection["导体"] + "',半导体='" + collection["半导体"] +
                "',绝缘层='" + collection["绝缘层"] + "',屏蔽层='" + collection["屏蔽层"] + "',铠装='" + collection["铠装"] + "',XinNumber='" + collection["XinNumber"] +
                "',RepairCompany='" + collection["RepairCompany"] + "',RepairMan='" + collection["RepairMan"] + "',RepairPhone='" + collection["RepairPhone"] +
                "',FinishTime='" + collection["FinishTime"] + "',UseTime='" + collection["UseTime"] + "',Description='" + collection["Description"] + "'" +
                " where id="+ collection["id"] + " ";

            //创建连接对象
            OleDbConnection dbConn = new OleDbConnection(ConnectionString);
            try
            {
                //打开数据库
                dbConn.Open();

                OleDbCommand cmd = new OleDbCommand(strSqlText, dbConn);
                var b = cmd.ExecuteNonQuery() > 0;
                return new Tuple<bool, string>(b, "");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.Message.Contains("数据"))
                {
                    msg = "日期格式有误或下拉框未选择！";
                }
                return new Tuple<bool, string>(false, msg);
                //throw ex;
                //NetLog.WriteSysLog("[获取本地机器信息GetMachineInfo]" + ex.Message);
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
            }
        }

        internal Tuple<bool, string> Delete(int id)
        {
            var strSqlText = " delete from EleCable  where id=" + id + " ";

            //创建连接对象
            OleDbConnection dbConn = new OleDbConnection(ConnectionString);
            try
            {
                //打开数据库
                dbConn.Open();

                OleDbCommand cmd = new OleDbCommand(strSqlText, dbConn);
                var b = cmd.ExecuteNonQuery() > 0;
                return new Tuple<bool, string>(b, "");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
                //throw ex;
                //NetLog.WriteSysLog("[获取本地机器信息GetMachineInfo]" + ex.Message);
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
            }
        }


    }
}