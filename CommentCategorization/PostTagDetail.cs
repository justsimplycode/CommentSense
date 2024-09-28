using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FinalGroupProject.SQLRepository;
using GenericLibrary.Database;
using System.Collections.Generic;
using GenericLibrary.Model;

namespace FinalGroupProject
{
    public class PostTagDetail
    {
        private ISqlRepository _sqlRepository;

        public PostTagDetail(ISqlRepository sqlRepository, ISqlDbConnection databaseConnection)
        {
            _sqlRepository = sqlRepository;
            _sqlRepository.DatabaseConnection = databaseConnection;
        }
        [FunctionName("PostTagDetail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tag")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                Tag tag = JsonConvert.DeserializeObject<Tag>(requestBody);

                _sqlRepository.PostTagDetail(tag);

                return new StatusCodeResult(StatusCodes.Status201Created); 
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
