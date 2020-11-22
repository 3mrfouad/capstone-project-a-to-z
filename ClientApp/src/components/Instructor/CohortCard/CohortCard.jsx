import React from 'react';
import { Form, Button, Row, Col, Container } from "react-bootstrap";

const CohortCard = () => {
    const submitHandler=(e)=>{
            e.preventDefault();
            console.log("create cohort");
    }
    return ( 
        <React.Fragment>
         <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h2>Cohort</h2>
            <Form onSubmit={submitHandler}>
              <Form.Group controlId="">
                <Form.Label>Cohort Name</Form.Label>
                <Form.Control
                //   type="email"
                //   placeholder="Enter Email"
                //   value={email}
                //   onChange={(e) => setEmail(e.target.value)}
                ></Form.Control>
              </Form.Group>

              <Form.Group controlId="">
                <Form.Label>Capacity</Form.Label>
                <Form.Control
                //   type="password"
                //   placeholder="Enter Password"
                //   value={password}
                //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>Mode of Teaching</Form.Label>
                <Form.Control
                //   type="password"
                //   placeholder="Enter Password"
                //   value={password}
                //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>Start Date</Form.Label>
                <Form.Control
                //   type="password"
                //   placeholder="Enter Password"
                //   value={password}
                //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>End Date</Form.Label>
                <Form.Control
                //   type="password"
                //   placeholder="Enter Password"
                //   value={password}
                //   onChange={(e) => setPassword(e.target.value)}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="">
                <Form.Label>City</Form.Label>
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
}
 
export default CohortCard;