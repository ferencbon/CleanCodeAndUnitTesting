namespace week05_homework.School;

public interface ISchoolClass
{
    void AddStudent(Student student);
    int GetStudentCount();
}

public class SchoolClass : ISchoolClass
{
    private List<Student> students = new List<Student>();

    public SchoolClass() { }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public int GetStudentCount()
    {
        return students.Count;
    }
}