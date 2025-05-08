namespace Manager_Device_Service.Services.Interfaces
{
    public interface IBorrowReminderService
    {
        void SendReturnReminderMail(string toEmail, string deviceName, DateTime dueDate);

    }
}
