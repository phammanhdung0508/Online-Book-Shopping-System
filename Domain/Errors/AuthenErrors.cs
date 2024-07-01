namespace Domain.Errors;

public static class AuthenErrors
{
    public static readonly Error LoginFail = new("Authentication.LoginFail", "Can't login email or password is incorrect.");
}
