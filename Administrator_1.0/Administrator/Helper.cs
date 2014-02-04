/// <summary>
/// The Actor status: Activated or Deactivated.
/// </summary>
public enum Status 
{
    Activated,
    Deactivated
}

/// <summary>
/// Contains a group of function that helps in programming.
/// </summary>
public static class Helper
{
    public static Status StringToStatus(string str)
    {
        Status st ;
        if (str == "Activated")
            st = Status.Activated;
        else
            st = Status.Deactivated;

        return st;
    }
}