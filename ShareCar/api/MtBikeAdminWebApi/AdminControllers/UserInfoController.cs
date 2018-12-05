using Autofac;
using MintCyclingService.Cycling;
using MintCyclingService.Transaction;
using MintCyclingService.User;
using MintCyclingService.Utils;
using MtBikeAdminWebApi;
using MtBikeAdminWebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MintCyclingWebApi.AdminControllers
{
    /// <summary>
    /// 用户管理控制器
    /// </summary>
    [CheckAdminAccessCodeFilter]
    public class UserInfoController : ApiController
    {
        IUserInfoService _userService;
        ITransactionInfoService _transacationService;

        /// <summary>
        /// 初始化单车控制器
        /// </summary>
        public UserInfoController()
        {
            _userService = AutoFacConfig.Container.Resolve<IUserInfoService>();
            _transacationService = AutoFacConfig.Container.Resolve<ITransactionInfoService>();
        }


        #region 后台用户管理接口
        /// <summary>
        /// 根据查询条件搜索用户列表  complete TOM
        /// DATE：2017-05-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel GetUserInfoByCondition([FromUri]AdminUserInfo_PM model)
        {
            return _userService.GetUserInfoList(model);
        }
        /// <summary>
        /// 通过用户的Guid查询个人信息
        /// 作者：TOM
        /// 时间：2017-05-23
        /// </summary>
        /// <param name="UserGuid"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel GetUserByUserGuid(Guid UserGuid)
        {
            return _userService.GetUserByUserGuid(UserGuid);

        }


        /// <summary>
        /// 编辑用户信息    complete TOM
        /// DATE：2017-05-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel EditUserInfoByUserGuid([FromBody]EditUserInfo_PM model)
        {
            return _userService.EditUserInfoByUserGuid(model);
        }



        /// <summary>
        /// 删除用户信息    complete TOM
        /// DATE：2017-05-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel DeleteUserByUserGuid([FromBody]DeleteUserInfo_PM model)
        {
            return _userService.DeleteUserByUserGuid(model);
        }



        /// <summary>
        /// 锁定用户状态 complete TOM
        /// DATE：2017-05-23
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel LockUserStatusByUserGuid(LockUserInfo_PM model)
        {
            return _userService.LockUserStatusByUserGuid(model);
        }


        #endregion

        #region  用户交易记录管理

        /// <summary>
        /// 当前交易记录API列表 complete TOM
        ///  DATE：2017-05-24
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel GetTransactionList([FromBody]UserTransaction_PM model)
        {

            return _transacationService.GetTransactionList(model);

        }

        /// <summary>
        /// 手动还车详细  complete TOM
        /// DATE：2017-05-24
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel GetUserByUserGuid([FromBody]RetrunBicycle_PM model)
        {
            return _transacationService.GetUserByUserGuid(model);
        }

 
        /// <summary>
        /// 手动还车处理 complete TOM
        /// DATE：2017-05-23
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel ReturnCarByPhone([FromBody] ReturnCar_PM model)
        {
            return _userService.ReturnCarByPhone(model);
        }

        /// <summary>
        /// 历史交易记录API列表  complete TOM
        /// DATE：2017-05-24
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel GetHistoryUserTransactionList([FromBody]HistoryUserTransaction_PM model)
        {

            return _transacationService.GetHistoryUserTransactionList(model);

        }


        /// <summary>
        /// 用户充值押金交易记录API列表  complete TOM
        /// DATE：2017-05-25
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel GetUserDepositRechargeRecordList([FromUri]UserDepositRecharge_PM model)
        {

            return _transacationService.GetUserDepositRechargeRecordList(model);

        }

        /// <summary>
        /// 用户充值余额交易记录API列表  complete TOM
        /// DATE：2017-05-25
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel GetUserAccountRechargeRecordList([FromUri]UserDepositRecharge_PM model)
        {
            return _transacationService.GetUserAccountRechargeRecordList(model);
        }


        /// <summary>
        /// 用户退款交易记录API列表  complete TOM
        /// DATE：2017-05-25
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel GetUserRefundDepositRecordList([FromUri]UserDepositRecharge_PM model)
        {
            return _transacationService.GetUserRefundDepositRecordList(model);
        }


        #endregion

    }
}

