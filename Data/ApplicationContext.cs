using StudentApi.Models;
namespace StudentApi.Data
{
    public static class ApplicationContext
    {
        public static List<Student> Students { get; set; }

        static ApplicationContext()
        {
            Students = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    FirstName = "Ahmet",
                    LastName = "Altuğ",
                    Age = 20,
                    Gender = "Erkek",
                    Grade = 3.5
                },
                new Student()
                {
                    Id = 2,
                    FirstName = "Ayşe",
                    LastName = "Aslan",
                    Age = 22,
                    Gender = "Kadın",
                    Grade = 3.8
                },
                new Student()
                {
                    Id = 3,
                    FirstName = "Mehmet",
                    LastName = "Kızılkaya",
                    Age = 21,
                    Gender = "Erkek",
                    Grade = 3.7
                },
            };
        }
    }
}
