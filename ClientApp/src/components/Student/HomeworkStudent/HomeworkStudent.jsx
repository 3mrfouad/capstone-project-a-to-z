import React, { useState } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  updateTimeSheetStudent,
  homeworkStudent,
} from "../../../actions/studentActions";

const HomeworkStudent = () => {
  const [solvingHrs, setSolvingHrs] = useState("");
  const [studyHrs, setStudyHrs] = useState("");
  const dispatch = useDispatch();

  const { homework, loading } = useSelector((state) => state.homeworkStudent);
  useEffect(() => {
    dispatch(homeworkStudent());
  }, [dispatch]);
  // console.log(homework);
  const summitHandler = (e) => {
    e.preventDefault();
    dispatch(updateTimeSheetStudent(solvingHrs, studyHrs));
  };
  return (
    <React.Fragment>
      {homework.length < 1 ? (
        <h2>Loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              <Form onSubmit={summitHandler}>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].courseId}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].courseId}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].instructorId}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].avgCompletionTime}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].dueDate}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].releaseDate}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].documentLink}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    disabled
                    value={homework[0].gitHubClassRoomLink}
                  ></Form.Control>
                </Form.Group>
              </Form>
              <Form>
                <h3>Timesheet</h3>
                <Form.Group controlId="Solving/Troubleshooting">
                  <Form.Label>Solving/Troubleshooting</Form.Label>
                  <Form.Control
                    value={homework[0].timesheets[0]}
                    onChange={(e) => setSolvingHrs(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Study/Research">
                  <Form.Label>Study/Research</Form.Label>
                  <Form.Control
                    value={homework[0].timesheets[1]}
                    onChange={(e) => setStudyHrs(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Total">
                  <Form.Label>Total</Form.Label>
                  <Form.Control
                    disabled
                    value={
                      homework[0].timesheets[0] + homework[0].timesheets[1]
                    }
                  ></Form.Control>
                </Form.Group>

                <a href="">Back</a>

                <Button type="submit" variant="primary" className="float-right">
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

export default HomeworkStudent;
