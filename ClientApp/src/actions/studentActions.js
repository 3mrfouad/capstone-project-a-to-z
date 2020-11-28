import axios from "axios";

export const courseSummaryStudent = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COURSE_SUMMARY_STUDENT_REQUEST" });
      // update the url later
      const { data } = await axios.get(
        "https://localhost:5001/application/getcourses"
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

export const homeworkSummaryStudent = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORK_SUMMARY_STUDENT_REQUEST" });
      // update the url later
      const { data } = await axios.get(
        "https://localhost:5001/application/gethomework"
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

export const homeworkStudent = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORK_STUDENT_REQUEST" });
      // update the url later
      const { data } = await axios.get(
        "https://localhost:5001/application/gethomework"
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

export const createTimeSheetStudent = (timeSheet) => {
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
      const { data } = await axios.post(`/api/products`, timeSheet);

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
