using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Address
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [JsonIgnore]
    public int PersonId { get; set; }
    
    [JsonPropertyName("line1")]
    [Required]
    [MaxLength(200)]
    public string Line1 { get; set; }
    
    [JsonPropertyName("city")]
    [Required]
    [MaxLength(100)]
    public string City { get; set; }
    
    [JsonPropertyName("postcode")]
    [Required]
    [MinLength(8)]
    [MaxLength(8)]
    public string Postcode { get; set; }
    
    // assume all records will be UK, i.e. disregard "Country" field
}