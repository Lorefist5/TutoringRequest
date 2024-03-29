﻿using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Data.Repositories.TestRepositories;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.InMemoryRepositories;

public class MajorInMemoryRepo : GenericInMemoryRepo<Major>, IMajorRepository
{
    public MajorInMemoryRepo()
    {
    }

    public MajorInMemoryRepo(List<Major> entities) : base(entities)
    {
    }

    public List<Course> GetCoursesInMajor(Major major)
    {
        throw new NotImplementedException();
    }

    public Task<List<Course>> GetCoursesInMajorAsync(Major major)
    {
        throw new NotImplementedException();
    }
}
