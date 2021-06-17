namespace ManagerBot.DAL.Entities.Abstract
{
    public interface IConvertbleToTelegramButton
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
