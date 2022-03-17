using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Onyx.Contracts.DTO;
using Onyx.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Onyx.EndToEndTests
{
    public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private IConfiguration configuration;

        public ProductControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<DatabaseContext>();
                        configuration = scopedServices.GetRequiredService<IConfiguration>();
                    }
                });
            });

            _client = client.CreateClient();
            SetAuthorization.AddBearerToken(_client, configuration);
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            var httpResponse = await _client.GetAsync("");
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }

        [Fact]
        public async Task GetAllProduct_ShouldReturnListOfProducts()
        {
            var httpResponse = await _client.GetAsync("api/Products");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(products);
            Assert.Equal(4, products.Count());
        }

        [Fact]
        public async Task GetProductByColour_ShouldReturnSameColourProducts()
        {
            var httpResponse = await _client.GetAsync("api/Products/Red");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(products);
            Assert.Equal(2, products.Count());
            Assert.True(products.All(a => a.ProductColour == "Red"));
        }

        public static class SetAuthorization
        {
            public static void AddBearerToken(HttpClient client, IConfiguration configuration)
            {
                var tokenService = new TokenService();
                var token = tokenService.BuildToken(configuration["JWT:Key"], configuration["JWT:Issuer"]);

                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }
    }
}