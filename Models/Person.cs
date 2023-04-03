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

    public bool ValidatePerson(Person person)
    {
        if (person.FirstName.Length > 50)
        {
            throw new ArgumentOutOfRangeException("FirstName needs to less than 50 characters");
        }
        if (person.LastName.Length > 50)
        {
            throw new ArgumentOutOfRangeException("LastName needs to less than 50 characters");
        }
        if (person.GMC > 0)
        {
            if (person.GMC.ToString().Length > 7)
            {
                throw new ArgumentOutOfRangeException("GMC must be 7 digits");
            }
        }

        return true;

    }
}