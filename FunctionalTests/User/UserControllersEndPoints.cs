namespace FunctionalTests.User;

public static class UserControllersEndPoints
{
    public static string Create { get; set; } = "Users/Create";
 
    public static string Update { get; set; } = "Users/Update";

    public static string GetUserByEmail { get; set; } = "Users/GetUserByEmail";

    public static string Delete { get; set; } = "Users/Delete";

    public static string GetUserById { get; set; } = "Users/GetUserById";
}