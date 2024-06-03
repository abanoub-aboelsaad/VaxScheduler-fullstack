using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Vaccine
{
    public class UpdateVaccineRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string Precautions { get; set; } = string.Empty;

        public int TimeGapFirstSecondDoseInDays { get; set; }
    }
}
