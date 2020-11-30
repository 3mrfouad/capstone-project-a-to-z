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

export const homeworkSummaryStudentReducer = (
  state = { homework: [] },
  action
) => {
  switch (action.type) {
    case "HOMEWORK_SUMMARY_STUDENT_REQUEST":
      return { loading: true, homework: [] };
    case "HOMEWORK_SUMMARY_STUDENT_SUCCESS":
      return { loading: false, homework: action.payload };

    case "HOMEWORK_SUMMARY_STUDENT_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const homeworkStudentReducer = (state = { homework: [] }, action) => {
  switch (action.type) {
    case "HOMEWORK_STUDENT_REQUEST":
      return { loading: true, homework: [] };
    case "HOMEWORK_STUDENT_SUCCESS":
      return { loading: false, homework: action.payload };

    case "HOMEWORK_STUDENT_FAIL":
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const createTimeSheetStudentReducer = (state = {}, action) => {
  switch (action.type) {
    case "CREATE_TIME_SHEET_STUDENT_REQUEST":
      return { loading: true };
    case "CREATE_TIME_SHEET_STUDENT_SUCCESS":
      return { loading: false, success: true, timeSheet: action.payload };

    case "CREATE_TIME_SHEET_STUDENT_FAIL":
      return { loading: false, error: action.payload };
    case "CREATE_TIME_SHEET_STUDENT_RESET":
      return {};

    default:
      return state;
  }
};

export const updateTimeSheetStudentReducer = (state = {}, action) => {
  switch (action.type) {
    case "UPDATE_TIME_SHEET_STUDENT_REQUEST":
      return { loading: true };
    case "UPDATE_TIME_SHEET_STUDENT_SUCCESS":
      return { loading: false, success: true, timeSheet: action.payload };

    case "UPDATE_TIME_SHEET_STUDENT_FAIL":
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};
