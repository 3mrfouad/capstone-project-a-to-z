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

export const cohortGetStateReducer = (state = {}, action) => {
  switch (action.type) {
    case "COHORT_GET_REQUEST":
      return { loading: true };
    case "COHORT_GET_SUCCESS":
      return { loading: false, cohort: action.payload };

    case "COHORT_GET_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const homeworkSummaryInstructorReducer = (
  state = { homeworkSummary: [] },
  action
) => {
  switch (action.type) {
    case "HOMEWORKSUMMARY_INSTRUCTOR_REQUEST":
      return { loading: true, homeworkSummary: [] };
    case "HOMEWORKSUMMARY_INSTRUCTOR_SUCCESS":
      return { loading: false, homeworkSummary: action.payload };

    case "HOMEWORKSUMMARY_INSTRUCTOR_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const cohortCreateReducer = (state = {}, action) => {
  switch (action.type) {
    case "COHORT_CREATE_REQUEST":
      return { loading: true };
    case "COHORT_CREATE_SUCCESS":
      return { loading: false, success: true, cohort: action.payload };
    case "COHORT_CREATE_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};

export const cohortEditReducer = (state = {}, action) => {
  switch (action.type) {
    case "COHORT_EDIT_REQUEST":
      return { loading: true };
    case "COHORT_EDIT_SUCCESS":
      return { loading: false, success: true, cohort: action.payload };

    case "COHORT_EDIT_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};

export const courseCreateReducer = (state = {}, action) => {
  switch (action.type) {
    case "COURSE_CREATE_REQUEST":
      return { loading: true };
    case "COURSE_CREATE_SUCCESS":
      return { loading: false, success: true, course: action.payload };
    case "COURSE_CREATE_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};

export const getCourseReducer = (state = {}, action) => {
  switch (action.type) {
    case "COURSE_GET_REQUEST":
      return { loading: true };
    case "COURSE_GET_SUCCESS":
      return { loading: false, course: action.payload };

    case "COURSE_GET_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const courseEditReducer = (state = {}, action) => {
  switch (action.type) {
    case "COURSE_EDIT_REQUEST":
      return { loading: true };
    case "COURSE_EDIT_SUCCESS":
      return { loading: false, success: true, course: action.payload };
    case "COURSE_EDIT_FAIL":
      return { loading: false, error: action.payload };
    case "COURSE_EDIT_RESET":
      return {};
    default:
      return state;
  }
};

export const manageCourseReducer = (state = { courses: [] }, action) => {
  switch (action.type) {
    case "MANAGE_COURSE_REQUEST":
      return { loading: true, courses: [] };
    case "MANAGE_COURSE_SUCCESS":
      return { loading: false, courses: action.payload };

    case "MANAGE_COURSE_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};
