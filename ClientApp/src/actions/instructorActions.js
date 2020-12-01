import axios from "axios";
import querystring from "querystring";

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

export const cohortGet = (id) => {
  return async (dispatch) => {
    try {
      dispatch({ type: "COHORT_GET_REQUEST" });
      const { data } = await axios.get(
        "https://localhost:5001/application/cohort",
        {
          params: { cohortId: id },
        }
      );
      dispatch({
        type: "COHORT_GET_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "COHORT_GET_FAIL",
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

export const createCohort = (cohort) => {
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
      //     "Content-Type": "application/json",
      //   },
      // };
      console.log(cohort);
      const params = {
        name: cohort.name,
        capacity: cohort.capacity,
        city: cohort.city,
        modeOfTeaching: cohort.modeOfTeaching,
        startDate: cohort.startDate,
        endDate: cohort.endDate,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/createcohort?" +
          querystring.stringify(params),
        method: "post",
        data: params,
      });

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

export const editCohort = (cohort) => {
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
      const params = {
        cohortId: cohort.cohortId,
        name: cohort.name,
        capacity: cohort.capacity,
        city: cohort.city,
        modeOfTeaching: cohort.modeOfTeaching,
        startDate: cohort.startDate,
        endDate: cohort.endDate,
      };
      const { data } = await axios.request({
        url:
          "https://localhost:5001/application/updatecohort?" +
          querystring.stringify(params),
        method: "patch",
        data: params,
      });

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

export const manageCourseInstructor = () => {
  return async (dispatch) => {
    try {
      dispatch({ type: "MANAGE_COURSE_REQUEST" });
      // update the url later
      const { data } = await axios.get(
        "https://localhost:5001/application/getcourses"
      );
      dispatch({
        type: "MANAGE_COURSE_SUCCESS",
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: "MANAGE_COURSE_FAIL",
        payload:
          error.response && error.response.data.message
            ? error.response.data.message
            : error.response,
      });
    }
  };
};
