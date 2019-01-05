using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisSite.Domain;

namespace ThesisSite.Services.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAll();

        Task<Course> GetCourseById(int id);

        bool Exists(int id);

        Task AddCourse(Course course);

        Task DeleteCourse(int id);

        Task UpdateCourse(Course course);
    }
}