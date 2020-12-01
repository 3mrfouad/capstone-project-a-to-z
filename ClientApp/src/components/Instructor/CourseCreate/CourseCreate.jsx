import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createCourse } from "../../../actions/instructorActions";

const CourseCreate = () => {
  const [courseName, setCourseName] = useState("");
  const [hours, setHours] = useState("");
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
    dispatch(
      createCourse({
        courseName,
        hours,
        description,
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
              <Form.Group controlId="CourseName">
                <Form.Label>Course Name</Form.Label>

                <Form.Control
                  type="text"
                  value={courseName}
                  onChange={(e) => setCourseName(e.target.value)}
                ></Form.Control>

                <Form.Label className="mr-5">Hours</Form.Label>

                <Form.Control
                  type="text"
                  value={hours}
                  onChange={(e) => setHours(e.target.value)}
                ></Form.Control>

                <Form.Label>Description</Form.Label>

                <Form.Control
                  type="text"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                ></Form.Control>
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
