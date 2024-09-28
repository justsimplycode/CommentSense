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
using CsvHelper;
using System.Globalization;
using System.Data;
using System.Collections.Generic;
using GenericLibrary.Model;

namespace FinalGroupProject
{
    public class PostCommentDetail
    {
        private ISqlRepository _sqlRepository;

        public PostCommentDetail(ISqlRepository sqlRepository, ISqlDbConnection databaseConnection)
        {
            _sqlRepository = sqlRepository;
            _sqlRepository.DatabaseConnection = databaseConnection;
        }
        [FunctionName("PostCommentDetail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "comments")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try 
            {


                //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                //if (string.IsNullOrEmpty(requestBody))
                //{
                //    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                //}

                //List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(requestBody);

                //_sqlRepository.PostCommentDetails(comments);

                _sqlRepository.PostCommentDetailsFromCSV();

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
