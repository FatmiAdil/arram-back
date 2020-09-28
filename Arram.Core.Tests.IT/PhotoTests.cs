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
  public class PhotoTests : IClassFixture<TestFixture<Startup>>
  {
    private HttpClient Client;

    public PhotoTests(TestFixture<Startup> fixture)
    {
      Client = fixture.Client;
    }

    [Fact]
    public async Task TestGetAsyncOk()
    {
      // Arrange
      var request = "/api/photo";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdOk()
    {
      // Arrange
      var request = "/api/photo/12";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task TestGetAsyncByIdShouldReturnNotFoundError()
    {
      // Arrange
      var request = "/api/photo/99999";

      // Act
      var response = await Client.GetAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task TestSearchOk()
    {
      // Arrange
      var request = "api/photo/search?id=&licenceId=&SortOrder=asc&PageIndex=0&PageSize=5";

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
        Url = "/api/photo",
        Body = new
        {
          LicenceId = 1,
          Description = "Photo test description",
          Url = "/photos/cn8zzz.jpg",          
        }
      };

      // Act
      var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<PhotoDTO>(jsonFromPostResponse);

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
        Url = "/api/photo",
        Body = new
        {
          id = 1006,
          LicenceId = 1,
          Description = "Phot test description updated",
          Url = "/photos/cn8zzz.jpg",
        }
      };

      // Act
      var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
      var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<PhotoDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.Id == 1006);
      Assert.True(singleResponse.DateModification.Day == DateTime.Now.Day && singleResponse.DateModification.Month == DateTime.Now.Month);
    }

    [Fact]
    public async Task TestDeleteOk()
    {
      // Arrange
      var request = "api/photo/66";

      // Act
      var response = await Client.DeleteAsync(request);
      var getResponse = await Client.GetAsync(request);
      var jsonFromPostResponse = await getResponse.Content.ReadAsStringAsync();
      var singleResponse = JsonConvert.DeserializeObject<PhotoDTO>(jsonFromPostResponse);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.True(singleResponse.IsDeleted);
    }
  }
}