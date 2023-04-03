using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Person
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [JsonPropertyName("firstName")]
    [Required]
    [Range(0,50)]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    [Required]
    [Range(0, 50)]
    public string LastName { get; set; }
    
    [Required]
    [MinLength(7)]
    [MaxLength(7)]
    public int GMC { get; set; }

}