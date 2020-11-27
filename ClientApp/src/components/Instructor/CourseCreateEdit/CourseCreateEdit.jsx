import React from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";

const CourseCreateEdit = () => {
  const submitHandler = (e) => {
    e.preventDefault();
    console.log("create cohort");
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
                    //   type="email"
                    //   placeholder="Enter Email"
                    //   value={email}
                    //   onChange={(e) => setEmail(e.target.value)}
                    ></Form.Control>
                  </Col>
                </Form.Row>
                <Form.Row className="mt-5">
                  <Form.Label className="mr-5">Hours</Form.Label>
                  <Col>
                    <Form.Control
                    //   type="email"
                    //   placeholder="Enter Email"
                    //   value={email}
                    //   onChange={(e) => setEmail(e.target.value)}
                    ></Form.Control>
                  </Col>
                </Form.Row>
                <Form.Row className="mt-5">
                  <Form.Label>Description</Form.Label>
                  <Col>
                    <Form.Control
                    //   type="email"
                    //   placeholder="Enter Email"
                    //   value={email}
                    //   onChange={(e) => setEmail(e.target.value)}
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

export default CourseCreateEdit;
