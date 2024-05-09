using System.Text.Json;
using Chemicals.Core.Interfaces.Integration;
using Shared.Integration.Authorization;
using Shared.Integration.Configuration;
using Shared.Integration.Models.Dtos;

namespace Chemicals.Core.Services;

public class WsHttpService : IWsHttpService
{
    private readonly HttpClient _httpClient;

    public WsHttpService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization",
            "Bearer " + IntegrationAuthService.GetIntegrationToken());
    }

    public async Task<List<SharedWarningSentenceDto>> GetActiveWarningSentenceAsync()
    {
        var response = await _httpClient.GetAsync(Config.IntegrationEndpoints.WarningSentenceIntegration);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<SharedWarningSentenceDto>>(content)!;
    }
}