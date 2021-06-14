using DragonCore.Domain.Models;
using ElasticSearch.Domain.Models;
using ElasticSearch.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IElasticSearchService _elasticClient;
        private readonly Account TestAccount1;
        private readonly Account TestAccount2;
        private readonly List<Account> Accounts;
        private readonly string AccountIndex = "account";

        public AccountController(IElasticSearchService elasticClient)
        {
            _elasticClient = elasticClient;
            TestAccount1 = new Account() {
                AccountId = 1,
                Name = "TestAccount1",
                Surname = "TestAccount1Surname",
                CellNumber = "0724404998",
                Email = "test@gmail.com",
                CreationDate = DateTime.Now,
                DateOfBirth = new DateTime(1997,8,17)
            };

            TestAccount2 = new Account()
            {
                AccountId = 2,
                Name = "TestAccount2",
                Surname = "TestAccount2Surname",
                CellNumber = "0846500045",
                Email = "test@gmail.com",
                CreationDate = DateTime.Now,
                DateOfBirth = new DateTime(1997, 8, 17)
            };

            Accounts = new List<Account>()
            {
                TestAccount1,
                TestAccount2
            };
        }

        [HttpGet]
        [Route("GetTest")]
        public IActionResult GetTest()
        {

            return Ok("Success");

        }

        [HttpGet]
        [Route("SearchAccount")]
        public async Task<IActionResult> SearchAccount()
        {
            try
            {

                var searchDescriptor = new SearchQuery<Account>(AccountIndex);

                var property = "accountId";

                searchDescriptor.AddShouldMatchCondtion(property, 2);

                searchDescriptor.AddShouldMatchCondtion(property, 1);

                //searchDescriptor.AddDocIds(1,2);

                var response = await _elasticClient.SearchAsync(searchDescriptor.QueryDescripter);

                if(response?.Count() > 0)
                {
                    return Ok(response);
                }
                return NotFound("AccountId: ");
            }
            catch(Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }

        [HttpGet]
        [Route("CreateElasticIndex")]
        public async Task<IActionResult> CreateElasticIndex()
        {
            try
            {
                var response = await _elasticClient.CreateIndex<Account>(AccountIndex);
                if(response.Success)
                {
                    return Ok(response.DebugInformation);
                }
                throw response.OriginalException;
            }
            catch(Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }

        [HttpGet]
        [Route("IndexDocument")]
        public async Task<IActionResult> IndexDocument()
        {
            try
            {
                var response = await _elasticClient.SaveSingleAsync(TestAccount1, AccountIndex);
                if (response.Success)
                {
                    return Ok(response.DebugInformation);
                }
                throw response.OriginalException;
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }

        [HttpGet]
        [Route("IndexManyDocuments")]
        public async Task<IActionResult> IndexManyDocuments()
        {
            try
            {
                var response = await _elasticClient.SaveManyAsync(Accounts, AccountIndex);
                if (response.Success)
                {
                    return Ok(response.DebugInformation);
                }
                throw response.OriginalException;
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }

        [HttpGet]
        [Route("IndexBulkDocuments")]
        public async Task<IActionResult> IndexBulkDocuments()
        {
            try
            {
                var response = await _elasticClient.SaveBulkAsync(Accounts, AccountIndex);
                if (response.Success)
                {
                    return Ok(response.DebugInformation);
                }
                throw response.OriginalException;
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }

    }
}
