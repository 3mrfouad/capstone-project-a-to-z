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

 //(1) Add validation states
 const [validated, setValidated] = useState(false);   
   
 //----------------------------

  const submitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
        e.preventDefault();
        e.stopPropagation();
    }
    setValidated(true); 
  //(3) Add business logic- No business Logic for now

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
            <Form noValidate validated={validated} onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  required
                  type="text"
                  maxlength ="50"
                  placeholder="Enter Name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                ></Form.Control>
                 <Form.Control.Feedback type="invalid">
                    Please enter a name.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="email">
                <Form.Label>Email Address</Form.Label>
                <Form.Control
                  required
                  type="email"
                  maxlength ="50"
                  placeholder="Enter Email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                    Please enter an email.
                </Form.Control.Feedback>

              </Form.Group>

              <Form.Group controlId="password">
                <Form.Label>Password</Form.Label>
                <Form.Control
                  required
                  type="password"
                  minlength ="8"
                  pattern = "(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}"                  
                  placeholder="Enter Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                    Please enter a password.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="cohort">
                <Form.Label>Cohort</Form.Label>
                <Form.Control                
                  as="select"                 
                  value={cohort}
                  onChange={(e) => setCohort(e.target.value)}
                ><option value="">Select</option>
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
