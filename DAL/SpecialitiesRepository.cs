using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class SpecialitiesRepository : GenericRepository<Speciality>, ISpecialitiesRepository
{
    public SpecialitiesRepository(DoctorDbContext doctorDbContext) :base(doctorDbContext)
    {
       
    }

}