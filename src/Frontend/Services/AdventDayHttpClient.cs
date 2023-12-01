// <copyright file="AdventDayHttpClient.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text.Json;

using Zearain.AoC23.Contracts.Requests;
using Zearain.AoC23.Contracts.Responses;

namespace Zearain.AoC23.Frontend.Services;

/// <summary>
/// Represents a client for the Advent Day API.
/// </summary>
public class AdventDayHttpClient
{
    private const string BaseUrl = "api/adventday";

    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdventDayHttpClient"/> class.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> to use.</param>
    public AdventDayHttpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <summary>
    /// Gets all Advent Days.
    /// </summary>
    /// <returns>A collection of <see cref="AdventDayResponse"/>s.</returns>
    public async Task<IEnumerable<AdventDayResponse>> GetAdventDaysAsync()
    {
        return await this.httpClient.GetFromJsonAsync<IEnumerable<AdventDayResponse>>(BaseUrl) ?? Array.Empty<AdventDayResponse>();
    }

    /// <summary>
    /// Creates a new Advent Day.
    /// </summary>
    /// <param name="dayNumber">The day number.</param>
    /// <returns>The created <see cref="AdventDayResponse"/>.</returns>
    public async Task<AdventDayResponse> CreateAdventDayAsync(int dayNumber)
    {
        var response = await this.httpClient.PostAsJsonAsync($"{BaseUrl}?dayNumber={dayNumber}", string.Empty);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<AdventDayResponse>(content)!;
    }

    /// <summary>
    /// Attempts to solve the specified Advent Day.
    /// </summary>
    /// <param name="adventDayId">The ID of the Advent Day to solve.</param>
    /// <param name="partNumber">The part number to solve.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task SolvePartAsync(Guid adventDayId, int partNumber)
    {
        var response = await this.httpClient.GetAsync($"{BaseUrl}/{adventDayId}/solve/{partNumber}");
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Uploads a file for the specified Advent Day.
    /// </summary>
    /// <param name="adventDayId">The ID of the Advent Day to upload the file for.</param>
    /// <param name="fileContent">The file content as base64 string.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task UploadFileAsync(Guid adventDayId, string fileContent)
    {
        var response = await this.httpClient.PostAsJsonAsync($"{BaseUrl}/{adventDayId}/input", new AdventDayFileInput
        {
            AdventDayId = adventDayId,
            FileContent = fileContent,
        });
        response.EnsureSuccessStatusCode();
    }
}