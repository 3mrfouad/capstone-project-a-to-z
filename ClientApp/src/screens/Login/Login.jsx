import React from "react";
import { useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { Link, useHistory } from "react-router-dom";

const Login = () => {
  const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [validated, setValidated] = useState(false);
    const [invalidPasswordBL, setInvalidPasswordBl] = useState(false);

    let input = {};
    input["password"] = "";
    let errors = {};

    const submitHandler = (e) => {
        //(2) Add form validation condition block if-else
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        setValidated(true);
        e.preventDefault();
    // dispatch(login(email, password));
        console.log("login");
    };
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h1>Sign In</h1>
            {/* {error && <Message variant="danger">{error}</Message>}
            {loading && <Loader />} */}
                      <Form noValidate validated={validated} onSubmit={submitHandler}>
              <Form.Group controlId="email">
                <Form.Label>Email Address</Form.Label>
                <Form.Control
                                  required
                                  type="email"
                                  maxlength="50"
                                  placeholder="Enter Email"
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
                                  placeholder="Enter Password"
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
        {/* <Row className="py-3">
          <Col>
            New Customer? <Link to="/register">Register</Link>
          </Col>
        </Row> */}
      </Container>
      </React.Fragment>
  );
};

export default Login;
