using AutoMapper;
using Bitcoin.Wallet.Api.Models;
using Bitcoin.Wallet.Api.Models.Request;
using Bitcoin.Wallet.Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bitcoin.Wallet.Api.Interfaces;
using Bitcoin.Wallet.Api.Common;

namespace Bitcoin.Wallet.Api.Controllers
{
    [RoutePrefix("bitcoin/api")]
    public class TransactionController : ApiController
    {
        private readonly IDbExecutor _dbExecutor;
        public TransactionController(IDbExecutor dbExecutor)
        {
            _dbExecutor = dbExecutor;
        }


        /// <summary>
        /// Sending bitcoins to receiver
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendBtc")]
        public async Task<IHttpActionResult> SendBtc([FromBody] BitcoinRequest request)
        {
            await Task.Run(async () =>
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                var query = Mapper.Map<BitcoinRequest,BitcoinQuery>(request);
                await _dbExecutor.SaveTransactions(query);

            });
            return Ok();
        }


        /// <summary>
        /// Getting Last Transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLast")]
        [ResponseType(typeof(IEnumerable<TransactionResponse>))]
        public async Task<IHttpActionResult> GetLast()
        {
            IList<TransactionResponse> mappedTransactions = null;
            await Task.Run(async () =>
            {
                var latestTransactions = await _dbExecutor.GetLatestTransactions();
                var requestedIds = Finder.RequestedFinder(latestTransactions);
                await _dbExecutor.UpdateRequestedTransactions(requestedIds);
                mappedTransactions = Mapper.Map<IList<Transaction>, IList<TransactionResponse>>(latestTransactions);
            });
           
            return Ok(mappedTransactions);
        }
    }
}
