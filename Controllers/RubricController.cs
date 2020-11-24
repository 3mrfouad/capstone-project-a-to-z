using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class RubricController :ControllerBase
    {
        /// <summary>
        ///     GetRubricsByHomeworkId
        ///     This action takes in Homework Id and returns List of Rubrics associated with that Homework Id
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>List of Rubrics associated with specified Homework Id</returns>
        public static List<Rubric> GetRubricsByHomeworkId(string homeworkId)
        {
            var parsedHomeworkId = int.Parse(homeworkId);
            var rubrics = new List<Rubric>();
            using var context = new AppDbContext();
            rubrics=context.Rubrics.Where(key => key.HomeworkId==parsedHomeworkId).ToList();
            return rubrics;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="rubrics"></param>
        public static void CreateRubricsByHomeworkId(string homeworkId,List<Tuple<string,string,string>> rubrics)
        {
            using var context = new AppDbContext();
            foreach ( var (isChallenge, criteria, weight) in rubrics )
            {
                int parsedHomeworkId = int.Parse(homeworkId);
                bool parsedIsChallenge = bool.Parse(isChallenge);
                int parsedWeight = int.Parse(weight);
                context.Rubrics.Add(new Rubric
                {
                    HomeworkId=parsedHomeworkId,
                    IsChallenge=parsedIsChallenge,
                    Criteria=criteria,
                    Weight=parsedWeight
                });
            }

            context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rubrics"></param>
        public static void UpdateRubricsById(Dictionary<string,Tuple<string,string,string>> rubrics)
        {
            using var context = new AppDbContext();
            foreach ( var (rubricId, (isChallenge, criteria, weight)) in rubrics )
            {
                int parsedRubricId = int.Parse(rubricId);
                bool parsedIsChallenge = bool.Parse(isChallenge);
                int parsedWeight = int.Parse(weight);
                var rubric = context.Rubrics.Find(parsedRubricId);
                rubric.IsChallenge=parsedIsChallenge;
                rubric.Criteria=criteria;
                rubric.Weight=parsedWeight;
            }

            context.SaveChanges();
        }
    }
}