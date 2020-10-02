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
  
  public class LienTests : IClassFixture<TestFixture<Startup>>
  {
    private HttpClient Client;

    public LienTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task TestGetAsyncOk()
    {
      // Arrange
      var request = "/api/lien";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdOk()
    {
      // Arrange
      var request = "/api/lien/1";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdShouldReturnNotFoundError()
    {
      // Arrange
      var request = "/api/lien/99999";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task TestSearchOk()
    {
      // Arrange
      var request = "api/lien/search?id=SortOrder=asc&PageIndex=0&PageSize=5";

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
        Url = "/api/lien",
        Body = new
        {
          url = "http://www.mincom.gov.ma",
          texte = "Texte lien test TI create",
          Desc = "Description Test",
          Ordre = 1,
          categorieId = 1
        }
      };

      // Act
      var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<LienDTO>(jsonFromPostResponse);

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
        Url = "/api/lien",
        Body = new
        {
          id = 1,
          url = "http://www.mincom.gov.ma/updated",
          texte = "Texte lien test TI update",
          Desc = "Description Test update",
          Ordre = 1,
          categorieId = 1
        }
      };

      // Act
      var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<LienDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.Id == 95);
      Assert.True(singleResponse.DateModification.Day == DateTime.Now.Day && singleResponse.DateModification.Month == DateTime.Now.Month);
    }

    [Fact]
    public async Task TestDeleteOk()
    {
      // Arrange
      var request = "api/lien/95";

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
