import React, { useState, useEffect } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getCoursesByCohortId,
  getAllInstructors,
  createHomeworkInstructor,
} from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const HomeworkCreateInstructor = ({ match, history }) => {
  const cohortId = match.params.id;
  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const [isAssignment, setIsAssignment] = useState("");
  const [title, setTitle] = useState("");
  const [avgCompletionTime, setAvgCompletionTime] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const [documentLink, setDocumentLink] = useState("");
  const [gitHubClassRoomLink, setGitHubClassRoomLink] = useState("");

  //(1) Add validation states (@Atinder)
  const [validated, setValidated] = useState(false);
  //----------------------------

  // ! (10.1) Anti-tamper validation - States and Variables
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let formSubmitIndicator = false;
  // ! ------------------------------------------------------

  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(getCoursesByCohortId(cohortId));
    dispatch(getAllInstructors());
  }, []);
  const { loading, error, success, courses } = useSelector(
    (state) => state.getCoursesByCohortId
  );
  const { instructors } = useSelector((state) => state.getAllInstructors);

  const goBack = () => {
    history.goBack();
  };

  // ! (10.2) Anti-tamper validation - Validate (parameters)
  function Validate(
    title,
    courseId,
    instructorId,
    avgCompletionTime,
    dueDate,
    releaseDate,
    documentLink,
    gitHubClassRoomLink
  ) {
    let parsedDueDate = 0;
    let parsedReleaseDate = 0;
    formSubmitIndicator = true;

    try {
      title = title.trim().toLowerCase();
      avgCompletionTime = avgCompletionTime.trim().toLowerCase();
      dueDate = dueDate.trim().toLowerCase();
      releaseDate = releaseDate.trim().toLowerCase();
      documentLink = documentLink.trim().toLowerCase();
      gitHubClassRoomLink = gitHubClassRoomLink.trim().toLowerCase();

      if (!title) {
        validFormData = false;
      } else if (title.Length > 100) {
        validFormData = false;
      } else if (!courseId) {
        validFormData = false;
      } else if (parseInt(courseId) > 2147483647 || parseFloat(courseId) < 1) {
        validFormData = false;
      } else if (!instructorId) {
        validFormData = false;
      } else if (
        parseInt(instructorId) > 2147483647 ||
        parseFloat(instructorId) < 1
      ) {
        validFormData = false;
      } else if (
        parseFloat(avgCompletionTime) > 999.99 ||
        parseFloat(avgCompletionTime) < 0
      ) {
        validFormData = false;
      } else if (!dueDate) {
        validFormData = false;
      } else if (!releaseDate) {
        validFormData = false;
      } else if (documentLink.Length > 250) {
        validFormData = false;
      } else if (gitHubClassRoomLink.Length > 250) {
        validFormData = false;
      } else if (
        documentLink &&
        !/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(
          documentLink
        )
      ) {
        validFormData = false;
      } else if (
        gitHubClassRoomLink &&
        !/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(
          gitHubClassRoomLink
        )
      ) {
        validFormData = false;
      } else {
        try {
          parsedReleaseDate = Date.parse(releaseDate);
        } catch (ParseException) {
          validFormData = false;
        }
        try {
          parsedDueDate = Date.parse(dueDate);
        } catch (ParseException) {
          validFormData = false;
        }
        /* Dates business logic */
        if (parsedDueDate < parsedReleaseDate) {
          validFormData = false;
        } else {
          validFormData = true;
        }
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
  // ! ------------------------------------------------------

  const handleSubmit = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    setValidated(true);
    //----------------------------

    // ! (10.3) Anti-tamper validation - Alert message conditions
    validFormData = false;
    formSubmitIndicator = true;
    setValidData(validFormData);
    // ! ------------------------------------------------------

    e.preventDefault();

    // ! (10.4) Anti-tamper validation - calling Validate
    Validate(
      title,
      courseId,
      instructorId,
      avgCompletionTime,
      dueDate,
      releaseDate,
      documentLink,
      gitHubClassRoomLink
    );
    if (validFormData) {
      setValidData(validFormData);
      // ! ------------------------------------------------------
      e.preventDefault();
      dispatch(
        createHomeworkInstructor({
          courseId,
          instructorId,
          cohortId,
          // isAssignment,
          title,
          avgCompletionTime,
          dueDate,
          releaseDate,
          documentLink,
          gitHubClassRoomLink,
        })
      );
    } else {
      // ! (10.5) Anti-tamper validation - Alert message conditions
      setValidData(validFormData);
    }
    // ! (10.6) Anti-tamper validation - Alert message conditions
    setFormSubmitted(formSubmitIndicator);
    // ! ------------------------------------------------------
  };

  return (
    <React.Fragment>
      {loading ? (
        <Loader />
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
                      ? `Unsuccessful attempt to create Homework. ${error.data}`
                      : !loading && !error && success
                      ? "Homework was successfully created"
                      : ""
                    : "Error: Form was submitted with invalid data fields"
                  : ""}
              </p>
              {/* ! ------------------------------------------------------  */}

              <Form noValidate validated={validated} onSubmit={handleSubmit}>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    maxLength="100"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    Please enter Title of Homework.(Max 100 characters allowed).
                  </Form.Control.Feedback>
                  {/*---------------------------------------*/}
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control
                    as="select"
                    required
                    value={courseId}
                    onChange={(e) => setCourseId(e.target.value)}
                  >
                    <option value="">select</option>
                    {courses.map((course, index) => (
                      <option value={course.item1.courseId} key={index}>
                        {course.item1.name}
                      </option>
                    ))}
                  </Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    Please select a course for this homework.
                  </Form.Control.Feedback>
                  {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    as="select"
                    required
                    value={instructorId}
                    onChange={(e) => setInstructorId(e.target.value)}
                  >
                    <option value="">select</option>
                    {instructors
                      .filter((item) => item.archive == false)
                      .map((instructor, index) => (
                        <option value={instructor.userId} key={index}>
                          {instructor.name}
                        </option>
                      ))}
                  </Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    Please select an Instructor for this homework.
                  </Form.Control.Feedback>
                  {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={avgCompletionTime}
                    onChange={(e) =>
                      setAvgCompletionTime(String(e.target.value))
                    }
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    Please enter average completion time between 0 and 999.99
                    inclusive
                  </Form.Control.Feedback>
                  {/*---------------------------------------*/}
                </Form.Group>
                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={releaseDate}
                    onChange={(e) => setReleaseDate(String(e.target.value))}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    Please enter Date in format: yyyy/mm/dd.
                  </Form.Control.Feedback>

                  {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    type="date"
                    min={releaseDate}
                    value={dueDate}
                    onChange={(e) => setDueDate(String(e.target.value))}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    Please enter Date in format: yyyy/mm/dd. Due Date can not be
                    set before Release Date.
                  </Form.Control.Feedback>

                  {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    type="url"
                    maxLength="250"
                    value={documentLink}
                    placeholder="example: https://www.google.com "
                    onChange={(e) => setDocumentLink(String(e.target.value))}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    <p>
                      Please enter Document Link in format:
                      http|https://yourLink.
                    </p>
                    <p>Max 250 characters allowed.</p>
                  </Form.Control.Feedback>

                  {/*---------------------------------------*/}
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    type="url"
                    maxLength="250"
                    value={gitHubClassRoomLink}
                    placeholder="example: https://www.google.com"
                    onChange={(e) =>
                      setGitHubClassRoomLink(String(e.target.value))
                    }
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                  <Form.Control.Feedback type="invalid">
                    <p>
                      Please enter Document Link in format:
                      http|https://yourLink.
                    </p>
                    <p>Max 250 characters allowed.</p>
                  </Form.Control.Feedback>

                  {/*---------------------------------------*/}
                </Form.Group>
                <Button variant="primary" onClick={goBack}>
                  Back
                </Button>
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

export default HomeworkCreateInstructor;
