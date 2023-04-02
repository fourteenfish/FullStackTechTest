using System.Text.Json.Serialization;

namespace Models;

public class Address
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public int PersonId { get; set; }
    [JsonPropertyName("line1")]
    public string Line1 { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("postcode")]
    public string Postcode { get; set; }
    
    // assume all records will be UK, i.e. disregard "Country" field
}