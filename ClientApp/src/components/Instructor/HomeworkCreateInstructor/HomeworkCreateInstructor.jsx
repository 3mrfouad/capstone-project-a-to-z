import React, { useState, useEffect } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getAllCourses,
  getAllInstructors,
  createHomeworkInstructor,
} from "../../../actions/instructorActions";

const HomeworkCreateInstructor = ({ match, history }) => {
  const cohortId = match.params.id;
  // const courseId = match.params.courseId;

  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const [isAssignment, setIsAssignment] = useState("");
  const [title, setTitle] = useState("");
  const [avgCompletionTime, setAvgCompletionTime] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const [documentLink, setDocumentLink] = useState("");
  const [gitHubClassRoomLink, setGitHubClassRoomLink] = useState("");
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(getAllCourses());
    dispatch(getAllInstructors());
  }, []);

  const { loading, courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);
  const goBack = () => {
    history.goBack();
  };
  const handleSubmit = (e) => {
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
  };

  return (
    <React.Fragment>
      {loading ? (
        <h2>Loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              <Form onSubmit={handleSubmit}>
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
                    value={courseId}
                    onChange={(e) => setCourseId(e.target.value)}
                  >
                    <option value="">select</option>
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
                    value={instructorId}
                    onChange={(e) => setInstructorId(e.target.value)}
                  >
                    <option value="">select</option>
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
                    type="text"
                    value={avgCompletionTime}
                    onChange={(e) => setAvgCompletionTime(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    type="text"
                    value={dueDate}
                    onChange={(e) => setDueDate(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    type="text"
                    value={releaseDate}
                    onChange={(e) => setReleaseDate(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    type="text"
                    value={documentLink}
                    placeholder="https://www.google.com "
                    onChange={(e) => setDocumentLink(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    type="text"
                    value={gitHubClassRoomLink}
                    placeholder="https://www.google.com"
                    onChange={(e) => setGitHubClassRoomLink(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <h3>Rubric</h3>
                <Form.Group controlId="Challenge">
                  <Form.Label>Challenge</Form.Label>
                  <Form.Control
                  //   type="text"
                  //   placeholder="Enter Description"
                  //   value={description}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Criteria">
                  <Form.Label>Criteria</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    // value={homework.item3}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Weight">
                  <Form.Label>Weight</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    // value={homework.item3}
                  ></Form.Control>
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
