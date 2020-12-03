import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getAllCourses,
  getAllInstructors,
  assignCourse,
} from "../../../actions/instructorActions";

const CourseAssign = ({ match }) => {
  const cohortId = match.params.id;
  const dispatch = useDispatch();
  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [resourcesLink, setResourcesLink] = useState("");
  const { loading, courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);
  const { success } = useSelector((state) => state.courseAssign);
  useEffect(() => {
    dispatch(getAllCourses());
    dispatch(getAllInstructors());
  }, []);
  const submitHandler = (e) => {
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
  };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h2>Course</h2>
            <Form onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Course Name</Form.Label>
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
              <Form.Group controlId="startdate">
                <Form.Label>Start Date</Form.Label>
                <Form.Control
                  type="text"
                  value={startDate}
                  onChange={(e) => setStartDate(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="enddate">
                <Form.Label>End Date</Form.Label>
                <Form.Control
                  type="text"
                  value={endDate}
                  onChange={(e) => setEndDate(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="hours">
                <Form.Label>Hours</Form.Label>
                <Form.Control disabled></Form.Control>
              </Form.Group>
              <Form.Group controlId="description">
                <Form.Label>Description</Form.Label>
                <Form.Control disabled></Form.Control>
              </Form.Group>
              <Form.Group controlId="link">
                <Form.Label>Resource Link</Form.Label>
                <Form.Control
                  type="text"
                  value={resourcesLink}
                  onChange={(e) => setResourcesLink(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <button type="button" className="btn btn-link">
                Back
              </button>
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
