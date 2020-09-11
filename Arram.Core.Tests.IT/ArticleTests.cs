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
  public class ArticleTests : IClassFixture<TestFixture<Startup>>
  {
    private HttpClient Client;

    public ArticleTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task TestGetAsyncOk()
    {
      // Arrange
      var request = "/api/article";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdOk()
    {
      // Arrange
      var request = "/api/article/51";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdShouldReturnNotFoundError()
    {
      // Arrange
      var request = "/api/article/99999";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task TestSearchOk()
    {
      // Arrange
      var request = "api/article/search?id=SortOrder=asc&PageIndex=0&PageSize=5";

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
        Url = "/api/article",
        Body = new
        {
          titre = "TI description tests create",
          texte = "Description article test TI create",
          dateArticle = DateTime.Now,
          licenceId = 339,
          typeArticleId = 1
        }
      };

      // Act
      var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<ArticleDTO>(jsonFromPostResponse);

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
        Url = "/api/article",
        Body = new
        {
          id = 95,
          titre = "TI description tests update",
          texte = "Description article test TI",
          dateArticle = DateTime.Now,
          licenceId = 339,
          typeArticleId = 1
        }
      };

      // Act
      var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<LicenceDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.Id == 95);
      Assert.True(singleResponse.DateModification.Day == DateTime.Now.Day && singleResponse.DateModification.Month == DateTime.Now.Month);
    }

    [Fact]
    public async Task TestDeleteOk()
    {
      // Arrange
      var request = "api/article/95";

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
