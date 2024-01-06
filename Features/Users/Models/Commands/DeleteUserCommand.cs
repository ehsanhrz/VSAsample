namespace Features.Users.Models.Commands;

public record DeleteUserCommand
{
    public DeleteUserCommand()
    {
        
    }

    public long Id { get; set; }
};