export const cohortSummaryInstructorReducer = (
  state = { cohorts: [] },
  action
) => {
  switch (action.type) {
    case "COHORT_SUMMARY_INSTRUCTOR_REQUEST":
      return { loading: true, cohorts: [] };
    case "COHORT_SUMMARY_INSTRUCTOR_SUCCESS":
      return { loading: false, cohorts: action.payload };

    case "COHORT_SUMMARY_INSTRUCTOR_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};
