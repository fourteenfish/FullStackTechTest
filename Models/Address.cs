namespace Models;

public class Address
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Line1 { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    
    // assume all records will be UK, i.e. disregard "Country" field
}