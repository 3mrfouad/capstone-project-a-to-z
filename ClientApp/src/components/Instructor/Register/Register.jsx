import React from "react";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Form, Button, Row, Col, Container, Modal } from "react-bootstrap";
import { Link, useHistory } from "react-router-dom";
import { registerUser } from "../../../actions/instructorActions";
const Register = () => {
  const dispatch = useDispatch();
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
  const { success } = useSelector((state) => state.userRegisterState);

  //(1) Add validation states
  const [validated, setValidated] = useState(false);

  //----------------------------

  // ! (10.1) Anti-tamper validation - States and Variables
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);
  let validFormData = false;
  let formSubmitIndicator = false;
  // ! ------------------------------------------------------

  // ! (10.2) Anti-tamper validation - Validate (parameters)
  function Validate(name, email, password, cohort) {
    formSubmitIndicator = true;

    try {
      name = name.trim().toLowerCase();
      email = email.trim().toLowerCase();
      password = password.trim().toLowerCase();
      cohort = cohort.trim().toLowerCase();
      isInstructor = isInstructor.trim().toLowerCase();

      if (!name) {
        validFormData = false;
      } else if (name.Length > 50) {
        validFormData = false;
      }
      // else if (!cohort) { validFormData = false; }
      else if (parseInt(cohort) > 2147483647 || parseInt(cohort) < 1) {
        validFormData = false;
        console.log("cohort: ", parseInt(cohort));
      } else if (!isInstructor) {
        validFormData = false;
        console.log("isInstructor");
      } else if (!(isInstructor === "true" || isInstructor === "false")) {
        validFormData = false;
      } else if (!email) {
        validFormData = false;
        console.log("email");
      } else if (email.Length > 50) {
        validFormData = false;
        console.log("email length");
      } else if (!password) {
        validFormData = false;
        console.log("password");
      } else if (password.Length < 8 || password.Length > 250) {
        validFormData = false;
        console.log("password length");
      } else {
        if (
          !/(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])/.test(
            password
          )
        ) {
          validFormData = false;
          console.log("password does not match the criteria");
        } else if (!/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(email)) {
          validFormData = false;
          console.log("email does not match the email format");
        } else {
          validFormData = true;
          console.log("All good :", validFormData);
        }
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
  // ! ------------------------------------------------------

  const submitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    console.log("pass initial validation 100");
    setValidated(true);
    //(3) Add business logic- no bl for now

    e.preventDefault();
    // dispatch(login(email, password));
    if (isInstructor) {
      handleShow();
    } else {
      dispatch(
        registerUser({
          cohort,
          name,
          password,
          email,
          isInstructor,
        })
      );
    }
  };

  const handleRegisterInstructor = () => {
    dispatch(
      registerUser({
        cohort,
        name,
        password,
        email,
        isInstructor,
      })
    );
    handleClose();

    // ! (10.4) Anti-tamper validation - calling Validate
    Validate(name, email, password, cohort);
    if (validFormData) {
      setValidData(validFormData);
      // ! ------------------------------------------------------

      // dispatch(login(email, password));
      console.log("register");
    } else {
      // ! (10.5) Anti-tamper validation - Alert message conditions
      setValidData(validFormData);
    }
    // ! (10.6) Anti-tamper validation - Alert message conditions
    setFormSubmitted(formSubmitIndicator);
    // ! ------------------------------------------------------
  };

  return (
    <>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            {/* {error && <Message variant="danger">{error}</Message>}
            {loading && <Loader />} */}
            {/* ! (10.7) Anti-tamper validation - Alert message conditions   */}
            <br></br>
            <br></br>
            <p
              className={
                formSubmitted
                  ? validData
                    ? !loading && error
                      ? "alert alert-danger"
                      : !loading && !error && success
                      ? "alert alert-success"
                      : ""
                    : "alert alert-danger"
                  : ""
              }
              role="alert"
            >
              {formSubmitted
                ? validData
                  ? !loading && error
                    ? "Unsuccessful attempt to create a cohort"
                    : !loading && !error && success
                    ? "Cohort was successfully created"
                    : ""
                  : "Error: Form were submitted with invalid data fields"
                : ""}
            </p>
            {/* ! ------------------------------------------------------  */}
            <Form noValidate validated={validated} onSubmit={submitHandler}>
              <Form.Group controlId="name">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  required
                  type="text"
                  maxlength="50"
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
                  maxlength="50"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please enter a valid email. e.g. youremailaddress@domain.com
                </Form.Control.Feedback>
              </Form.Group>

              <Form.Group controlId="password">
                <Form.Label>Password</Form.Label>
                <Form.Control
                  required
                  type="password"
                  minLength="8"
                  pattern="(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
                <Form.Control.Feedback type="invalid">
                  Please enter a valid password. Format: atleast- 1 small
                  letter, 1 capital letter, 1 digit & 1 special character
                  required
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group controlId="cohort">
                <Form.Label>Cohort</Form.Label>
                <Form.Control
                  as="select"
                  value={cohort}
                  onChange={(e) => setCohort(e.target.value)}
                >
                  <option value="">Select</option>
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
          <Button variant="secondary" onClick={handleRegisterInstructor}>
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
