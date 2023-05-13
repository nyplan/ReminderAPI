using AutoMapper;
using ReminderAPI.DTOs.ReminderDTOs;
using ReminderAPI.Entities;
using ReminderAPI.Repositories.Abstract;
using ReminderAPI.Services.Abstract;

namespace ReminderAPI.Services.Concrete
{
    public class ReminderMappingService : IReminderMappingService
    {
        private readonly IMapper _mapper;
        private readonly IReminderRepository _reminderRepository;
        public ReminderMappingService(IMapper mapper, IReminderRepository reminderRepository)
        {
            _mapper = mapper;
            _reminderRepository = reminderRepository;
        }

        public async Task CreateAsync(ReminderToAddDto dto)
        {
            await _reminderRepository.CreateAsync(_mapper.Map<Reminder>(dto));
        }

        public IEnumerable<ReminderToReadDto> Read()
        {
            IEnumerable<Reminder> reminders = _reminderRepository.Read();
            IEnumerable<ReminderToReadDto> dtos = _mapper.Map<IEnumerable<ReminderToReadDto>>(reminders);
            return dtos;
        }

        public async Task Update(ReminderToUpdateDto dto)
        {
            Reminder reminder = _mapper.Map<Reminder>(dto);
            await _reminderRepository.UpdateReminderAsync(reminder);
        }

        public async Task DeleteRange(IEnumerable<int> ids)
        {
            IEnumerable<Reminder> reminders = _reminderRepository.Read(c => ids.Contains(c.Id));
            await _reminderRepository.DeleteRangeAsync(reminders);
        }
       
    }
}
