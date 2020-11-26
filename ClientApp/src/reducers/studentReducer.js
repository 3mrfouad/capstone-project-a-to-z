export const courseSummaryStudentReducer = (
  state = { courses: [] },
  action
) => {
  switch (action.type) {
    case "COURSE_SUMMARY_STUDENT_REQUEST":
      return { loading: true, courses: [] };
    case "COURSE_SUMMARY_STUDENT_SUCCESS":
      return { loading: false, courses: action.payload };

    case "COURSE_SUMMARY_STUDENT_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};
