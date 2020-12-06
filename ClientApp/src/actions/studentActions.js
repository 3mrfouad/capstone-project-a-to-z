import axios from "axios";

export const courseSummaryStudent = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COURSE_SUMMARY_STUDENT_REQUEST" });
      // update the url later
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
      // update the url later
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
      // update the url later
      const {
        data,
      } = await axios.get("https://localhost:5001/application/gethomework", {
        params: { homeworkId: id },
      });
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

export const createTimeSheetStudent = (solvingHrs, studyHrs) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "CREATE_TIME_SHEET_STUDENT_REQUEST",
      });
      //   const {
      //     userLogin: { userInfo },
      //   } = getState();
      //   const config = {
      //     headers: {
      //       Authorization: `Bearer ${userInfo.token}`,
      //     },
      //   };
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

export const updateTimeSheetStudent = (solvingHrs, studyHrs) => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "UPDATE_TIME_SHEET_STUDENT_REQUEST",
      });
      //   const {
      //     userLogin: { userInfo },
      //   } = getState();
      //   const config = {
      //     headers: {
      //       Authorization: `Bearer ${userInfo.token}`,
      //     },
      //   };
      const { data } = await axios.patch(
        "https://localhost:5001/application/updatetimesheet",
        {
          params: {
            timesheetId: "1",
            solvingTime: solvingHrs,
            studyTime: studyHrs,
          },
        }
      );

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
