using ReminderAPI.DTOs.ReminderDTOs; 

namespace ReminderAPI.Services.Abstract
{
    public interface IReminderMappingService
    {
        Task CreateAsync(ReminderToAddDto dto);
        IEnumerable<ReminderToReadDto> Read();
        Task Update(ReminderToUpdateDto dto);
        Task DeleteRange(IEnumerable<int> ids);
    }
}
