using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TutoringRequest.Data.Repositories.Base;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.Enums;

namespace TutoringRequest.Data.Repositories.DatabaseRepositories;


public class TutorRepository : GenericRoleRepository, ITutorRepository
{
    public TutorRepository(TutoringDbContext context) : base(context, DefaultRoles.Tutor.ToString())
    {
        
    }

}
