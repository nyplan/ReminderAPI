using AutoMapper;
using ReminderAPI.DTOs.ReminderDTOs;
using ReminderAPI.Entities;

namespace ReminderAPI.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ReminderToAddDto, Reminder>();
            CreateMap<ReminderToUpdateDto, Reminder>();
            CreateMap<Reminder, ReminderToReadDto>();
        }
    }
}
