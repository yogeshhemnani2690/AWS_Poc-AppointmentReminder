using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Transform;
using DynamoDb_Library.Interfaces;
using DynamoDb_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamoDb_Library.DynamoDb
{
    public class DynamoDbServices : IDynamoDb
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string TableName = "PocAppointments";
       public  async void  CreateDynamoDbTable()
        {
            var amazonDbClient = new AmazonDynamoDBClient();
            Console.WriteLine("Fetching Existing Tables");
            ListTablesResponse currentTables = await amazonDbClient.ListTablesAsync();
            Console.WriteLine("Existing Tables" +currentTables.TableNames);
            if (!(currentTables.TableNames.Contains(TableName)))
            {
                var createTableReq = new CreateTableRequest()
                {
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "PocAppointmentId",
                            AttributeType = "N"
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "PocAppointmentDate",
                            AttributeType = "S"
                        },
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "PocAppointmentId",
                            KeyType = "HASH"
                        },
                        new KeySchemaElement
                        {
                            AttributeName = "PocAppointmentDate",
                            KeyType = "Range"
                        },
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 5,
                        WriteCapacityUnits = 5
                    },
                    TableName = TableName
                };
                var response = await amazonDbClient.CreateTableAsync(createTableReq);

                Console.WriteLine("Table Created " +response.ResponseMetadata.RequestId);
            }
        }

        public DynamoDbServices(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;

        }

        public  void isTableReady(string tablename)
        {
            string status = null;
            do
            {
                Thread.Sleep(5000);
                try
                {
                    var result =  _dynamoDbClient.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tablename
                    });
                    status = result.Result.Table.TableStatus;
                }
                catch (Amazon.Runtime.Internal.HttpErrorResponseException)
                {

                }
                catch (ResourceNotFoundException)
                {


                }
            }
            while (status != "ACTIVE");
            {
                Console.WriteLine("Table Created");
            }
        }

        public async Task<Appointments> GetAppointments(int? id)
        {
            
            var queryRequest = RequestBuilder(id);
            var result = await ScanAsync(queryRequest);
            return new Appointments
            {
                Items = result.Items.Select(Map).ToList()
            };
        }

        private async Task<ScanResponse> ScanAsync(ScanRequest queryRequest)
        {
            var response = await _dynamoDbClient.ScanAsync(queryRequest);
            Console.WriteLine(response.ResponseMetadata);
            return response; 
        }
        private Items Map(Dictionary<string, AttributeValue> result)
        {
            return new Items
            {
                PocAppointmentId = Convert.ToInt32(result["PocAppointmentId"].N),
                PocAppointmentDate = Convert.ToString(result["PocAppointmentDate"].S) ,
                PocEmail = Convert.ToString(result["PocEmail"].S) ,
                PocPatientName = Convert.ToString(result["PocPatientName"].S) ,
                PocDoctorName = Convert.ToString(result["PocDoctorName"].S)
            };
        }

        private ScanRequest RequestBuilder(int? id)
            {
                if (id.HasValue == false)
                {
                    return new ScanRequest
                    {
                        TableName = TableName
                    };
                }
                return new ScanRequest
                {
                    TableName = TableName,
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                 {":v_pocAppointmentId", new AttributeValue
                    { N = id.ToString() }
                 }
                },
                    FilterExpression = $"PocAppointmentId=:v_pocAppointmentId",
                    ProjectionExpression= "PocAppointmentId,PocAppointmentDate,PocEmail,PocPatientName,PocDoctorName"
                };

            }
        
        public async Task<Appointments> GetNextDayAppointmentsAsync(string nextDate)
        {
            var queryRequest = new ScanRequest
            {
                TableName = TableName,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {
                        ":v_pocAppointmentDate", new AttributeValue
                        { S = nextDate.ToString() }
                    }
                },
                FilterExpression = $"PocAppointmentDate=:v_pocAppointmentDate",
                ProjectionExpression = "PocAppointmentId,PocAppointmentDate,PocEmail,PocPatientName,PocDoctorName"
            };
            var result = await ScanAsync(queryRequest);
            return new Appointments
            {
                Items = result.Items.Select(Map).ToList()
            };
        }

    }
}
