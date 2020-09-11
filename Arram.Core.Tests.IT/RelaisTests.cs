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
  public class RelaisTests : IClassFixture<TestFixture<Startup>>
  {
    private HttpClient Client;

    public RelaisTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task TestGetAsyncOk()
    {
      // Arrange
      var request = "/api/relais";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdOk()
    {
      // Arrange
      var request = "/api/relais/17";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdShouldReturnNotFoundError()
    {
      // Arrange
      var request = "/api/relais/99999";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task TestSearchOk()
    {
      // Arrange
      var request = "api/relais/search?id=SortOrder=asc&PageIndex=0&PageSize=5";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestCreateOk()
    {
      // Arrange
      var request = new
      {
        Url = "/api/relais",
        Body = new
        {
          region = "Region TI",
          site ="Site TI",
          nom = "Nom relais TI created",
          altitude = 0,
          freqEntree  = 145100,
          freqSortie = 145600,
          bande  = 1,
          shift = 600,
          tone = "1750Hz",
          puissance  = 25,
          qraLocator = "QRA10TI",
          latitude  = 35.4d,
          longitude = -5.45,
        }
      };

      // Act
      var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<RelaisDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.Id.HasValue);
    }

    [Fact]
    public async Task TestUpdateOk()
    {
      // Arrange
      var request = new
      {
        Url = "/api/relais",
        Body = new
        {
          id = 18,
          region = "Region TI",
          site = "Site TI",
          nom = "Nom relais TI updated",
          altitude = 0,
          freqEntree = 145100,
          freqSortie = 145600,
          bande = 1,
          shift = 600,
          tone = "1750Hz",
          puissance = 25,
          qraLocator = "QRA10TI",
          latitude = 35.4d,
          longitude = -5.45,
        }
      };

      // Act
      var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<LicenceDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.Id == 18);
      Assert.True(singleResponse.DateModification.Day == DateTime.Now.Day && singleResponse.DateModification.Month == DateTime.Now.Month);
    }

    [Fact]
    public async Task TestDeleteOk()
    {
      // Arrange
      var request = "api/relais/18";

      // Act
      var response = await Client.DeleteAsync(request);
      var getResponse = await Client.GetAsync(request);
      var jsonFromPostResponse = await getResponse.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<LicenceDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.IsDeleted);
    }
  }
}
