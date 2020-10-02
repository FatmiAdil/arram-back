using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Arram.Core.DTO;
using Arram.Core.API;
using Xunit;

namespace Arram.Core.Tests.IT
{  
  public class RefTypeArticleTests : IClassFixture<TestFixture<Startup>>
  {
    private HttpClient Client;

    public RefTypeArticleTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task TestGetAsyncOk()
    {
      // Arrange
      var request = "/api/reftypearticle";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdOk()
    {
      // Arrange
      var request = "/api/reftypearticle/1";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdShouldReturnNotFoundError()
    {
      // Arrange
      var request = "/api/reftypearticle/99999";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task TestSearchOk()
    {
      // Arrange
      var request = "api/reftypearticle/search?id=SortOrder=asc&PageIndex=0&PageSize=5";

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
        Url = "/api/reftypearticle",
        Body = new
        {
          libelle = "Create RetTypeArticle test",
          
        }
      };

      // Act
      var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<TypeArticleDTO>(jsonFromPostResponse);

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
        Url = "/api/reftypearticle",
        Body = new
        {
          Id = 16,
          libelle = "Create RetTypeArticle test updated",
        }
      };

      // Act
      var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<TypeArticleDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.Id == 16);
      Assert.True(singleResponse.DateModification.Day == DateTime.Now.Day && singleResponse.DateModification.Month == DateTime.Now.Month);
    }

    [Fact]
    public async Task TestDeleteOk()
    {
      // Arrange
      var request = "api/reftypearticle/1";

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
