using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GenericLibrary.Model;
using System.Collections.Generic;
using FinalGroupProject.SQLRepository;
using GenericLibrary.Database;

namespace FinalGroupProject
{
    public class GetLabelCount
    {
        private ISqlRepository _sqlRepository;

        public GetLabelCount(ISqlRepository sqlRepository, ISqlDbConnection databaseConnection)
        {
            _sqlRepository = sqlRepository;
            _sqlRepository.DatabaseConnection = databaseConnection;
        }

        [FunctionName("GetLabelCount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "labelCount")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                List<LabelCount> labelCounts = _sqlRepository.GetLabelCount();

                if (labelCounts == null)
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }

                return new OkObjectResult(labelCounts);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
