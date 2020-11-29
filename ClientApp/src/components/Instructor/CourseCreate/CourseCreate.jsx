import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createCourse } from "../../../actions/instructorActions";

const CourseCreate = () => {
  const [courseName, setCourseName] = useState("");
  const [hours, setHours] = useState(0);
  const [description, setDescription] = useState("");
  const dispatch = useDispatch();
  useEffect(() => {
    // get course by id
    // populate the cohort data in here
  }, []);
  const courseCreate = useSelector((state) => state.courseCreate);
  const { loading, error, course } = courseCreate;
  const submitHandler = (e) => {
    e.preventDefault();
    console.log("create course");
    dispatch(createCourse());
  };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h2>Course</h2>
            <Form onSubmit={submitHandler}>
              <Form.Group controlId="">
                <Form.Row className="mt-5">
                  <Form.Label>Course Name</Form.Label>
                  <Col>
                    <Form.Control
                      type="text"
                      value={courseName}
                      onChange={(e) => setCourseName(e.target.value)}
                    ></Form.Control>
                  </Col>
                </Form.Row>
                <Form.Row className="mt-5">
                  <Form.Label className="mr-5">Hours</Form.Label>
                  <Col>
                    <Form.Control
                      type="text"
                      value={hours}
                      onChange={(e) => setHours(e.target.value)}
                    ></Form.Control>
                  </Col>
                </Form.Row>
                <Form.Row className="mt-5">
                  <Form.Label>Description</Form.Label>
                  <Col>
                    <Form.Control
                      type="text"
                      value={description}
                      onChange={(e) => setDescription(e.target.value)}
                    ></Form.Control>
                  </Col>
                </Form.Row>
              </Form.Group>
              <button type="button" className="btn btn-link">
                Back
              </button>{" "}
              <Button type="submit" className="float-right">
                Create Course
              </Button>
            </Form>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default CourseCreate;
