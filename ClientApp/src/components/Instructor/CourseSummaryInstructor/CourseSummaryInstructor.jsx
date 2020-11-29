import React, { useState } from "react";
import { Table, Container, Button, Modal } from "react-bootstrap";
import { Link } from "react-router-dom";

const CourseSummaryInstructor = () => {
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  return (
    <React.Fragment>
      <Container>
        <h2>Cohort 4.2</h2>
        <Table>
          <thead>
            <tr>
              <th>Course Name</th>
              <th>Description</th>
              <th>Duration</th>
              <th>Instructor</th>
              <th>Homework</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>React.js</td>
              <td>React Basics</td>
              <td>10</td>
              <td>Instructor A</td>
              <td>
                <a href="#">Homework</a>{" "}
              </td>
              <td>
                Edit | <Link onClick={handleShow}>Remove</Link>{" "}
              </td>
            </tr>
            <tr>
              <td>CSS</td>
              <td>CSS Basics</td>
              <td>10</td>
              <td>Instructor B</td>
              <td>
                <a href="#">Homework</a>{" "}
              </td>
              <td>Edit | Remove</td>
            </tr>
            <tr>
              <td>HTML</td>
              <td>HTML Basics</td>
              <td>10</td>
              <td>Instructor B</td>
              <td>
                {" "}
                <a href="#">Homework</a>{" "}
              </td>
              <td>Edit | Remove</td>
            </tr>
          </tbody>
        </Table>
        <button type="button" className="btn btn-link">
          Back
        </button>{" "}
        <Button className="float-right mr-3">Add Course</Button>
      </Container>
      <Modal show={show} onHide={handleClose}>
        <Modal.Body>Archive: Are you sure?</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            No
          </Button>
          <Button variant="primary" onClick={handleClose}>
            Yes
          </Button>
        </Modal.Footer>
      </Modal>
    </React.Fragment>
  );
};

export default CourseSummaryInstructor;
