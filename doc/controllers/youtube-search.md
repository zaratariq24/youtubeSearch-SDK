# Youtube Search

```csharp
YoutubeSearchController youtubeSearchController = client.YoutubeSearchController;
```

## Class Name

`YoutubeSearchController`


# Get Search Results

```csharp
GetSearchResultsAsync(
    string part,
    string key,
    string q = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `part` | `string` | Query, Required | - |
| `key` | `string` | Query, Required | - |
| `q` | `string` | Query, Optional | The search query with the search term for youtube |

## Response Type

`Task<dynamic>`

## Example Usage

```csharp
string part = "snippet";
string key = "AIzaSyBuwl45ObqR_g7Viy6S7RqHKLJDTqNs1n4";

try
{
    dynamic result = await youtubeSearchController.GetSearchResultsAsync(part, key, null);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | invalidChannelID | [`BadRequestException`](../../doc/models/bad-request-exception.md) |

