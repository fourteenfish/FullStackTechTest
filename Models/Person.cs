using System.Text.Json.Serialization;

namespace Models;

public class Person
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    public int GMC { get; set; }
}