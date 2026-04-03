namespace Web_API_Tutorial.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
