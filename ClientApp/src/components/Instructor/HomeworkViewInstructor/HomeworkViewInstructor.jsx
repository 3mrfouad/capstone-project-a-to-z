import React, { useState, useEffect } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { getHomeworkDetailInstructor } from "../../../actions/instructorActions";

const HomeworkViewInstructor = ({ match }) => {
  const homeworkId = match.params.id;
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(getHomeworkDetailInstructor(homeworkId));
  }, [dispatch]);

  const { loading, homework } = useSelector(
    (state) => state.homeworkDetailInstructor
  );

  return (
    <React.Fragment>
      {loading ? (
        <h2>Loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              <Form>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    type="text"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control
                    type="text"
                    value={homework.item4}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    type="text"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    type="text"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    type="number"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    type="text"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>
              </Form>
              <Form>
                <h3>Rubric</h3>
                <Form.Group controlId="Challenge">
                  <Form.Label>Challenge</Form.Label>
                  <Form.Control
                  //   type="text"
                  //   placeholder="Enter Description"
                  //   value={description}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Criteria">
                  <Form.Label>Criteria</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Weight">
                  <Form.Label>Weight</Form.Label>
                  <Form.Control
                    type="text"
                    //   placeholder="Enter Description"
                    value={homework.item3}
                  ></Form.Control>
                </Form.Group>

                <a href="">Back</a>

                <Button type="submit" variant="primary">
                  Save
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default HomeworkViewInstructor;
