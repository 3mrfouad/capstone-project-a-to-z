import React from "react";
import { useState } from "react";
import { Form, Button, Row, Col, Container, Modal } from "react-bootstrap";
import { Link, useHistory } from "react-router-dom";

const Register = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [cohort, setCohort] = useState("");
  const [isInstructor, setIsInstructor] = useState(false);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const handleChange = () => {
    setIsInstructor(!isInstructor);
  };
  const submitHandler = (e) => {
    e.preventDefault();
    // dispatch(login(email, password));
    console.log("register");
  };
  return (
    <>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            {/* {error && <Message variant="danger">{error}</Message>}
            {loading && <Loader />} */}
            <Form onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter Name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="email">
                <Form.Label>Email Address</Form.Label>
                <Form.Control
                  type="email"
                  placeholder="Enter Email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                ></Form.Control>
              </Form.Group>

              <Form.Group controlId="password">
                <Form.Label>Password</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="Enter Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="cohort">
                <Form.Label>Cohort</Form.Label>
                <Form.Control
                  as="select"
                  value={cohort}
                  onChange={(e) => setCohort(e.target.value)}
                >
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                </Form.Control>
              </Form.Group>
              <Form.Group controlId="isInstructor">
                <Form.Check
                  type="checkbox"
                  label="Instructor"
                  value={isInstructor}
                  onChange={handleChange}
                />
              </Form.Group>
              <Button type="submit" variant="primary">
                {" "}
                Register
              </Button>
            </Form>
          </Col>
        </Row>
        {/* <Row className="py-3">
          <Col>
            New Customer? <Link to="/register">Register</Link>
          </Col>
        </Row> */}
      </Container>
      <Modal show={show} onHide={handleClose}>
        <Modal.Body>
          You are giving this user instructor(Write/Edit) privileges.Are you
          sure?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Proceed
          </Button>
          <Button variant="primary" onClick={handleClose}>
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default Register;
