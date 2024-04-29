using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week04_homework
{
    public class GradeCalculator
    {
        private readonly List<(int Score,string Grade)> GRADEBOUNDARIES=new List<(int Score, string Grade)>
        {
            (90,"A"),
            (80,"B"),
            (70,"C"),
            (0,"D")
        };

        public string CalculateGrade(int score)
        {
            foreach (var gradeBoundary in GRADEBOUNDARIES)
            {
                if(score >= gradeBoundary.Score) 
                    return gradeBoundary.Grade;
            }
            return GRADEBOUNDARIES.Last().Grade;
        }
    }
}
