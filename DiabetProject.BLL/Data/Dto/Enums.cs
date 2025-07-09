using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetProject.BLL.Data.Dto
{
    public class Enums
    {
        public enum Gender 
        {
            [Display(Name ="Kadın")]
            Female=1,
            [Display(Name = "Erkek")]
            Male =2,
            [Display(Name = "Diğer")]
            Other =3,

        }
        public enum Section
        {
            Var=1,
            Yok=2
        }

        public enum Smooking
        {
            [Display(Name ="Hiç İçmedi")]
            HicIcmedi=1,
            [Display(Name ="Bilgi Yok")]
            BilgiYok =2,
            [Display(Name ="Hala İçiyor ")]
            SuanIciyor =3,
            [Display(Name ="Eskiden İçiyordu")]
            EskidenIciyordu =4,
            [Display(Name = "Çok Az İçmiş")]
            CokAzIcmis =5,
            [Display(Name ="Pasif İçici")]
            PasifIcici =6
        }

    }
}
