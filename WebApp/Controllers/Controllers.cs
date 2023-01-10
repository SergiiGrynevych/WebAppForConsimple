using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("")]
    [ApiController]
    public class Controllers : ControllerBase
    {

        /// <summary>
        /// Get Birthday List of Clients from database
        /// </summary>
        /// <returns>Returns a list of clients (id, full name) whose day is today birth</returns>
        /// <remarks>
        /// Sample XML request:
        /// 
        ///     <FindByDate>    
        ///         <date>"24/01/2022"</date>
        ///     </FindByDate>
        /// 
        ///   Description:
        /// 
        ///       date - Дата народження іменинника для пошуку (формат 'DD/MM/YYYY')
        ///     
        /// </remarks>
        [HttpPost("GetBirthdayList")]
        public async Task<object> GetBirthdayList([Required, FromBody] FindByDate request)
        {
            var getEmployees = await SQL_GetDataListByRequest<EmployeeBirthday>(request.date, ConfigInterfaceRun.BirthdayList);
            if (getEmployees.Item is null)
            {
                return Ok($"No Clients found with this day of birth: {request.date}");
            }
            else if (getEmployees.IsSuccess)
            {
                return Ok(getEmployees.Item);
            }
            else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Get Recent Buyers List of Clients from database
        /// </summary>
        /// <returns>
        /// Returns a list of clients (id, full name) who made a purchase for last N days. 
        /// For each client, I also display the date of his last purchase.
        /// </returns>
        /// <remarks>
        /// Sample XML request:
        /// 
        ///     <FindByDate>    
        ///         <date>"2"</date>
        ///     </FindByDate>
        /// 
        ///   Description:
        /// 
        ///       date - Шукає інформацію по принципу: сьогоднішня дата - date
        ///     
        /// </remarks>
        [HttpPost("GetRecentBuyers")]
        public async Task<object> GetRecentBuyers([Required, FromBody] FindByDate request)
        {
            var getBuyers = await SQL_GetDataListByRequest<Buyers>(request.date, ConfigInterfaceRun.BuyersList);
            if (getBuyers.Item is null)
            {
                return Ok($"No Buyers found with this day : {request.date}");
            }
            else if (getBuyers.IsSuccess)
            {
                return Ok(getBuyers.Item);
            }
            else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Get Popular Categories of products of Clients from database
        /// </summary>
        /// <returns>
        /// Returns a list of product categories that have been purchased
        /// found client.For each category returns the number purchased units.
        /// </returns>
        /// <remarks>
        /// Sample XML request:
        /// 
        ///     <FindCatByIdClient>    
        ///         <id>"4"</id>
        ///     </FindCatByIdClient>
        /// 
        ///   Description:
        /// 
        ///       id - id Клієнта.Повертає список категорій продуктів, котрі купляв знайденный кліент.
        ///       Для кожної категориї повертає количество купленных одиниць
        ///     
        /// </remarks>
        [HttpPost("GetPopularCategories")]
        public async Task<object> GetPopularCategories([Required, FromBody] FindCatByIdClient request)
        {
            var getBuyers = await SQL_GetDataListByRequest<Cathegories>(request.id, ConfigInterfaceRun.CathegoriesList);
            if (getBuyers.Item is null)
            {
                return Ok($"No Buyers(Clients) found with this id : {request.id}");
            }
            else if (getBuyers.IsSuccess)
            {
                return Ok(getBuyers.Item);
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<OperationResult> SQL_GetDataListByRequest<T>(string date, string proc) where T : class
        {
            string procedureName = proc;
            string msg = string.Concat(" procedureName: ", procedureName);

            try
            {
                SqlParameter[] parameterSapRC = new SqlParameter[] { new SqlParameter("@request", date) };

                SqlProcedure sqlProcedure = new SqlProcedure();

                var ListItems = sqlProcedure.pr_GetListObject<T>(procedureName, ConfigInterfaceRun.testDB, parameterSapRC); 

                if(ListItems.Result.Count == 0)
                {
                    return new OperationResult(true, null, $"{msg}. Clients was received");
                }

                return new OperationResult(true, ListItems.Result, $"{msg}. Clients was received");
            }
            catch (Exception e)
            {
                return new OperationResult(false, StringinObj.GetStringinObject(e), string.Concat(e.Message, msg));
            }
        }
    }
}
