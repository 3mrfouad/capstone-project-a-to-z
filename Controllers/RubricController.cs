using System;
using System.Collections.Generic;
using System.Linq;
using AZLearn.Data;
using AZLearn.Models;
using AZLearn.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AZLearn.Controllers
{
    public class RubricController : ControllerBase
    {
        /// <summary>
        ///     GetRubricsByHomeworkId
        ///     This action takes in Homework Id and returns List of Rubrics associated with that Homework Id
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>List of Rubrics associated with specified Homework Id</returns>
        public static List<Rubric> GetRubricsByHomeworkId(string homeworkId)
        {
            int parsedHomeworkId = 0;

            #region Validation

            using var context = new AppDbContext();
            ValidationException exception = new ValidationException();
            homeworkId = (string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)) ? null : homeworkId.Trim();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId), nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                }
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
                }
            }
            if (exception.ValidationExceptions.Count > 0)
            {
                throw exception;
            }

            #endregion

            var rubrics = new List<Rubric>();
            rubrics = context.Rubrics.Where(key => key.HomeworkId == parsedHomeworkId).ToList();
            return rubrics;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="homeworkId"></param>
        /// <param name="rubrics"></param>
        public static void CreateRubricsByHomeworkId(string homeworkId, List<Tuple<string, string, string>> rubrics)
        {
            int parsedHomeworkId = 0;

            #region Validation for HomeworkId

            using var context = new AppDbContext();
            ValidationException exception = new ValidationException();
            homeworkId = (string.IsNullOrEmpty(homeworkId) || string.IsNullOrWhiteSpace(homeworkId)) ? null : homeworkId.Trim();

            if (string.IsNullOrWhiteSpace(homeworkId))
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(homeworkId), nameof(homeworkId) + " is null."));
            }
            else
            {
                if (!int.TryParse(homeworkId, out parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Invalid value for Homework Id"));
                }
                if (parsedHomeworkId > 2147483647 || parsedHomeworkId < 1)
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id value should be between 1 & 2147483647 inclusive"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework Id does not exist"));
                }
                else if (!context.Homeworks.Any(key => key.HomeworkId == parsedHomeworkId && key.Archive == false))
                {
                    exception.ValidationExceptions.Add(new Exception("Homework is archived"));
                }
            }

            if ( rubrics.Count==0 )
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubrics),
                    nameof(rubrics)+" is null."));
            }
            #endregion

            foreach ( var (tempIsChallenge, tempCriteria, tempWeight) in rubrics )
            {
                var isChallenge = string.IsNullOrEmpty(tempIsChallenge) || string.IsNullOrWhiteSpace(tempIsChallenge)
                    ? null
                    : tempIsChallenge.Trim();
                var criteria = string.IsNullOrEmpty(tempCriteria) || string.IsNullOrWhiteSpace(tempCriteria)
                    ? null
                    : tempCriteria.Trim().ToLower();
                var weight = string.IsNullOrEmpty(tempWeight) || string.IsNullOrWhiteSpace(tempWeight)
                    ? null
                    : tempWeight.Trim();

                bool parsedIsChallenge = false;
                int parsedWeight = 0;

                #region Validation for Rubrics

               
                if (!string.IsNullOrWhiteSpace(isChallenge))
                {
                    if (!bool.TryParse(isChallenge.Trim(), out parsedIsChallenge))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for isChallenge"));
                    }
                }
                if (string.IsNullOrWhiteSpace(criteria))
                {
                    exception.ValidationExceptions.Add(new Exception("Criteria is null"));
                }
                else
                {
                    if (criteria.Length > 250)
                    {
                        exception.ValidationExceptions.Add(new Exception("Criteria should be max 250 characters long."));
                    }
                    if ((!string.IsNullOrWhiteSpace(homeworkId)) && int.TryParse(homeworkId, out parsedHomeworkId))
                    {
                        if (context.Rubrics.Any(key => key.Criteria.ToLower() == criteria.Trim().ToLower() && key.HomeworkId == parsedHomeworkId && key.Archive == false))
                        {
                            exception.ValidationExceptions.Add(new Exception("Rubric already exists for this homework."));
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(weight))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(weight), nameof(weight) + " is null."));
                }
                else
                {
                    if (!int.TryParse(weight.Trim(), out parsedWeight))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for weight"));
                    }
                    else if (parsedWeight > 999 || parsedWeight < 0)
                    {
                        exception.ValidationExceptions.Add(new Exception("Weight should be between 0 and 999 inclusive."));
                    }
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                #endregion
                context.Rubrics.Add(new Rubric
                {
                    HomeworkId=parsedHomeworkId,
                    IsChallenge=parsedIsChallenge,
                    Criteria= char.ToUpper(criteria[0]) + criteria.Substring(1),
                    Weight=parsedWeight
                });
            }

            if ( exception.ValidationExceptions.Count>0 )
            {
                throw exception;
            }
            context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rubrics"></param>
        public static void UpdateRubricsById(Dictionary<string, Tuple<string, string, string>> rubrics)
        {
            using var context = new AppDbContext();
            ValidationException exception = new ValidationException();
            if (rubrics.Count == 0)
            {
                exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubrics),
                    nameof(rubrics)+" is null."));
            }

            foreach ( var (tempRubricId, (tempIsChallenge, tempCriteria, tempWeight)) in rubrics )
            {
                var rubricId = string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)
                    ? null
                    : tempRubricId.Trim();
                var isChallenge = string.IsNullOrEmpty(tempIsChallenge) || string.IsNullOrWhiteSpace(tempIsChallenge)
                    ? null
                    : tempIsChallenge.Trim().ToLower();
                var criteria = string.IsNullOrEmpty(tempCriteria) || string.IsNullOrWhiteSpace(tempCriteria)
                    ? null
                    : tempCriteria.Trim().ToLower();
                var weight = string.IsNullOrEmpty(tempWeight) || string.IsNullOrWhiteSpace(tempWeight)
                    ? null
                    : tempWeight.Trim();

                int parsedRubricId = 0;
                bool parsedIsChallenge = false;
                int parsedWeight = 0;

                #region Validation


                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId), nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Rubric Id"));
                    }
                    if (parsedRubricId > 2147483647 || parsedRubricId < 1)
                    {
                        exception.ValidationExceptions.Add(new Exception("Rubric Id value should be between 1 & 2147483647 inclusive"));
                    }
                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Rubric Id does not exist"));
                    }
                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                    {
                        exception.ValidationExceptions.Add(new Exception("Rubric is archived."));
                    }
                }
                if (!string.IsNullOrWhiteSpace(isChallenge))
                {
                    if (!bool.TryParse(isChallenge, out parsedIsChallenge))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for isChallenge"));
                    }
                }

                if (string.IsNullOrWhiteSpace(criteria))
                {
                    exception.ValidationExceptions.Add(new Exception("Criteria is null"));
                }
                else
                {
                    if (criteria.Length > 250)
                    {
                        exception.ValidationExceptions.Add(new Exception("Criteria should be max 250 characters long."));
                    }
                    if ((!string.IsNullOrWhiteSpace(rubricId)) && int.TryParse(rubricId, out parsedRubricId) && parsedRubricId >0 && context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                    {
                        int matchingHomeworkId = context.Rubrics.SingleOrDefault(key => key.RubricId == parsedRubricId)
                            .HomeworkId;
                        if (context.Rubrics.Any(key => key.Criteria.ToLower() == criteria.ToLower()))
                        {
                            if (!context.Rubrics.Any(key =>
                                key.Criteria.ToLower() == criteria.ToLower() && key.RubricId == parsedRubricId))
                            {
                                if (context.Rubrics.Any(key =>
                                    key.Criteria.ToLower() == criteria.ToLower() &&
                                    key.Homework.HomeworkId == matchingHomeworkId))
                                {
                                    exception.ValidationExceptions.Add(
                                        new Exception("Rubric with this criteria already exists for this homework."));
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(weight))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(weight), nameof(weight) + " is null."));
                }
                else
                {
                    if (!int.TryParse(weight, out parsedWeight))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for weight"));
                    }
                    else if (parsedWeight > 999 || parsedWeight < 0)
                    {
                        exception.ValidationExceptions.Add(new Exception("Weight should be between 0 and 999 inclusive."));
                    }
                }
                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                #endregion

                var rubric = context.Rubrics.Find(parsedRubricId);

                rubric.IsChallenge=parsedIsChallenge;
                rubric.Criteria = char.ToUpper(criteria[0]) + criteria.Substring(1);
                rubric.Weight=parsedWeight;
                }

            if ( exception.ValidationExceptions.Count>0 )
            {
                throw exception;
            }
            context.SaveChanges();
            }
        public static void ArchiveRubricsById(List<string> rubrics)
        {
            var exception = new ValidationException();
            using var context = new AppDbContext();
            var parsedRubricId = 0;

            foreach (var tempRubricId in rubrics)
            {

                var rubricId = (string.IsNullOrEmpty(tempRubricId) || string.IsNullOrWhiteSpace(tempRubricId)) ? null : tempRubricId.Trim();

                #region Validation

                if (string.IsNullOrWhiteSpace(rubricId))
                {
                    exception.ValidationExceptions.Add(new ArgumentNullException(nameof(rubricId), nameof(rubricId) + " is null."));
                }
                else
                {
                    if (!int.TryParse(rubricId, out parsedRubricId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Invalid value for Rubric Id"));
                    }
                    if (parsedRubricId > 2147483647 || parsedRubricId < 1)
                        exception.ValidationExceptions.Add(new Exception("Rubric Id value should be between 1 & 2147483647 inclusive"));
                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId))
                    {
                        exception.ValidationExceptions.Add(new Exception("Rubric Id does not exist"));
                    }
                    else if (!context.Rubrics.Any(key => key.RubricId == parsedRubricId && key.Archive == false))
                    {
                        exception.ValidationExceptions.Add(new Exception("Rubric Id is already archived"));
                    }

                }

                if (exception.ValidationExceptions.Count > 0)
                {
                    throw exception;
                }
                #endregion

                var rubric = context.Rubrics.Find(parsedRubricId);
                rubric.Archive = true;
            }

            
        }
    }
}