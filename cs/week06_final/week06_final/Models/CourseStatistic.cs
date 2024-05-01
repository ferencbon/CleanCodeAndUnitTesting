using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Models
{
    public class CourseStatistic
    {
        public string CourseName { get; private set; }
        public int TotalLectures { get; private set; }
        public int LecturesCompleted { get; private set; }
        public double Progress { get; private set; }
        public DateTime LastAccessed { get; private set; }

        public CourseStatistic(string courseName, int totalLectures, int lecturesCompleted, double progress, DateTime lastAccessed)
        {
            CourseName = courseName;
            TotalLectures = totalLectures;
            LecturesCompleted = lecturesCompleted;
            Progress = progress;
            LastAccessed = lastAccessed;
        }
    }
}
