﻿using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Data.Repositories.TestRepositories;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Data.Repositories.InMemoryRepositories;

public class AdministratorInMemoryRepo : GenericInMemoryRepo<Administrator>, IAdministratorRepository
{

}
