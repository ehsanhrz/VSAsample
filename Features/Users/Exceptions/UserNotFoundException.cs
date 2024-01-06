namespace Features.Users.Exceptions;

public class UserNotFoundException : Exception
{
    public override string Message => "There is no such user with given context.";
}