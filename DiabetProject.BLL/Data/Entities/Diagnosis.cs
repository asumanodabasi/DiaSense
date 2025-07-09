using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiabetProject.BLL.Data.Dto.Enums;

namespace DiabetProject.BLL.Data.Entities
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        public Gender Gender { get; set; } 
        public float Age { get; set; }
        public Section Hypertension { get; set; }
        public Section HeartDisease { get; set; }
        public Smooking SmokingHistory { get; set; } 
        public float Bmi { get; set; }
        public float HbA1cLevel { get; set; }
        public float BloodGlucoseLevel { get; set; } 
        public float Weight { get; set; }
        public float Height { get; set; }
        public DateTime CreatedAt { get; set; }

        public int Predict { get; set; }
    }
}
