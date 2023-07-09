using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ProjectDatabase.Models
{
    public class UserStoreRollModelView
    {
        public string ? UserName { get; set; }
        public string ? Role { get; set; }
        public string ? Store { get; set; }

    }
}
