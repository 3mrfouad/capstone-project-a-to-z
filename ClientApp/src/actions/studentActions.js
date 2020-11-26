import axios from "axios";

export const courseSummaryStudent = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COURSE_SUMMARY_STUDENT_REQUEST" });
      // update the url later
      const { data } = await axios.get("/api/products");
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
