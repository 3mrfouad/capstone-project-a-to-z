import React from "react";
import { Table, Container, Button, Modal } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { cohortSummaryInstructor } from "../../../actions/instructorActions";
import { Link } from "react-router-dom";

const CohortSummaryInstructor = () => {
  const dispatch = useDispatch();
  const { cohorts, loading } = useSelector(
    (state) => state.cohortSummaryInstructor
  );
  useEffect(() => {
    dispatch(cohortSummaryInstructor());
  }, [dispatch]);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  return (
    <React.Fragment>
      <Container>
        <Table>
          <thead>
            <tr>
              <th>Cohort Name</th>
              <th>Capacity</th>
              <th>Mode</th>
              <th>Start Date</th>
              <th>End Date</th>
              <th>City</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>4.2</td>
              <td>20</td>
              <td>Remote</td>
              <td>2020-08-04</td>
              <td>2021-01-30</td>
              <td>Edmonton </td>
              <td>
                Edit | <Link onClick={handleShow}> Retire </Link>
              </td>
            </tr>
            <tr>
              <td>4.2</td>
              <td>20</td>
              <td>Remote</td>
              <td>2020-08-04</td>
              <td>2021-01-30</td>
              <td>Edmonton </td>
              <td>Edit | Retire</td>
            </tr>
            <tr>
              <td>4.2</td>
              <td>20</td>
              <td>Remote</td>
              <td>2021-08-04</td>
              <td>2022-01-30</td>
              <td>Calgary</td>
              <td>Edit | Retire</td>
            </tr>
          </tbody>
        </Table>
        <button type="button" className="btn btn-link">
          Back
        </button>{" "}
        <Button className="float-right mr-3">Create Cohort</Button>
        <Button className="float-right mr-3">Register Users</Button>{" "}
        <Button className="float-right mr-3">Manage Course</Button>{" "}
      </Container>
      <Modal show={show} onHide={handleClose}>
        <Modal.Body>Retire: Are you sure?</Modal.Body>
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

export default CohortSummaryInstructor;
