import React from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";

const CourseEditAssigned = () => {
  const submitHandler = (e) => {
    e.preventDefault();
    console.log("Course Edit Assigned");
  };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h2>Course</h2>
            <Form onSubmit={submitHandler}>
              <Form.Group controlId="">
                <Form.Label>Course Name</Form.Label>
                <Form.Control
                  disabled
                  //   type="email"
                  //   placeholder="Enter Email"
                  //   value={email}
                  //   onChange={(e) => setEmail(e.target.value)}
                ></Form.Control>
              </Form.Group>

              <Form.Group controlId="">
                <Form.Label>Hours</Form.Label>
                <Form.Control
                  disabled
                  //   type="password"
                  //   placeholder="Enter Password"
                  //   value={password}
                  //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  disabled
                  //   type="password"
                  //   placeholder="Enter Password"
                  //   value={password}
                  //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>Resources Link</Form.Label>
                <Form.Control
                //   type="password"
                //   placeholder="Enter Password"
                //   value={password}
                //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>Instructor</Form.Label>
                <Form.Control
                //   type="password"
                //   placeholder="Enter Password"
                //   value={password}
                //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Button type="submit" variant="primary">
                {" "}
                Save
              </Button>
            </Form>
          </Col>
        </Row>
        <Button>Back</Button>
      </Container>
    </React.Fragment>
  );
};

export default CourseEditAssigned;