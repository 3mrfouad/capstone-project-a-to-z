import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getAllInstructors,
  getAssignedCourse,
  editAssignedCourse,
} from "../../../actions/instructorActions";

const CourseEditAssigned = ({ match }) => {
  const cohortId = match.params.id;
  const courseId = match.params.courseId;
  const dispatch = useDispatch();
  const [instructorId, setInstructorId] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
    const [resourcesLink, setResourcesLink] = useState("");
    //(1) Add validation states
    const [validated, setValidated] = useState(false);
    const [invalidDatesBL, setInvalidDatesBl] = useState(false);

  const { instructors } = useSelector((state) => state.getAllInstructors);
  const { loading, course, success } = useSelector(
    (state) => state.getAssignedCourse
  );

  useEffect(() => {
    if (!success) {
      dispatch(getAssignedCourse(courseId, cohortId));
    }

    dispatch(getAllInstructors());
  }, [dispatch, courseId, cohortId, success]);
    const submitHandler = (e) => {
        //(2) Add form validation condition block if-else
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        setValidated(true);
        //(3) Add business logic
        if (endDate === "" || startDate === "" || Date.parse(endDate) < Date.parse(startDate)) {
            e.preventDefault();
            Date.parse(endDate) < Date.parse(startDate) ? setInvalidDatesBl(true) : setInvalidDatesBl(false);
            setEndDate("");
        } else {
            e.preventDefault();
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
        }
    };
  return (
    <React.Fragment>
        {loading ? (
        <h2>loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h2>Course</h2>
                              <Form noValidate validated={validated} onSubmit={submitHandler}>
                <Form.Group controlId="course name">
                  <Form.Label>Course Name</Form.Label>
                                      <Form.Control value={course.item1} disabled>
                                          {/* {courses.map((course, index) => (
                    <option value={course.courseId} key={index}>
                      {course.name}
                    </option>
                  ))} */}
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
                    <option value="">Select</option>
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
                                          required
                                          type="date"
                                          value={startDate}
                    onChange={(e) => setStartDate(String(e.target.value))}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="enddate">
                  <Form.Label>End Date</Form.Label>
                                      <Form.Control
                                          required
                    type="date"
                    value={endDate}
                    onChange={(e) => setEndDate(String(e.target.value))}
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
                                          maxlength="250"
                    type="url"
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
      )}
    </React.Fragment>
  );
};

export default CourseEditAssigned;
