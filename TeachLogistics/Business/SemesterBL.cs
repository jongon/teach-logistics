using System;
using System.Collections.Generic;
using TeachLogistics.Models;

namespace TeachLogistics.Business
{
    public class SemesterBL
    {
        public static Semester CreateSemesterSection()
        {
            Semester semester = new Semester
            {
                Id = Guid.NewGuid(),
                Description = "Semestre Prueba"
            };
            List<Section> sections = new List<Section> {
                new Section { Id = Guid.NewGuid(), Number = "1001", SemesterId = semester.Id },
                new Section { Id = Guid.NewGuid(), Number = "1002",  SemesterId = semester.Id }, 
            };
            semester.Sections = sections;
            return semester;
        }
    }
}