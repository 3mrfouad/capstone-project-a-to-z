import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getAllInstructors,
  getAssignedCourse,
  editAssignedCourse,
  getCoursesByCohortId,
} from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const CourseEditAssigned = ({ match, history }) => {
  const cohortId = match.params.id;
  const courseId = match.params.courseId;
  const dispatch = useDispatch();
  const [instructorId, setInstructorId] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [resourcesLink, setResourcesLink] = useState("");
  const [validated, setValidated] = useState(false);
  const [invalidDatesBL, setInvalidDatesBl] = useState(false);
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let validStartDate = false;
  let validEndDate = false;
  let formSubmitIndicator = false;
  const [previousCourseId, setPreviousCourseId] = useState("");
  const [previousCohortId, setPreviousCohortId] = useState("");

  // ! ------------------------------------------------------
  const { instructors } = useSelector((state) => state.getAllInstructors);
  const { loading, course, success, error } = useSelector(
    (state) => state.getAssignedCourse
  );
  useEffect(() => {
    if (
      !success ||
      courseId != previousCourseId ||
      cohortId != previousCohortId
    ) {
      setPreviousCourseId(courseId);
      setPreviousCohortId(cohortId);
      dispatch(getAssignedCourse(courseId, cohortId));
    }

    dispatch(getAllInstructors());
  }, [dispatch, courseId, cohortId, success]);
  function Validate(
    cohortId,
    courseId,
    instructorId,
    resourcesLink,
    startDate,
    endDate
  ) {
    let parsedEndDate = 0;
    let parsedStartDate = 0;
    formSubmitIndicator = true;
    try {
      cohortId = cohortId.trim().toLowerCase();
      courseId = courseId.trim().toLowerCase();
      instructorId = instructorId.trim().toLowerCase();
      resourcesLink = resourcesLink.trim().toLowerCase();
      startDate = startDate.trim().toLowerCase();
      endDate = endDate.trim().toLowerCase();

      if (!cohortId) {
        validFormData = false;
      } else if (cohortId < 0 || cohortId > 2147483647) {
        validFormData = false;
      } else if (!courseId) {
        validFormData = false;
      } else if (courseId < 0 || courseId > 2147483647) {
        validFormData = false;
      } else if (!instructorId) {
        validFormData = false;
      } else if (instructorId < 0 || instructorId > 2147483647) {
        validFormData = false;
      } else if (resourcesLink > 250) {
        validFormData = false;
      }
      //@Link:https://stackoverflow.com/questions/1410311/regular-expression-for-url-validation-in-javascript
      else if (
        resourcesLink &&
        !/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(
          resourcesLink
        )
      ) {
        validFormData = false;
      } else if (!startDate || !endDate) {
        validFormData = false;
      } else {
        try {
          parsedStartDate = Date.parse(startDate);
          validStartDate = true;
        } catch (ParseException) {
          validStartDate = false;
          validFormData = false;
        }
        try {
          parsedEndDate = Date.parse(startDate);
          validEndDate = true;
        } catch (ParseException) {
          validEndDate = false;
          validFormData = false;
        }
        /* Dates business logic */

        if (validStartDate && validEndDate) {
          if (parsedEndDate < parsedStartDate) {
            validFormData = false;
          } else {
            validFormData = true;
          }
        }
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
  // ! ------------------------------------------------------
  const submitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    setValidated(true);
    //(3) Add business logic
    if (
      endDate === "" ||
      startDate === "" ||
      Date.parse(endDate) < Date.parse(startDate)
    ) {
      e.preventDefault();
      Date.parse(endDate) < Date.parse(startDate)
        ? setInvalidDatesBl(true)
        : setInvalidDatesBl(false);
      setEndDate("");
      // ! (10.3) Anti-tamper validation - Alert message conditions
      validFormData = false;
      formSubmitIndicator = true;
      setValidData(validFormData);
      // ! ------------------------------------------------------
    } else {
      e.preventDefault();
      setInvalidDatesBl(false);
      // ! (10.4) Anti-tamper validation - calling Validate
      Validate(
        cohortId,
        courseId,
        instructorId,
        resourcesLink,
        startDate,
        endDate
      );
      if (validFormData) {
        setValidData(validFormData);
        // ! ------------------------------------------------------
        dispatch(
          editAssignedCourse({
            cohortId,
            courseId,
            instructorId,
            startDate,
            endDate,
            resourcesLink,
          })
        );
      } else {
        // ! (10.5) Anti-tamper validation - Alert message conditions
        setValidData(validFormData);
      }
    }
    // ! (10.6) Anti-tamper validation - Alert message conditions
    setFormSubmitted(formSubmitIndicator);
    // ! ------------------------------------------------------
  };
  const goBack = () => {
    history.goBack();
  };
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h2>Course</h2>
              {/* ! (10.7) Anti-tamper validation - Alert message conditions   */}
              <p
                class={
                  formSubmitted
                    ? validData
                      ? !loading && error
                        ? "alert alert-danger"
                        : !loading && !error && success
                        ? "alert alert-success"
                        : ""
                      : "alert alert-danger"
                    : ""
                }
                role="alert"
              >
                {formSubmitted
                  ? validData
                    ? !loading && error
                      ? `Unsuccessful attempt to update course assignment. ${error.data}`
                      : !loading && !error && success
                      ? "Assigned Cohort was successfully updated"
                      : ""
                    : "Error: Form was submitted with invalid data fields"
                  : ""}
              </p>
              <Form noValidate validated={validated} onSubmit={submitHandler}>
                <Form.Group controlId="course name">
                  <Form.Label>Course Name</Form.Label>
                  <Form.Control value={course.item1} disabled></Form.Control>
                </Form.Group>
                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    as="select"
                    required
                    value={instructorId}
                    onChange={(e) => setInstructorId(e.target.value)}
                  >
                    {instructors
                      .filter((item) => item.name == course.item2)
                      .map((instructor, index) => (
                        <option value={instructor.userId} key={index}>
                          {instructor.name}
                        </option>
                      ))}
                    {instructors
                      .filter(
                        (item) =>
                          item.archive == false && item.name != course.item2
                      )
                      .map((instructor, index) => (
                        <option value={instructor.userId} key={index}>
                          {instructor.name}
                        </option>
                      ))}
                  </Form.Control>
                </Form.Group>
                <Form.Group controlId="startdate">
                  <Form.Label>Start Date</Form.Label>
                  <Form.Control
                    required
                    type="date"
                    value={startDate.split(" ")[0]}
                    onChange={(e) =>
                      setStartDate(String(e.target.value).split(" ")[0])
                    }
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="enddate">
                  <Form.Label>End Date</Form.Label>
                  <Form.Control
                    required
                    type="date"
                    min={startDate}
                    value={endDate.split(" ")[0]}
                    onChange={(e) =>
                      setEndDate(String(e.target.value).split(" ")[0])
                    }
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please choose an end date.
                  </Form.Control.Feedback>
                  {/* (9) Add business logic validation message. */}
                  <p className="text-danger small">
                    {invalidDatesBL
                      ? "End date can't be before start date"
                      : ""}
                  </p>
                </Form.Group>
                <Form.Group controlId="hours">
                  <Form.Label>Hours</Form.Label>
                  <Form.Control disabled value={course.item5}></Form.Control>
                </Form.Group>
                <Form.Group controlId="description">
                  <Form.Label>Description</Form.Label>
                  <Form.Control disabled value={course.item6}></Form.Control>
                </Form.Group>
                <Form.Group controlId="link">
                  <Form.Label>Resource Link</Form.Label>
                  <Form.Control
                    maxlength="250"
                    type="url"
                    value={course.item7}
                    disabled
                    onChange={(e) => setResourcesLink(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <button type="button" className="btn btn-link" onClick={goBack}>
                  Back
                </button>{" "}
                <Button type="submit" variant="primary" className="float-right">
                  {" "}
                  Save
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default CourseEditAssigned;
