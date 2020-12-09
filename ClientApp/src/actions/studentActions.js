import axios from "axios";
import querystring from "querystring";

export const courseSummaryStudent = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COURSE_SUMMARY_STUDENT_REQUEST" });
      const {
        data,
      } = await axios.get(
        "https://localhost:5001/application/GetCourseSummary",
        { params: { cohortId: "3" } }
      );
      dispatch({
        type: "COURSE_SUMMARY_STUDENT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_SUMMARY_STUDENT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const homeworkSummaryStudent = (id) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORK_SUMMARY_STUDENT_REQUEST" });
      const {
        data,
      } = await axios.get(
        "https://localhost:5001/application/homeworksummary",
        { params: { courseId: id, cohortId: "3" } }
      );
      dispatch({
        type: "HOMEWORK_SUMMARY_STUDENT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "HOMEWORK_SUMMARY_STUDENT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const homeworkStudent = (id) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORK_STUDENT_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/gethomework",
        {
          params: { homeworkId: id },
        }
      );

      dispatch({
        type: "HOMEWORK_STUDENT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "HOMEWORK_STUDENT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const getHomeworkTimesheetStudent = (ids) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORK_TIMESHEET_STUDNET_REQUEST" });
      const params = { homeworkId: ids.homeworkId, studentId: ids.studentId };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/homeworktimesheet?" +
          querystring.stringify(params),
        method: "get",
        data: params,
      });
      dispatch({
        type: "HOMEWORK_TIMESHEET_STUDNET_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "HOMEWORK_TIMESHEET_STUDNET_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const createTimeSheetStudent = (solvingHrs, studyHrs) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "CREATE_TIME_SHEET_STUDENT_REQUEST",
      });
      const { data } = await axios.post(
        "https://localhost:5001/application/createtimesheet",

        {
          headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
          },

          params: {
            homeworkId: "1",
            studentId: "4",
            solvingTime: solvingHrs,
            studyTime: studyHrs,
          },
        }
      );

      dispatch({
        type: "CREATE_TIME_SHEET_STUDENT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "CREATE_TIME_SHEET_STUDENT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const updateTimeSheetStudent = (timesheetId, solvingHrs, studyHrs) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "UPDATE_TIME_SHEET_STUDENT_REQUEST",
      });
      const params = {
        timesheetId: timesheetId,
        solvingTime: solvingHrs,
        studyTime: studyHrs,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/updatetimesheet?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

      dispatch({
        type: "UPDATE_TIME_SHEET_STUDENT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "UPDATE_TIME_SHEET_STUDENT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};
