import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getAllCourses,
  getAllInstructors,
  assignCourse,
  getCourse,
} from "../../../actions/instructorActions";

const CourseAssign = ({ match, history }) => {
  const cohortId = match.params.id;
  const dispatch = useDispatch();
  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [resourcesLink, setResourcesLink] = useState("");
  const { loading, courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);
  const { success, error } = useSelector((state) => state.courseAssign);
  const { course } = useSelector((state) => state.getCourse);
  //(1) Add validation states
  const [validated, setValidated] = useState(false);
  const [invalidDatesBL, setInvalidDatesBl] = useState(false);
  //----------------------------
  // ! (10.1) Anti-tamper validation - States and Variables
  //const [courseName, setCourseName] = useState(""); //Added by Ayesha for validation
  //const [instructor, setInstructor] = useState(""); //Added by Ayesha for validation
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let validStartDate = false;
  let validEndDate = false;
  let formSubmitIndicator = false;
  // ! ------------------------------------------------------

  useEffect(() => {
    dispatch(getAllCourses());
    dispatch(getAllInstructors());
  }, [dispatch]);

  // useEffect(() => {
  //   if (courseId) {
  //     dispatch(getCourse(courseId));
  //   }
  // }, [courseId]);

  // ! (10.2) Anti-tamper validation - Validate (parameters)
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
    console.log("FUNCTION");

    try {
      cohortId = cohortId.trim().toLowerCase();
      courseId = courseId.trim().toLowerCase();
      instructorId = instructorId.trim().toLowerCase();
      resourcesLink = resourcesLink.trim().toLowerCase();
      startDate = startDate.trim().toLowerCase();
      endDate = endDate.trim().toLowerCase();
      console.log("TRY");

      if (!cohortId) {
        validFormData = false;
        console.log("cohortid");
      } else if (!courseId) {
        validFormData = false;
        console.log("courseid");
      } else if (!instructorId) {
        validFormData = false;
        console.log("instructorid");
      } else if (resourcesLink > 250) {
        validFormData = false;
        console.log("resource length");
      }
      //@Link:https://stackoverflow.com/questions/1410311/regular-expression-for-url-validation-in-javascript
      else if (
        resourcesLink &&
        !/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(
          resourcesLink
        )
      ) {
        {
          validFormData = false;
          console.log("Resource pattern");
        }
      } else if (!startDate || !endDate) {
        validFormData = false;
        console.log("startDate/endDate");
      } else {
        try {
          parsedStartDate = Date.parse(startDate);
          validStartDate = true;
          console.log("startDate parse");
        } catch (ParseException) {
          validStartDate = false;
          console.log("startDate parse exception");
          validFormData = false;
        }
        try {
          parsedEndDate = Date.parse(startDate);
          validEndDate = true;
          console.log("endDate purse");
        } catch (ParseException) {
          validEndDate = false;
          console.log("endDate parse exception");
          validFormData = false;
        }
        /* Dates business logic */

        console.log(
          "parsed start date validation: ",
          validStartDate,
          "parsed end date validation: ",
          validEndDate
        );
        if (validStartDate && validEndDate) {
          console.log("Dates are both pursed ok");
          if (parsedEndDate < parsedStartDate) {
            validFormData = false;
            console.log("parsedEndDate < parsedStartDate");
          } else {
            validFormData = true;
            console.log("All good :", validFormData);
          }
        }
      }
    } catch (Exception) {
      validFormData = false;
      console.log("CATCH ");
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
      console.log("pass initial validation 100", validFormData);

      // ! (10.3) Anti-tamper validation - Alert message conditions
      validFormData = false;
      formSubmitIndicator = true;
      setValidData(validFormData);

      // ! ------------------------------------------------------
    } else {
      console.log("BL ELse");

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
        e.preventDefault();
        dispatch(
          assignCourse({
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
                    ? `Unsuccessful attempt to assign course. ${error.data}`
                    : !loading && !error && success
                    ? "Course was successfully assigned"
                    : ""
                  : "Error: Form was submitted with invalid data fields"
                : ""}
            </p>
            {/* ! ------------------------------------------------------  */}

            <Form noValidate validated={validated} onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Course Name</Form.Label>
                <Form.Control
                  as="Select"
                  required
                  value={courseId}
                  onChange={(e) => setCourseId(e.target.value)}
                >
                  <option value="">Select</option>
                  {courses.map((course, index) => (
                    <option value={course.courseId} key={index}>
                      {course.name}
                    </option>
                  ))}
                </Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please select a course
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="instructor">
                <Form.Label>Instructor</Form.Label>
                <Form.Control
                  as="select"
                  required
                  value={instructorId}
                  onChange={(e) => setInstructorId(e.target.value)}
                >
                  <option value="">Select</option>
                  {instructors.map((instructor, index) => (
                    <option value={instructor.userId} key={index}>
                      {instructor.name}
                    </option>
                  ))}
                </Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose an instructor
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="startdate">
                <Form.Label>Start Date</Form.Label>
                <Form.Control
                  required
                  type="date"
                  value={startDate}
                  onChange={(e) => setStartDate(String(e.target.value))}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose a start date.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="enddate">
                <Form.Label>End Date</Form.Label>
                <Form.Control
                  required
                  type="date"
                  min={startDate}
                  value={endDate}
                  onChange={(e) => setEndDate(String(e.target.value))}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please choose an end date.
                </Form.Control.Feedback>
                {/* (9) Add business logic validation message. */}
                <p className="text-danger small">
                  {invalidDatesBL ? "End date can't be before start date" : ""}
                </p>
                {/*---------------------------------------*/}
              </Form.Group>
              {/* <Form.Group controlId="hours">
                <Form.Label>Hours</Form.Label>
                <Form.Control disabled></Form.Control>
              </Form.Group>
              <Form.Group controlId="description">
                <Form.Label>Description</Form.Label>
                <Form.Control disabled></Form.Control>
              </Form.Group> */}
              <Form.Group controlId="link">
                <Form.Label>Resource Link</Form.Label>
                <Form.Control
                  maxLength="250"
                  type="url"
                  value={resourcesLink}
                  onChange={(e) => setResourcesLink(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please enter a valid URL.
                </Form.Control.Feedback>
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
    </React.Fragment>
  );
};

export default CourseAssign;
