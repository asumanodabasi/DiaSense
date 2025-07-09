using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetProject.BLL.Data.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string FullName => String.Join("", FirstName +" "+ LastName);
        public ICollection<Diagnosis> Predictions { get; set; }

    }
}
