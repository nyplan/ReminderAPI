using FluentValidation;
using ReminderAPI.DTOs.ReminderDTOs;

namespace ReminderAPI.Validators.Reminder
{
    public class UpdateValidator : AbstractValidator<ReminderToUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .NotEqual(0);
            RuleFor(x => x.SendAt)
                .GreaterThan(DateTime.UtcNow);
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Recipient)
                .NotNull()
                .NotEmpty()
                .Matches(@"^([\w.-]+)@([\w-]+)(\.[\w-]+)*$|^@[A-Za-z_][A-Za-z_\d]*$")
                .WithMessage("Recipient must be a valid email address or username.");
            RuleFor(r => r.Method)
                .NotNull()
                .NotEmpty()
                .Matches(@"^(telegram|email)$")
                .WithMessage("Method must be either 'telegram' or 'email'.");
        }
    }
}
