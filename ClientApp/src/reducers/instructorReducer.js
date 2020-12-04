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

export const cohortArchiveReducer = (state = {}, action) => {
  switch (action.type) {
    case "COHORT_ARCHIVE_REQUEST":
      return { loading: true };
    case "COHORT_ARCHIVE_SUCCESS":
      return { loading: false, success: true };
    case "COHORT_ARCHIVE_FAIL":
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

export const courseAssignReducer = (state = {}, action) => {
  switch (action.type) {
    case "COURSE_ASSIGN_REQUEST":
      return { loading: true };
    case "COURSE_ASSIGN_SUCCESS":
      return { loading: false, success: true };
    case "COURSE_ASSIGN_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};

export const getAssignedCourseReducer = (state = { course: {} }, action) => {
  switch (action.type) {
    case "GET_ASSIGNED_COURSE_REQUEST":
      return { loading: true, course: {} };
    case "GET_ASSIGNED_COURSE_SUCCESS":
      return { loading: false, success: true, course: action.payload };
    case "GET_ASSIGNED_COURSE_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};

export const editAssignedCourseReducer = (state = {}, action) => {
  switch (action.type) {
    case "EDIT_ASSIGNED_COURSE_REQUEST":
      return { loading: true };
    case "EDIT_ASSIGNED_COURSE_SUCCESS":
      return { loading: false, success: true, course: action.payload };

    case "EDIT_ASSIGNED_COURSE_FAIL":
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

export const getAllCoursesReducer = (state = { courses: [] }, action) => {
  switch (action.type) {
    case "GET_ALL_COURSES_REQUEST":
      return { loading: true, courses: [] };
    case "GET_ALL_COURSES_SUCCESS":
      return { loading: false, courses: action.payload };

    case "GET_ALL_COURSES_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const getAllInstructorsReducer = (
  state = { instructors: [] },
  action
) => {
  switch (action.type) {
    case "GET_ALL_INSTRUCTORS_REQUEST":
      return { loading: true, instructors: [] };
    case "GET_ALL_INSTRUCTORS_SUCCESS":
      return { loading: false, instructors: action.payload };

    case "GET_ALL_INSTRUCTORS_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const getCoursesByCohortIdReducer = (
  state = { courses: [] },
  action
) => {
  switch (action.type) {
    case "COURSES_GET_BY_COHORT_ID_REQUEST":
      return { loading: true, courses: [] };
    case "COURSES_GET_BY_COHORT_ID_SUCCESS":
      return { loading: false, courses: action.payload };

    case "COURSES_GET_BY_COHORT_ID_FAIL":
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

export const courseArchiveReducer = (state = {}, action) => {
  switch (action.type) {
    case "COURSE_ARCHIVE_REQUEST":
      return { loading: true };
    case "COURSE_ARCHIVE_SUCCESS":
      return { loading: false, success: true };
    case "COURSE_ARCHIVE_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};

export const assignedCourseArchiveReducer = (state = {}, action) => {
  switch (action.type) {
    case "ASSIGNED_COURSE_ARCHIVE_REQUEST":
      return { loading: true };
    case "ASSIGNED_COURSE_ARCHIVE_SUCCESS":
      return { loading: false, success: true };
    case "ASSIGNED_COURSE_ARCHIVE_FAIL":
      return { loading: false, error: action.payload };
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

export const homeworkDetailInstructorReducer = (
  state = { homework: {} },
  action
) => {
  switch (action.type) {
    case "GET_HOMEWORK_DETAIL_INSTRUCTOR_REQUEST":
      return { loading: true };
    case "GET_HOMEWORK_DETAIL_INSTRUCTOR_SUCCESS":
      return { loading: false, homework: action.payload };

    case "GET_HOMEWORK_DETAIL_INSTRUCTOR_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const gradeSummaryInstructorReducer = (state = {}, action) => {
  switch (action.type) {
    case "GET_GRADE_SUMMARY_INSTRUCTOR_REQUEST":
      return { loading: true };
    case "GET_GRADE_SUMMARY_INSTRUCTOR_SUCCESS":
      return { loading: false, grade: action.payload };

    case "GET_GRADE_SUMMARY_INSTRUCTOR_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};
