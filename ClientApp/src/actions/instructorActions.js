import axios from "axios";

export const cohortSummaryInstructor = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COHORT_SUMMARY_INSTRUCTOR_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/getcohorts"
      );
      dispatch({
        type: "COHORT_SUMMARY_INSTRUCTOR_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_SUMMARY_INSTRUCTOR_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const homeworkSummaryInstructor = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "HOMEWORKSUMMARY_INSTRUCTOR_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/gethomework"
      );
      dispatch({
        type: "HOMEWORKSUMMARY_INSTRUCTOR_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "HOMEWORKSUMMARY_INSTRUCTOR_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const createCohort = () => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COHORT_CREATE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const { data } = await axios.post(
        `https://localhost:5001/application/createcohort`
      );

      dispatch({
        type: "COHORT_CREATE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_CREATE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const editCohort = () => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COHORT_EDIT_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const { data } = await axios.patch(
        `https://localhost:5001/application/updatecohort`
      );

      dispatch({
        type: "COHORT_EDIT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_EDIT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const createCourse = () => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COURSE_CREATE_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const { data } = await axios.post(
        `https://localhost:5001/application/createcourse`
      );

      dispatch({
        type: "COURSE_CREATE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_CREATE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};

export const editCourse = () => {
  return async (dispatch, getState) => {
    try {
      dispatch({
        type: "COURSE_EDIT_REQUEST",
      });
      // const {
      //   userLogin: { userInfo },
      // } = getState();
      // const config = {
      //   headers: {
      //     Authorization: `Bearer ${userInfo.token}`,
      //   },
      // };
      const { data } = await axios.patch(
        `https://localhost:5001/application/updatecourse`
      );

      dispatch({
        type: "COURSE_EDIT_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COURSE_EDIT_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};
