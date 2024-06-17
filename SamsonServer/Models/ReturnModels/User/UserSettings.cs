using SamsonCommon.Enums;

namespace SamsonServer.Models.ReturnModels.User;

public class UserSettings
{
    public int Id { get; set; }
    
    public ListenMode ListenMode { get; set; }
        
    public int ListenDuration { get; set; }
}