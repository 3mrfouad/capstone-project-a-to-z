import React, { useState, useEffect } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getHomeworkDetailInstructor,
  editHomeworkInstructor,
  getAllCourses,
  getAllInstructors,
} from "../../../actions/instructorActions";
import { Link } from "react-router-dom";

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
        // setTimeout(() => {
        //   setTitle(homework.Title);
        //   setAvgCompletionTime(homework.AvgCompletionTime);
        //   setDocumentLink(homework.DocumentLink);
        //   setGitHubClassRoomLink(homework.GitHubClassRoomLink);
        // }, 1000);
      } else {
        setTitle(homework.Title);
        setAvgCompletionTime(homework.AvgCompletionTime);
        setDocumentLink(homework.DocumentLink);
        setGitHubClassRoomLink(homework.GitHubClassRoomLink);
      }

      // setTimeout(() => {
      //   setTitle(homework.Title);
      //   setAvgCompletionTime(homework.AvgCompletionTime);
      //   setDocumentLink(homework.DocumentLink);
      //   setGitHubClassRoomLink(homework.GitHubClassRoomLink);
      // }, 500);
    }
  }, [dispatch, loading]);

  const submitHandler = (e) => {
    e.preventDefault();
    dispatch(
      editHomeworkInstructor({
        homeworkId,
        courseId,
        instructorId,
        cohortId: homework.CohortId,
        // isAssignment: "true",
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
        <h2>Loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              <Form onSubmit={submitHandler}>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    type="text"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control
                    as="select"
                    required
                    value={courseId}
                    onChange={(e) => setCourseId(e.target.value)}
                  >
                    <option value={homework.CourseId}>
                      {homework.CourseName}
                    </option>
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
                    as="select"
                    required
                    value={instructorId}
                    onChange={(e) => setInstructorId(e.target.value)}
                  >
                    <option value="">{homework.InstructorName}</option>
                    {instructors
                      .filter((item) => item.archive == false)
                      .map((instructor, index) => (
                        <option value={instructor.userId} key={index}>
                          {instructor.name}
                        </option>
                      ))}
                  </Form.Control>
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    type="text"
                    value={avgCompletionTime}
                    onChange={(e) => setAvgCompletionTime(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={dueDate}
                    onChange={(e) => setDueDate(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={releaseDate}
                    onChange={(e) => setReleaseDate(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    type="text"
                    value={documentLink}
                    onChange={(e) => setDocumentLink(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    value={gitHubClassRoomLink}
                    onChange={(e) => setGitHubClassRoomLink(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Link onClick={goBack}>Back</Link>

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
