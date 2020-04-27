using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SolTechnology.Avro;
using Newtonsoft.Json;

namespace POC_kafka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvroMessageController : ControllerBase
    {
        private readonly ILogger<AvroMessageController> logger;
        private readonly IConfiguration configuration;
        private IProducerService producerService;
        private IConsumerService consumerService;
        private AvroMessage AvroMsg;
        private string result;

        public AvroMessageController(IProducerService _producerService, IConsumerService _consumerService, IConfiguration _configuration, ILogger<AvroMessageController> _logger)
        {
            producerService = _producerService;
            consumerService = _consumerService;
            configuration = _configuration;
            logger = _logger;
        }

        [HttpPost("avro")]
        public void AvroProcess(AvroMessage avroMessage)
        {
            try
            {
                //mapIncomeData(avroMessage);
                mapIncomeData();

                logMappedIncomeData(AvroMsg);

                byte[] avroObject = AvroConvert.Serialize(AvroMsg);

                Console.WriteLine("avroObject", avroObject);

                //sendIncomeDataToKafka(AvroMsg);
                sendIncomeDataToKafka(avroObject);

                logEndProcess(result);

                consumirMensagem();

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERR AVRO: " + ex.Message);
            }
        }

        //protected void mapIncomeData(AvroMessage avroMessage)
        protected void mapIncomeData()
        {
            AvroMsg = new AvroMessage();
            //AvroMsg.Create(avroMessage);
            AvroMsg.Create();
        }

        protected void logMappedIncomeData(AvroMessage AvroMsg)
        {
            var objReceived = JsonConvert.SerializeObject(AvroMsg, Formatting.Indented);
            logger.LogInformation($"DataShow: {objReceived}");
        }

        protected void sendIncomeDataToKafka(byte[] avroObject)
        //protected void sendIncomeDataToKafka(AvroMessage avroObject)
        {
            result = producerService.SendMessage("IMPORTED_FILE_AVRO", avroObject.ToString(), avroObject);
        }

        protected void logEndProcess(string result)
        {
            logger.LogInformation("KafkaProducerService status: " + result);
            logger.LogInformation($"Process END at {MyTimeZone.Now}");
        }

        protected void consumirMensagem()
        {
            consumerService.Subscribe("IMPORTED_FILE_AVRO", "IMPORTED_FILE_AVRO_GROUP");

            while (true)
            {
                ConsumerResult result = consumerService.ConsumeMessage();
                if (String.IsNullOrEmpty(result.Message)) return;

                var response = JsonConvert.DeserializeObject<AvroMessage>(result.Message);
                Console.WriteLine("Response: ", response);
            }
        }

    }
}
