using DragonCore.Domain.Models;
using ElasticSearch.Domain.Models;
using ElasticSearch.Domain.Models.QueryDescriptors;
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
    public class AccountController : Controller
    {
        private readonly IElasticSearchService _elasticClient;
        private readonly Account TestAccount1;
        private readonly Account TestAccount2;
        private readonly List<Account> Accounts;
        private readonly string AccountIndex = typeof(Account).Name.ToLowerInvariant();

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
            return Ok("Success " + AccountIndex);
        }

        [HttpGet]
        [Route("SearchAccountByMatches")]
        public async Task<IActionResult> SearchAccountByMatches()
        {
            try
            {
                var searchQuery = new SearchQuery<Account>(AccountIndex);

                var property = "accountId";

                searchQuery.AddShouldMatchCondtion(property, 2);

                searchQuery.AddShouldMatchCondtion(property, 1);

                var response = await _elasticClient.SearchAsync(searchQuery.QueryDescripter);

                return (response?.Count() > 0) ? Ok(response) : NotFound("AccountId: 1, 2");

            }
            catch (Exception ex)
            {
               
                return BadRequest("Error Occured: " + ex);
            }
        }

        [HttpGet]
        [Route("SearchAccountByIds")]
        public async Task<IActionResult> SearchAccountByIds()
        {
            try
            {
                var searchQuery = new SearchQuery<Account>(AccountIndex);

                searchQuery.AddDocIds(2,3);

                var response = await _elasticClient.SearchAsync(searchQuery.QueryDescripter);

                return (response?.Count() > 0) ? Ok(response) : NotFound("AccountId: ");
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
                var indexQuery = new IndexQuery<Account>(AccountIndex);

                indexQuery.AutoMapIndex();

                var response = await _elasticClient.CreateIndexAsync(AccountIndex, indexQuery.CreateIndexQueryDescripter);

                return (response.Success) ? Ok(response.DebugInformation) : BadRequest(response.OriginalException);
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
                var indexQuery = new IndexQuery<Account>(AccountIndex);
                
                indexQuery.DocumentId(TestAccount1.AccountId);

                var response = await _elasticClient.IndexAsync(TestAccount1, indexQuery.IndexQueryDescripter);

                return (response.Success) ? Ok(response.DebugInformation) : BadRequest(response.OriginalException);
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
                var bulkQuery = new BulkQuery<Account>(AccountIndex);

                bulkQuery.AddCollectionToSave(Accounts);

                var response = await _elasticClient.BulkAsync(bulkQuery.BulkDescriptor);

                return (response.Success) ? Ok(response.DebugInformation) : BadRequest(response.OriginalException);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }

        [HttpGet]
        [Route("UpdateDocument")]
        public async Task<IActionResult> UpdateDocument()
        {
            try
            {
                var updateQuery = new UpdateQuery<Account>(AccountIndex, TestAccount1.AccountId);

                updateQuery.EnableDocAsUpsert();
                updateQuery.EnableElasticShardRefresh();

                updateQuery.UpdateDocument(TestAccount2);


                //var response = await _elasticClient2.UpdateAsync<object>(TestAccount1.AccountId, u => u
                //.Index(AccountIndex)
                //.Doc(TestAccount1)
                //.DocAsUpsert()
                //.Refresh(Elasticsearch.Net.Refresh.True)
                //);

                var response = await _elasticClient.UpdateAsync(updateQuery.QueryDescripter);

                return (response.Success) ? Ok(response.DebugInformation) : BadRequest(response.OriginalException);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }
        }
    }
}
