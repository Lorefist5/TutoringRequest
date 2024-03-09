using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorAvailabilitySlotController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TutorAvailabilitySlotController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    //private async Task<bool> CheckForSlotOverlap(Tutor tutor, AvailabilitySlot availabilitySlot)
    //{
    //    // Check for slots that overlap with the new slot
    //    var overlappingSlots = await _unitOfWork.AvailabilitySlotRepository
    //        .WhereAsync(slot => tutor.Id == slot.TutorId &&
    //            ((availabilitySlot.StartTime >= slot.StartTime && availabilitySlot.StartTime <= slot.EndTime) ||
    //             (availabilitySlot.EndTime >= slot.StartTime && availabilitySlot.EndTime <= slot.EndTime)));





    //    overlappingSlots = overlappingSlots.Where(slot => !(availabilitySlot.StartTime <= slot.StartTime && availabilitySlot.EndTime >= slot.EndTime)).ToList();

    //    // Return true if there are any overlapping slots
    //    return overlappingSlots.Any();
    //}
}
