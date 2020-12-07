import React, { useState } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  updateTimeSheetStudent,
  homeworkStudent,
  createTimeSheetStudent,
} from "../../../actions/studentActions";
import {
  getAllCourses,
  getAllInstructors,
} from "../../../actions/instructorActions";
const HomeworkStudent = ({ match, history }) => {
  const studentId = match.params.studentId;
  const homeworkId = match.params.homeworkId;
  const [solvingHrs, setSolvingHrs] = useState("");
  const [studyHrs, setStudyHrs] = useState("");
  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const dispatch = useDispatch();

  //(1) Add validation states
  const [validated, setValidated] = useState(false);
  //----------------------------
  // ! (10.1) Anti-tamper validation - States and Variables
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  //let validStartDate = false;
  // let validEndDate = false;
  let formSubmitIndicator = false;
  // ! ------------------------------------------------------
  const { homework, loading } = useSelector((state) => state.homeworkStudent);
  const {
    loading: loadingCreate,
    success: successCreate,
    timeSheet,
    error,
  } = useSelector((state) => state.createTimeSheetStudent);
  const { courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);
  useEffect(() => {
    dispatch(homeworkStudent(homeworkId));
    dispatch(getAllCourses());
    dispatch(getAllInstructors());
  }, [dispatch]);
  console.log(homework);

  // ! (10.2) Anti-tamper validation - Validate (parameters)
  function Validate(solvingHrs, studyHrs) {
    formSubmitIndicator = true;
    try {
      solvingHrs = solvingHrs.trim().toLowerCase();
      studyHrs = studyHrs.trim().toLowerCase();

      if (!solvingHrs) {
        validFormData = false;
      } else if (
        parseFloat(solvingHrs) > 999.99 ||
        parseFloat(solvingHrs) < 0
      ) {
        validFormData = false;
        console.log("solvingHrs: ", parseFloat(solvingHrs));
      } else if (parseFloat(studyHrs) > 999.99 || parseFloat(studyHrs) < 0) {
        validFormData = false;
        console.log("studyHrs: ", parseFloat(studyHrs));
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
  // ! ------------------------------------------------------

  const summitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    } else {
      setValidated(true);
      e.preventDefault();
      // ! (10.4) Anti-tamper validation - calling Validate
      Validate(solvingHrs, studyHrs);
      if (validFormData) {
        setValidData(validFormData);
        // ! -------------------------------------------------
        dispatch(updateTimeSheetStudent(solvingHrs, studyHrs));
        console.log("create timesheet");
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
      {homework.length < 1 ? (
        <h2>Loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              {/* ! (10.7) Anti-tamper validation - Alert message conditions   */}
              <p
                class={
                  formSubmitted
                    ? validData
                      ? !loading && error
                        ? "alert alert-danger"
                        : !loading && !error && successCreate
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
                      ? "Unsuccessful attempt to update Timesheet"
                      : !loading && !error && successCreate
                      ? "Timesheet was successfully updated"
                      : ""
                    : "Error: Form was submitted with invalid data fields"
                  : ""}
              </p>
              {/* ! ------------------------------------------------------  */}
              <Form>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control disabled value={homework.Title}></Form.Control>
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control as="select" required value={courseId} disabled>
                    <option value="">{homework.CourseName}</option>
                    {courses.map((course, index) => (
                      <option value={course.courseId} key={index}>
                        {course.name}
                      </option>
                    ))}
                  </Form.Control>
                </Form.Group>
                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    disabled
                    as="select"
                    required
                    value={instructorId}
                  >
                    <option value="">{homework.InstructorName}</option>
                    {instructors.map((instructor, index) => (
                      <option value={instructor.userId} key={index}>
                        {instructor.name}
                      </option>
                    ))}
                  </Form.Control>
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.AvgCompletionTime}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.DueDate}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.ReleaseDate}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.DocumentLink}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    disabled
                    value={homework.GitHubClassRoomLink}
                  ></Form.Control>
                </Form.Group>
              </Form>
              <Form noValidate validated={validated} onSubmit={summitHandler}>
                <h3>Timesheet</h3>
                <Form.Group controlId="Solving/Troubleshooting">
                  <Form.Label>Solving/Troubleshooting</Form.Label>
                  <Form.Control
                    required
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={solvingHrs ? solvingHrs : 0}
                    onChange={(e) => setSolvingHrs(String(e.target.value))}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please fill in Solving/Troubleshooting Hours. It can be a
                    number or a decimal(upto 2 decimal places)
                  </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="Study/Research">
                  <Form.Label>Study/Research</Form.Label>
                  <Form.Control
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={studyHrs ? studyHrs : 0}
                    onChange={(e) => setStudyHrs(String(e.target.value))}
                  ></Form.Control>
                  <Form.Control.Feedback type="invalid">
                    Please fill Study/Research Hours in a valid format. It can
                    be a number or a decimal(upto 2 decimal places).
                  </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="Total">
                  <Form.Label>Total</Form.Label>
                  <Form.Control
                    disabled
                    value={
                      Number(studyHrs) + Number(solvingHrs)
                      // homework[0].timesheets[0] + homework[0].timesheets[1]
                    }
                  ></Form.Control>
                </Form.Group>

                <button type="button" className="btn btn-link" onClick={goBack}>
              Back
            </button>{" "}

                <Button type="submit" variant="primary" className="float-right">
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

export default HomeworkStudent;
