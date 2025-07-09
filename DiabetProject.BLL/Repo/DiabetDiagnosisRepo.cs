using DiabetProject.BLL.Data;
using DiabetProject.BLL.Data.Dto;
using DiabetProject.BLL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiabetProject.BLL.Repo
{
    public class DiabetDiagnosisRepo
    {
        private readonly DiabetsDataContext _diabetsDataContext;

        public DiabetDiagnosisRepo(DiabetsDataContext diabetsDataContext)
        {
            _diabetsDataContext = diabetsDataContext;
        }

        public async Task<DiagnosisDto> Add(DiagnosisDto diagnosisDto)
        {  
            Diagnosis diagnosis = new()
            {
                Age = diagnosisDto.Age,
                BloodGlucoseLevel = diagnosisDto.BloodGlucoseLevel,
                Bmi = diagnosisDto.Bmi,
                Gender = diagnosisDto.Gender,
                HbA1cLevel = diagnosisDto.HbA1cLevel,
                HeartDisease = diagnosisDto.HeartDisease,
                Height = diagnosisDto.Height,
                Hypertension = diagnosisDto.Hypertension,
                SmokingHistory = diagnosisDto.SmokingHistory,
                Weight = diagnosisDto.Weight,
                UserId=diagnosisDto.UserId,
                Id=diagnosisDto.Id,
                CreatedAt=DateTime.UtcNow,
                Predict=diagnosisDto.Predict,
            };
            
             _diabetsDataContext.Diagnoses.Add(diagnosis);
            try
            {
                await _diabetsDataContext.SaveChangesAsync();
            }
            
            catch (Exception e)
{
                Console.WriteLine(e.GetType()); // what is the real exception?
            }


            return diagnosisDto;
        }

        public async  Task<List<DiagnosisDto>> GetAll(Guid userId)
        {
            var result = await _diabetsDataContext.Diagnoses.Where(x => x.UserId == userId).OrderBy(x => x.CreatedAt)
                .Select(x => new DiagnosisDto
                {
                    UserId = x.UserId,
                    Age = x.Age,  
                    Bmi=x.Bmi,
                    Gender=x.Gender,
                    HbA1cLevel=x.HbA1cLevel,
                    HeartDisease = x.HeartDisease,
                    Height=x.Height,
                    Hypertension = x.Hypertension,
                    Id=x.Id,
                    SmokingHistory = x.SmokingHistory,
                    Weight = x.Weight,
                    CreatedAt=x.CreatedAt,
                    BloodGlucoseLevel=x.BloodGlucoseLevel,
                    Predict=x.Predict,
                }).ToListAsync();
           
            return result;
        }

    }
}
