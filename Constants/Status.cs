namespace AssetwiseApi.Constants;
public enum ServiceStatus{
    New,
    Processing,
    Declined,
    Approved,
    Pending,
    Incomplete,
    Invalid
}

public enum ServiceReminderStatus{
    New,
    Overdue,
    Pending
}
