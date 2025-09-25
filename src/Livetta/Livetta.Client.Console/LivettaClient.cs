using System.Net;
using System.Net.Http.Json;
using System.Security.Authentication;
using Livetta.Application.DTO.Messages;

namespace Livetta.Client.Console;

public sealed class LivettaClient
{
    const string BaseUri = "http://localhost:5100/";
    const string BaseApiUri = BaseUri + "api/";

    string? _jwtToken;

    public string? JwtToken => _jwtToken;

    public string ChatHubUri { get; } = BaseUri + "hub/chat";
    
    public async Task LoginAsync(Guid residentId, CancellationToken ctk = default)
    {
        using var client = new HttpClient();

        using var response = await client.PostAsync(
            BaseApiUri + $"v1/Identity/{residentId}", new StringContent(string.Empty), ctk).ConfigureAwait(false);

        GuardResponse(response);
        
        _jwtToken = await response.Content.ReadAsStringAsync(ctk).ConfigureAwait(false);
        _jwtToken = _jwtToken.Replace("\"", string.Empty);
    }

    public async Task<MessageReadDto?> SendMessageAsync(Guid chatId, string text, CancellationToken ctk = default)
    {
        using var client = new HttpClient();

        if (string.IsNullOrWhiteSpace(_jwtToken))
            throw new AuthenticationException("No access token");
        
        client.DefaultRequestHeaders.Authorization = new("Bearer", _jwtToken);

        var request = new MessageCreateDto(text);
        
        using var response = await client.PostAsJsonAsync(
            BaseApiUri + $"v1/Chats/{chatId}/messages", request, ctk).ConfigureAwait(false);
        
        GuardResponse(response);

        return await response.Content.ReadFromJsonAsync<MessageReadDto>(ctk).ConfigureAwait(false);
    }

    static void GuardResponse(HttpResponseMessage response, HttpStatusCode code = HttpStatusCode.OK)
    {
        if (response.StatusCode != code)
            throw new InvalidOperationException("Invalid status code response");
    }
}