using Arram.Core.API;
using Arram.Core.DTO;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Arram.Core.Tests.IT
{


  public class ParametreTests : IClassFixture<TestFixture<Startup>>
  {
    private HttpClient Client;

    public ParametreTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task TestGetAsyncOk()
    {
      // Arrange
      var request = "/api/parametre";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdOk()
    {
      // Arrange
      var request = "/api/parametre";

      // Act
      var response = await Client.GetAsync(request);
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<ParametreDTO>(jsonFromPostResponse);
      // Assert
      response.EnsureSuccessStatusCode();
    }    
  }
}