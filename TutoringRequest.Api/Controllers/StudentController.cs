using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TutoringRequest.Data;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Students;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

}
