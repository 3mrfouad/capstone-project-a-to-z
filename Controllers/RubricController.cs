using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZLearn.Data;
using AZLearn.Models;

namespace AZLearn.Controllers
{
    public class RubricController : ControllerBase
    {
        public static void CreateRubricsByHomeworkId(string homeworkId, List<Tuple<string, string, string>> rubrics)
        {
            using var context = new AppDbContext();
            foreach (var (isChallenge, criteria, weight) in rubrics)
            {
                context.Rubrics.Add(new Rubric()
                {
                    HomeworkId = int.Parse(homeworkId),
                    IsChallenge = bool.Parse(isChallenge),
                    Criteria = criteria,
                    Weight = int.Parse(weight)
                });
            }
            context.SaveChanges();
        }


        public static void UpdateRubricsById(Dictionary<string, Tuple<string, string, string>> rubrics)
        {
            using var context = new AppDbContext();
            foreach (var (rubricId, (isChallenge, criteria, weight)) in rubrics)
            {
                var rubric = context.Rubrics.Find(int.Parse(rubricId));
                rubric.IsChallenge = bool.Parse(isChallenge);
                rubric.Criteria = criteria;
                rubric.Weight = int.Parse(weight);
            }
            context.SaveChanges();


        }

    }
}
