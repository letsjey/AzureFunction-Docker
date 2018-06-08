#r "Newtonsoft.Json"
#r "Microsoft.Azure.WebJobs.Extensions.Http"

using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Globalization;

    internal class Gamer
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("bornDate")]
        public string BornDate { get; set; }

        [JsonProperty("registerDate")]
        public string RegisterDate { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }

    }

public static IActionResult Run(HttpRequest req, TraceWriter log)
{

            log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["newJson"];
            string requestBody = new StreamReader(req.Body).ReadToEnd();
 
            dynamic jsonObj = JsonConvert.DeserializeObject(requestBody);
            var newGamer = JsonConvert.DeserializeObject<Gamer>(requestBody);


            newGamer.BornDate = DateTime.ParseExact(newGamer.BornDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            newGamer.RegisterDate = DateTime.Today.ToString();    
            
            ItCounts contador = new ItCounts();

            return newGamer != null
              ? (ActionResult)new OkObjectResult(
              "Datos de JSON: \n UserName:" + newGamer.UserName + " \n Field length: " + contador.countingLetters(newGamer.UserName)
              + " \n Name:" + newGamer.Name + " \n Field length: " + contador.countingLetters(newGamer.Name)
              + " \n Location:" + newGamer.Location + " \n Field length: " + contador.countingLetters(newGamer.Location)
              + " \n BornDate:" + newGamer.BornDate + " \n Field length: " + contador.countingLetters(newGamer.BornDate)
              + " \n RegisterDate:" + newGamer.RegisterDate + " \n Field length: " + contador.countingLetters(newGamer.RegisterDate)
              + " \n Rol:" + newGamer.Rol + " \n Field length: " + contador.countingLetters(newGamer.Rol))
              : new BadRequestObjectResult("Please try again with another JSON");


}



public class ItCounts
{	
	public int countingLetters(string word)
        {
            return word.Length;
	}
}
