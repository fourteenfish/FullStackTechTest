
namespace Models
{
    /// <summary>
    /// Junction table model for people to specialities many-to-many relationship
    /// </summary>
    public class PeopleSpecialities
    {
        public int PersonId { get; set; }
        public int SpecialityId { get; set; }
    }
}
