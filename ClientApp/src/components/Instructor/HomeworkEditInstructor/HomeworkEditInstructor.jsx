import React from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";

//(1) Add validation states
const [validated, setValidated] = useState(false);
const [invalidDatesBL, setInvalidDatesBl] = useState(false);
const [validStartDate, setValidStartDate] = useState(false);
const [validEndDate, setValidEndDate] = useState(false);
let temp = false;
//----------------------------
const HomeworkEditInstructor = () => {
  return (
    <React.Fragment>
      <Container>
        <Row className="justify-content-md-center">
          <Col xs={12} md={6}>
            <h3>Homework</h3>
            <Form>
              <Form.Group controlId="title">
                <Form.Label>Title</Form.Label>
                <Form.Control
                  required
                  type="text"
                  maxLength="100"
                  value={name}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="Course">
                <Form.Label>Course</Form.Label>
                <Form.Control as="select" required value={courseId}>
                  <option></option>
                </Form.Control>
              </Form.Group>

              <Form.Group controlId="instructor">
                <Form.Label>Instructor</Form.Label>
                <Form.Control as="select" required value={instructorId}>
                  <option></option>
                </Form.Control>
              </Form.Group>

              <Form.Group controlId="Avg Completion Time">
                <Form.Label>Avg Completion Time</Form.Label>
                <Form.Control
                  type="number"
                  min={0}
                  max={999.99}
                  step="0.1"
                  value={avgCompletionTime}
                ></Form.Control>
              </Form.Group>

              <Form.Group controlId="Due Date">
                <Form.Label>Due Date</Form.Label>
                <Form.Control
                  type="date"
                  min={releaseDate}
                  value={dueDate}
                ></Form.Control>
              </Form.Group>

              <Form.Group controlId="Release Date">
                <Form.Label>Release Date</Form.Label>
                <Form.Control type="date" value={releaseDate}></Form.Control>
              </Form.Group>

              <Form.Group controlId="DocLink">
                <Form.Label>DocLink</Form.Label>
                <Form.Control
                  type="url"
                  maxLength="250"
                  value={docLink}
                ></Form.Control>
              </Form.Group>
              <Form.Group controlId="GitHubLink">
                <Form.Label>GitHubLink</Form.Label>
                <Form.Control
                  type="url"
                  maxLength="250"
                  value={gitHubLink}
                ></Form.Control>
              </Form.Group>
            </Form>
            <Form>
              <h3>Rubric</h3>
              <Form.Group controlId="Challenge">
                <Form.Check
                  type="checkbox"
                  label="Challenge"
                  value={isChallenge}
                />
              </Form.Group>
              <Form.Group controlId="Criteria">
                <Form.Label>Criteria</Form.Label>
                <Form.Control type="text" value={criteria}></Form.Control>
              </Form.Group>
              <Form.Group controlId="Weight">
                <Form.Label>Weight</Form.Label>
                <Form.Control
                  type="number"
                  min={0}
                  max={999}
                  step="1"
                  value={weight}
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
    </React.Fragment>
  );
};

export default HomeworkEditInstructor;
