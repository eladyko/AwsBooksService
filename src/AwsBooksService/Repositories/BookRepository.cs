using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AwsBooksService.Contract;
using AwsBooksService.Contract.Dtos;

namespace AwsBooksService.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IAmazonDynamoDB _dynamoDB;
        private const string TABLE_NAME = "books";

        public BookRepository(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var request = new DeleteItemRequest
            {
                TableName = TABLE_NAME,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "pk", new AttributeValue(id.ToString()) }
                }
            };

            var response = await _dynamoDB.DeleteItemAsync(request);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var request = new ScanRequest
            {
                TableName = TABLE_NAME,
            };

            var response = await _dynamoDB.ScanAsync(request);

            return response.Items.Select(x =>
            {
                var json = Document.FromAttributeMap(x).ToJson();
                return JsonSerializer.Deserialize<BookDto>(json);
            });
        }

        public async Task<BookDto> GetAsync(Guid id)
        {
            var request = new GetItemRequest
            {
                TableName = TABLE_NAME,
                Key = new Dictionary<string, AttributeValue>
                {
                    { nameof(Book.Id), new AttributeValue(id.ToString()) }
                }
            };

            var response = await _dynamoDB.GetItemAsync(request);

            if (response.Item.Count == 0)
            {
                return null;
            }    

            var asDocument = Document.FromAttributeMap(response.Item);

            return JsonSerializer.Deserialize<BookDto>(asDocument.ToJson());
        }

        public async Task<bool> UpdateAsync(BookDto item)
        {
            var asJson = JsonSerializer.Serialize(item);
            var asAttributes = Document.FromJson(asJson).ToAttributeMap();

            var updateRequest = new PutItemRequest
            {
                TableName = TABLE_NAME,
                Item = asAttributes
            };

            var response = await _dynamoDB.PutItemAsync(updateRequest);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> CreateAsync(BookDto item)
        {
            var asJson = JsonSerializer.Serialize(item);
            var asAttributes = Document.FromJson(asJson).ToAttributeMap();

            var createRequest = new PutItemRequest
            {
                TableName = TABLE_NAME,
                Item = asAttributes,
            };

            var response = await _dynamoDB.PutItemAsync(createRequest);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
