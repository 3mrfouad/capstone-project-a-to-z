import React, { useState, useEffect } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getHomeworkDetailInstructor,
  editHomeworkInstructor,
  getAllCourses,
  getAllInstructors,
} from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const HomeworkViewInstructor = ({ match, history }) => {
  const homeworkId = match.params.id;
  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const [avgCompletionTime, setAvgCompletionTime] = useState("");
  const [title, setTitle] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const [documentLink, setDocumentLink] = useState("");
  const [gitHubClassRoomLink, setGitHubClassRoomLink] = useState("");
  const [isAssignment, setIsAssignment] = useState("");

  //(1) Add validation states (@Atinder)
  const [validated, setValidated] = useState(false);
  //----------------------------

  const dispatch = useDispatch();
  const { loading, homework } = useSelector(
    (state) => state.homeworkDetailInstructor
  );
  useEffect(() => {
    if (!homework) {
      dispatch(getHomeworkDetailInstructor(homeworkId));
      dispatch(getAllCourses());
      dispatch(getAllInstructors());
    } else {
      if (!homework.HomeworkId || homework.HomeworkId != homeworkId) {
        dispatch(getHomeworkDetailInstructor(homeworkId));
      } else {
        setTitle(homework.Title);
        setAvgCompletionTime(homework.AvgCompletionTime);
        setDocumentLink(homework.DocumentLink);
        setGitHubClassRoomLink(homework.GitHubClassRoomLink);
        setInstructorId(homework.InstructorId);
        setCourseId(homework.CourseId);
      }
    }
  }, [dispatch, loading]);

  const submitHandler = (e) => {

  //(2) Add form validation condition block if-else
  const form = e.currentTarget;
  if (form.checkValidity() === false) {
    e.preventDefault();
    e.stopPropagation();
  }
  setValidated(true);
  //----------------------------

    e.preventDefault();
    dispatch(
      editHomeworkInstructor({
        homeworkId,
        courseId,
        instructorId,
        cohortId: homework.CohortId,
        title,
        avgCompletionTime,
        dueDate,
        releaseDate,
        documentLink,
        gitHubClassRoomLink,
      })
    );
  };

  const goBack = () => {
    history.goBack();
  };

  const { success } = useSelector((state) => state.editHomeworkInstructorState);

  const { courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              <Form noValidate validated={validated} onSubmit={submitHandler}>
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
                  Please enter Title of Homework.
                    <p>Max 100 characters allowed.</p>
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
                    {courses.map((course, index) => (
                      <option value={course.courseId} key={index}>
                        {course.name}
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
                    onChange={(e) => setAvgCompletionTime(String(e.target.value))}
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
                    Please enter Date in format: yyyy/mm/dd. 
                    <p>Due Date can not be set before Release Date.</p>
                  </Form.Control.Feedback>
                  {/*---------------------------------------*/}
                </Form.Group>
                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    type="url"
                    maxLength="250"
                    value={documentLink}
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
                    onChange={(e) => setGitHubClassRoomLink(e.target.value)}
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

export default HomeworkViewInstructor;
