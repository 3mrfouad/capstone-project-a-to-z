using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.EntityFrameworkCore;

namespace AZLearn.Models.CodeSnippets.LINQ
{
    public class QueriesController : Controller
    {
        // Create new Cohort, new Courses and Populate both plus CohortCourse
        public static void CreateCohortCourse()
        {
            var cohort = new Cohort() { Name = "4.2", Capacity = 20, ModeOfTeaching = "Online", StartDate = new DateTime(2020, 11, 20), EndDate = new DateTime(2021, 03, 16), City = "Edmonton" };
            var course1 = new Course() { Name = "React 101", Description = "React Fundamentals", DurationHrs = 10, InstructorId = 2 };
            var course2 = new Course() { Name = "Redux 101", Description = "Redux Fundamentals", DurationHrs = 10, InstructorId = 2 };

            var cohortCourse1 = new CohortCourse()
            {
                Cohort = cohort,
                Course = course1,
                StartDate = new DateTime(2021, 03, 16),
                EndDate = new DateTime(2021, 03, 18)
            };
            var cohortCourse2 = new CohortCourse()
            {
                Cohort = cohort,
                Course = course2,
                StartDate = new DateTime(2021, 03, 16),
                EndDate = new DateTime(2021, 03, 18)
            };
            using var context = new AppDbContext();
            context.CohortCourses.Add(cohortCourse1); // will also add member1 and comment1
            context.CohortCourses.Add(cohortCourse2); // will also add comment2

            context.SaveChanges();

        }
        //Create Rubric, Existing User and populating the grades
        public static void CreateGradeNewUser()
        {
            var student = new User() { Name = "Student A", PasswordHash = "myPassword", Email = "studentA@host.com", IsInstructor = false };
            var rubric1 = new Rubric() { IsChallenge = false, Criteria = "Use CSS", Weight = 1, HomeworkId = 1 };
            var rubric2 = new Rubric() { IsChallenge = false, Criteria = "Use HTML", Weight = 2, HomeworkId = 1 };


            var grade1 = new Grade()
            {
                Mark = 0,
                Rubric = rubric1,
                Student = student
            };
            var grade2 = new Grade()
            {
                Mark = 1,
                Rubric = rubric2,
                Student = student
            };
            using var context = new AppDbContext();
            context.Grades.Add(grade1); // will also add member1 and comment1
            context.Grades.Add(grade2); // will also add comment2

            context.SaveChanges();

        }
        //Create Rubric, Existing User and populating the grades
        public static void CreateGradeExistingUser() {
            using var context = new AppDbContext();
            var studentBId = context.Users.SingleOrDefault(key => key.Name == "Student B").UserId;
            var rubric1 = new Rubric() { IsChallenge = false, Criteria = "Use CSS", Weight = 1, HomeworkId = 1 };
            var rubric2 = new Rubric() { IsChallenge = false, Criteria = "Use HTML", Weight = 2, HomeworkId = 1 };


            var grade1 = new Grade()
            {
                Mark = 0,
                Rubric = rubric1,
                StudentId = studentBId
            };
            var grade2 = new Grade()
            {
                Mark = 1,
                Rubric = rubric2,
                StudentId = studentBId
            };
           
            context.Grades.Add(grade1); // will also add member1 and comment1
            context.Grades.Add(grade2); // will also add comment2

            context.SaveChanges();
        }
        //Read Grades of a student with Id=4 from Grade Table
        /*public static void GetGradeByStudentId(string studentId)
        {
            var parsedStudentId = int.Parse(studentId);
            using var context = new AppDbContext();

            var grades = context.Grades.Where(key => key.StudentId == parsedStudentId).ToList();
            foreach (var grade in grades)
            {
                context.Rubrics.Where(key => key.HomeworkId == 1).ToList();
            }
        }*/
    }
}
