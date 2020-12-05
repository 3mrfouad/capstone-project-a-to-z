import React from "react";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { Link, useHistory } from "react-router-dom";
import { loginUser } from "../../actions/instructorActions";

const Login = () => {
  const dispatch = useDispatch();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [validated, setValidated] = useState(false);
  const [invalidPasswordBL, setInvalidPasswordBl] = useState(false);
  // ! (10.1) Anti-tamper validation - States and Variables
  const [validData, setValidData] = useState(false);
  const [formSubmitted, setFormSubmitted] = useState(false);

  const { user, success } = useSelector((state) => state.userLoginState);
  let validFormData = false;
  let formSubmitIndicator = false;
  //! -------Created by Ayesha(For Store that needs to be created)---
  //const { loading, error, user, success } = login;
  //!-----------------------------------------

  // ! ------------------------------------------------------
  // ! (10.2) Anti-tamper validation - Validate (parameters)
  function Validate(email, password) {
    formSubmitIndicator = true;

    try {
      email = email.trim().toLowerCase();

      if (!email) {
        validFormData = false;
      } else if (email.Length > 50) {
        validFormData = false;
      } else if (!/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(email)) {
        validFormData = false;
      } else if (!password) {
        validFormData = false;
      } else if (password.Length > 250) {
        validFormData = false;
      }
    } catch (Exception) {
      validFormData = false;
    }
  }
  const submitHandler = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    setValidated(true);
    e.preventDefault();
    dispatch(
      loginUser({
        email,
        password,
      })
    );
    // ! (10.4) Anti-tamper validation - calling Validate  -Created by ayesha need to uncomment it once store is created
    // Validate(email,password);
    //if (validFormData) {
    //  setValidData(validFormData);
    // ! ------------------------------------------------------
    console.log("login");
    // dispatch(login(email, password));
    //}
    //else {
    // ! (10.5) Anti-tamper validation - Alert message conditions
    //  setValidData(validFormData);
    //}

    // ! (10.6) Anti-tamper validation - Alert message conditions
    setFormSubmitted(formSubmitIndicator);
    // ! ------------------------------------------------------
  };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h1>Sign In</h1>
            {/* ! (10.7) Anti-tamper validation - Alert message conditions   */}
            {/*  <p
              class={
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
                    ? "Unsuccessful attempt to Login"
                    : !loading && !error && success
                    ? "Successful Login"
                    : ""
                  : "Error: Form was submitted with invalid data fields"
                : ""}
              </p>*/}
            {/* {error && <Message variant="danger">{error}</Message>}
            {loading && <Loader />} */}
            <Form noValidate validated={validated} onSubmit={submitHandler}>
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
                  Please enter an email address.
                </Form.Control.Feedback>
              </Form.Group>

              <Form.Group controlId="password">
                <Form.Label>Password</Form.Label>
                <Form.Control
                  required
                  type="password"
                  name="password"
                  value={password}
                  maxLength="250"
                  onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Button type="submit" variant="primary">
                {" "}
                Sign In
              </Button>
            </Form>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default Login;
