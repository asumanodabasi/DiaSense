using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DiabetProject.BLL.Data.Dto.Enums;

namespace DiabetProject.BLL.Data.Dto
{

    public class DiagnosisDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("age")]
        public float Age { get; set; }

        [JsonPropertyName("hypertension")]
        public Section Hypertension { get; set; }

        [JsonPropertyName("heart_disease")]
        public Section HeartDisease { get; set; }

        [JsonPropertyName("smoking_history")]
        public Smooking SmokingHistory { get; set; }

        [JsonPropertyName("bmi")]
        public float Bmi { get; set; }

        [JsonPropertyName("HbA1c_level")]
        public float HbA1cLevel { get; set; }

        [JsonPropertyName("blood_glucose_level")]
        public float BloodGlucoseLevel { get; set; }

        public float Weight { get; set; }
        public float Height { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Predict { get; set; }
    }
}
